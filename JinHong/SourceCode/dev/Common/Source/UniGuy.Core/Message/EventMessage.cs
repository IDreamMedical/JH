using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

//  Not Tested...
namespace Update1.Core.Messaging
{
    public struct EventMessage
    {
        public EventMessage(object sender,
                       Guid msg,
                       string t,
                       object param)
        {
            Sender = sender;
            MsgType = t;
            Msg = msg;
            Param = param;
        }

        public object Sender;
        public string MsgType;
        public Guid Msg;
        public object Param;
    }

    /// <example>
    /*
        #region IMessageRecipient implementation

        public object SendMessage(Message msg)
        {
            return m_messageHelper.Invoke(msg.Sender, msg.Msg, msg.Param);
        }

        public IAsyncMessageResult PostMessage(Message msg)
        {
            return m_messageHelper.BeginInvoke(msg.Sender, msg.Msg, msg.Param);
        }

        #endregion
     */
    /// </example>
    public interface IMessageRecipient
    {
        object SendMessage(EventMessage msg);
        IAsyncResult PostMessage(EventMessage msg);
    }

    public class AsyncMessageResult : IAsyncResult
    {
        Task task;

        public AsyncMessageResult(Task t)
        {
            task = t;
        }

        public bool Wait(TimeSpan timeout)
        {
            // Don't use DispatcherOperation.Wait, as this will 
            // call PushFrame if we're in the dispatcher's thread, and
            // may throw an exception in some re-entrant situations
            /*
            new DispatcherOperationEvent(m_operation).WaitOne(timeout);
            return m_operation.Status == DispatcherOperationStatus.Completed;*/
            task.Wait(timeout);
            return task.Status == TaskStatus.RanToCompletion;
        }

        public object Result
        {
            get { return task.Status; }
        }

        public bool IsCompleted
        {
            get
            {
                return task.Status == TaskStatus.RanToCompletion ||
                    task.Status == TaskStatus.Canceled ||
                    task.Status == TaskStatus.Faulted;
            }
        }

        public object AsyncState
        {
            get { return task.AsyncState; }
        }

        public WaitHandle AsyncWaitHandle
        {
            get { throw new NotImplementedException(); }
        }

        public bool CompletedSynchronously
        {
            get { throw new NotImplementedException(); }
        }
    }

    /// <summary>
    /// For event calling between de-coupled modules.
    /// Each modules can register callbacks, and also invokes messages.
    /// Procedures:
    /// 1: Inherit IMessageRecipient, and implement it;
    /// 2: Register callbacks when initialized, and/or invokes messages somewhere. It's all your freedom. It is not necessarily in the class that inherits IMessageRecipient;
    /// </summary>
    public class MessageHelper
    {
        public delegate object MessageCallback(object sender, Guid msg, object param);
        private Dictionary<Guid, MessageCallback> m_callbacks = new Dictionary<Guid, MessageCallback>();

        public MessageHelper()
        {
        }

        public void RegisterMessageCallback(Guid msg, MessageCallback callback)
        {
            try
            {
                lock (m_callbacks)
                {
                    if (m_callbacks.ContainsKey(msg))
                    {
                        m_callbacks[msg] = callback;
                        throw new Exception("MessageCallback " + callback.Method.ToString() + " already registered with the framework.  Reregistering current MessageCallback against msg guid " + msg);
                    }
                    else
                        m_callbacks.Add(msg, callback);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeregisterMessageCallback(Guid msg)
        {
            lock (m_callbacks)
            {
                if (m_callbacks.ContainsKey(msg))
                    m_callbacks.Remove(msg);
            }
        }

        public bool HandlesMessage(Guid msg)
        {
            lock (m_callbacks)
            {
                return m_callbacks.ContainsKey(msg);
            }
        }

        public object Invoke(object sender, Guid msg, object param)
        {
            if (!HandlesMessage(msg))
                return null;

            return Invoke(new object[] { sender, msg, param });
        }

        public AsyncMessageResult BeginInvoke(object sender, Guid msg, object param)
        {
            // Check to make sure the thread is still alive or this method gets stuck waiting on an invoke on a thread
            // that is no longer around.
            if (!HandlesMessage(msg))
                return null;
            else
            {
                Task task = Task.Factory.StartNew(() => { Invoke(new object[] { sender, msg, param }); });
                return new AsyncMessageResult(task);
            }

        }

        private object Invoke(object o)
        {
            MessageCallback cb = null;
            object[] items = o as object[];

            lock (m_callbacks)
            {
                if (!m_callbacks.TryGetValue((Guid)items[1], out cb))
                    return null;
            }
            return cb(items[0] as object, (Guid)items[1], items[2] as object);
        }
    }
}