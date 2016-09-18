using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.ComponentModel;
using System.Windows.Controls.Primitives;

namespace UniGuy.Controls.Panels
{
    /// <summary>
    /// A uniform grid which supports orientation setting.
    /// BTW, I haven't made support for "FirstColumn" nor for "FirstRow". This functionality seems very odd, anybody know about a good use case?
    /// </summary>
    public class OrientatedUniformGrid:UniformGrid
    {
        private int _columns;   
        private int _rows;   

        public static readonly DependencyProperty OrientationProperty
            = DependencyProperty.Register("Orientation", typeof(Orientation), typeof(OrientatedUniformGrid),
            new FrameworkPropertyMetadata(Orientation.Horizontal, FrameworkPropertyMetadataOptions.AffectsMeasure, 
                new PropertyChangedCallback(OnOrientationChanged)), new ValidateValueCallback(IsValidOrientation));

        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        public OrientatedUniformGrid() { }

        internal static bool IsValidOrientation( object o )   
        {   
            Orientation orientation = (Orientation)o;   
            if( orientation != Orientation.Horizontal )   
            {   
                return ( orientation == Orientation.Vertical );   
            }   
            return true;   
        }  


        private static void OnOrientationChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            //  TODO
        }

        private void UpdateComputedValues()   
        {   
            this._columns = this.Columns;   
            this._rows = this.Rows;   
            if( this.FirstColumn >= this._columns )   
            {   
                this.FirstColumn = 0;   
            }   
  
            if( FirstColumn > 0 )   
                throw new NotImplementedException( "There is no support for seting the FirstColumn (nor the FirstRow)." );   
            if( ( this._rows == 0 ) || ( this._columns == 0 ) )   
            {   
                int num = 0;    // Visible children   
                int num2 = 0;   
                int count = base.InternalChildren.Count;   
                while( num2 < count )   
                {   
                    UIElement element = base.InternalChildren[ num2 ];   
                    if( element.Visibility != Visibility.Collapsed )   
                    {   
                        num++;   
                    }   
                    num2++;   
                }   
                if( num == 0 )   
                {   
                    num = 1;   
                }   
                if( this._rows == 0 )   
                {   
                    if( this._columns > 0 )   
                    {   
                        this._rows = ( ( num + this.FirstColumn ) + ( this._columns - 1 ) ) / this._columns;   
                    }   
                    else  
                    {   
                        this._rows = (int)Math.Sqrt( (double)num );   
                        if( ( this._rows * this._rows ) < num )   
                        {   
                            this._rows++;   
                        }   
                        this._columns = this._rows;   
                    }   
                }   
                else if( this._columns == 0 )   
                {   
                    this._columns = ( num + ( this._rows - 1 ) ) / this._rows;   
                }   
            }   
        }

        protected override Size MeasureOverride(Size constraint)
        {
            if (Orientation == Orientation.Vertical)
                return base.MeasureOverride(constraint);
            else if (Orientation == Orientation.Horizontal)
            {

                this.UpdateComputedValues();
                Size availableSize = new Size(constraint.Width / ((double)this._columns), constraint.Height / ((double)this._rows));
                double width = 0.0;
                double height = 0.0;
                int num3 = 0;
                int count = base.InternalChildren.Count;
                while (num3 < count)
                {
                    UIElement element = base.InternalChildren[num3];
                    element.Measure(availableSize);
                    Size desiredSize = element.DesiredSize;
                    if (width < desiredSize.Width)
                    {
                        width = desiredSize.Width;
                    }
                    if (height < desiredSize.Height)
                    {
                        height = desiredSize.Height;
                    }
                    num3++;
                }
                return new Size(width * this._columns, height * this._rows);
            }

            throw new Exception("Impossible");
        }

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            if (Orientation == Orientation.Vertical)
                return base.ArrangeOverride(arrangeSize);
            else if (Orientation == Orientation.Horizontal)
            {
                Rect finalRect = new Rect(0.0, 0.0, arrangeSize.Width / ((double)this._columns), arrangeSize.Height / ((double)this._rows));
                double height = finalRect.Height;
                double numX = arrangeSize.Height - 1.0;
                finalRect.X += finalRect.Width * this.FirstColumn;
                foreach (UIElement element in base.InternalChildren)
                {
                    element.Arrange(finalRect);
                    if (element.Visibility != Visibility.Collapsed)
                    {
                        finalRect.Y += height;
                        if (finalRect.Y >= numX)
                        {
                            finalRect.X += finalRect.Width;
                            finalRect.Y = 0.0;
                        }
                    }
                }
                return arrangeSize;
            }
            throw new Exception("Impossible");
        }   
    }
}
