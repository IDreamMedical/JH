using System;
using System.Collections.Generic;
using System.Text;

namespace UniGuy.Core.Data
{
    public class SqlException:Exception
    {
        public SqlException() : base() { }
        public SqlException(string msg) : base(msg) { }
    }
}
