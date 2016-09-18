using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace UniGuy.Windows
{
    #region ExceptionHelper
    public static class ExceptionHelper2
    {
        public static void HandleFatalException(Exception ex)
        {
            // If not executing on the main UI thread, then dispatch the exception back to the main UI thread.  Then,
            // reraise the exception on the main UI thread and handle it from the handler
            // in the Application object's DispatcherUnhandledException event.
            if (Application.Current.Dispatcher.Thread.ManagedThreadId != Thread.CurrentThread.ManagedThreadId)
            {
                Application.Current.Dispatcher.Invoke(
                    DispatcherPriority.Send,
                    (DispatcherOperationCallback)(arg =>
                    {
                        throw new FatalException(ex);
                    }),
                        null);
            }
            else
            {
                throw new FatalException(ex);
            }
        }
    }

    /// <summary>
    /// This class is used to encapsulate inner exceptions so that we do not lose
    /// call stack information when the exception is being rethrown to the Dispatcher thread.
    /// </summary>
    class FatalException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FatalException"/> class.
        /// </summary>
        /// <param name="innerException">The inner exception.</param>
        public FatalException(Exception innerException)
            : base("Fatal Exception", innerException)
        {
        }
    }
    #endregion
}
