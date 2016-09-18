using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Markup;

namespace UniGuy.Controls.Markup
{
    public class ArrayTypeExtension : MarkupExtension
    {
        //  数组元素类型
        private Type elmentType;
        //  数组的秩(维数)
        private int rank = 1;

        public Type ElementType 
        {
            get { return elmentType; }
            set { elmentType = value; }
        }
        public int Rank
        {
            get{return rank;}
            set { rank = value; }
        }

        public ArrayTypeExtension() { }
        public ArrayTypeExtension(Type elmentType)
        {
            this.elmentType = elmentType;
        }
        public ArrayTypeExtension(Type elmentType, int rank)
            : this(elmentType)
        {
            this.rank = rank;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (elmentType != null)
                return elmentType.MakeArrayType(rank);
            return null;
        }
    }
}
