using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace UniGuy.Controls.Converters
{
    public class DataHolder
    {
        private string name;
        private object data;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public object Data
        {
            get { return data; }
            set { data = value; }
        }
    }
}
