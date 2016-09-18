using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniGuy.Core.Message
{
    public enum MessageRank
    {
        Info = 0,
        Warning = 1,
        Error = 2
    }

    public class Message
    {
        public DateTime Time { get; internal set; }
        public object Source { get; internal set; }
        public MessageRank Rank { get; internal set; }
        public object Content { get; internal set; }
        public string Description { get; internal set; }

        public Message(DateTime time, object content)
        {
            this.Time = time;
            this.Content = content;
        }

        public Message(object content):this(DateTime.Now, content)
        {
        }

        public Message(object source, object content)
            : this(content)
        {
            this.Source = source;
        }

        public Message(object content, MessageRank rank = MessageRank.Info):this(content)
        {
            this.Rank = rank;
        }

        public Message(object source, object content, MessageRank rank = MessageRank.Info)
            : this(content, rank)
        {
            this.Source = source;
        }

        public Message(DateTime time, object content, MessageRank rank = MessageRank.Info)
            : this(time, content)
        {
            this.Rank = rank;
        }

        public Message(object source, DateTime time, object content, MessageRank rank = MessageRank.Info)
            : this(time, content, rank)
        {
            this.Source = source;
        }
    }

    public class MessageOutputEventArgs : EventArgs
    {
        public Message Message { get; set; }

        public MessageOutputEventArgs(Message message)
            : base()
        {
            if (message == null)
                throw new ArgumentNullException("message");

            this.Message = message;
        }
    }

    public delegate void MessageOutputEventHandler(object sender, MessageOutputEventArgs args);

    /// <summary>
    /// 用于管理程序产生的Output消息的管理类; 非静态徐创建实例;
    /// 1. 通过Output, OutputInfo, OutputWarning, OutputError等方法请求输出信息;
    /// 2. 程序相关输出UI模块捕获对应事件进行处理
    /// </summary>
    public class MessageManager
    {
        public event MessageOutputEventHandler MessageOutput;

        public void OnOutput(Message message)
        {
            if (MessageOutput != null)
                MessageOutput(this, new MessageOutputEventArgs(message));
        }

        public void Output(Message message)
        {
            OnOutput(message);
        }

        public void Output(object content)
        {
            Output(new Message(content));
        }

        public void Output(object source, object content)
        {
            Output(new Message(source, content));
        }

        public void Output(object content, MessageRank rank = MessageRank.Info)
        {
            Output(new Message(content, rank));
        }

        public void Output(object source, object content, MessageRank rank = MessageRank.Info)
        {
            Output(new Message(source, content, rank));
        }

        public void Output(object content, string description, MessageRank rank = MessageRank.Info)
        {
            Output(new Message(content, rank) { Description = description });
        }

        public void Output(object source, object content, string description, MessageRank rank = MessageRank.Info)
        {
            Output(new Message(source, content, rank) { Description = description });
        }

        public void OutputInfo(object content, string description = null)
        {
            Output(new Message(content, MessageRank.Info));
        }

        public void OutputInfo(object source, object content, string description = null)
        {
            Output(new Message(source, content, MessageRank.Info));
        }

        public void OutputWarning(object content, string description = null)
        {
            Output(new Message(content, MessageRank.Warning));
        }

        public void OutputWarning(object source, object content, string description = null)
        {
            Output(new Message(source, content, MessageRank.Warning));
        }

        public void OutputError(object content, string description = null)
        {
            Output(new Message(content, MessageRank.Error));
        }

        public void OutputError(object source, object content, string description = null)
        {
            Output(new Message(source, content, MessageRank.Error));
        }
    }
}
