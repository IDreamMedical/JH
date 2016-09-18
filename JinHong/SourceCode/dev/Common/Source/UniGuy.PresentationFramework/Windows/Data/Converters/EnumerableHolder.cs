using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace UniGuy.Controls.Converters
{
    public class EnumerableHolder
    {
        private string name;
        private IEnumerable items;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public IEnumerable Items
        {
            get { return items; }
            set { items = value; }
        }
    }
}
