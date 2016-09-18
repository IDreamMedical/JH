using System;
using System.Collections.Generic;
using System.Text;

namespace UniGuy.Core.Common
{
    public static class Guard
    {
        public static void CheckNull(object parameter, string message)
        {
            if (parameter == null)
                throw new ArgumentNullException(message);
        }

        public static void CheckNull(object parameter)
        {
            if (parameter == null)
                throw new ArgumentNullException();
        }

        public static void CheckNullOrEmpty(string parameter, string message)
        {
            Guard.CheckNull(parameter, message);

            if (parameter.Length == 0)
                throw new ArgumentException(message);
        }

        public static void CheckNullOrTrimEmpty(string parameter, string message)
        {
            Guard.CheckNull(parameter, message);

            if (parameter.Trim().Length == 0)
                throw new ArgumentException(message);
        }

        #region Wj  //  201112131510
        public static void CheckDelegateNull(Delegate parameter, string message)
        {
            Guard.CheckNull(parameter, message);
        }
        public static void CheckDelegateNull(Delegate parameter)
        {
            Guard.CheckDelegateNull(parameter, "Delegate instance can not be null.");
        }
        #endregion //   Wj
    }
}
