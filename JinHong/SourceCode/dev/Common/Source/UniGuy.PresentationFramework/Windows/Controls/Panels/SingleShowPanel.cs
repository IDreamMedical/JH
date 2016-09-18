using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace UniGuy.Controls.Panels
{
	public enum FlipDirection
	{
		LeftToRight,
		RightToLeft,
		TopToBottom,
		BottomToTop
	}
	public class SingleShowPanel : Panel
	{
		public static readonly DependencyProperty OldElementIndexProperty
			= DependencyProperty.Register("OldElementIndex", typeof(int), typeof(SingleShowPanel),
				new FrameworkPropertyMetadata(0));
		public static readonly DependencyProperty CurrentElementIndexProperty
			= DependencyProperty.Register("CurrentElementIndex", typeof(int), typeof(SingleShowPanel),
				new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(OnCurrentElementSet)));
		public static readonly DependencyProperty FlipRangeProperty
			= DependencyProperty.Register("FlipRange", typeof(double), typeof(SingleShowPanel),
				new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.AffectsMeasure));
		public static readonly DependencyProperty FlipDirectionProperty
			= DependencyProperty.Register("FlipDirection", typeof(FlipDirection), typeof(SingleShowPanel),
						new FrameworkPropertyMetadata(FlipDirection.RightToLeft, FrameworkPropertyMetadataOptions.AffectsMeasure));
		
		public int OldElementIndex
		{
			set{SetValue(OldElementIndexProperty, value<0?0:(value>InternalChildren.Count?InternalChildren.Count:value));}
			get{return (int)GetValue(OldElementIndexProperty);}
		}
		public int CurrentElementIndex
		{
			set{SetValue(CurrentElementIndexProperty, value<0?0:(value>InternalChildren.Count?InternalChildren.Count:value));}
			get{return (int)GetValue(CurrentElementIndexProperty);}
		}
		public double FlipRange
		{
			set{SetValue(FlipRangeProperty, value<0?0:(value>1?1:value));}
			get{return (double)GetValue(FlipRangeProperty);}
		}
		public FlipDirection FlipDirection
		{
			set{SetValue(FlipDirectionProperty, value);}
			get{return (FlipDirection)GetValue(FlipDirectionProperty);}
		}
		
		//	Override of MeasureOverride.
		protected override Size MeasureOverride(Size sizeAvailable)
		{
			if(InternalChildren.Count>0)
			{
				UIElement old = InternalChildren[OldElementIndex];
				UIElement current = InternalChildren[CurrentElementIndex];
				
				old.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
				current.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
				
				return(new Size(Math.Max(old.DesiredSize.Width, current.DesiredSize.Width), Math.Max(old.DesiredSize.Height, current.DesiredSize.Height)));
			}
			return sizeAvailable;
		}
		
		//	Override of ArrangeOverride.
		protected override Size ArrangeOverride(Size sizeFinal)
		{
			if(InternalChildren.Count>0)
			{
				UIElement old = InternalChildren[OldElementIndex];
				UIElement current = InternalChildren[CurrentElementIndex];

                double oldLeft = 0, oldTop = 0, currentLeft = 0, currentTop = 0;
				switch(FlipDirection)
				{
					case FlipDirection.LeftToRight:
						oldLeft = sizeFinal.Width * FlipRange;
                        currentLeft = sizeFinal.Width + oldLeft;
						break;
					case FlipDirection.RightToLeft:
						oldLeft=-sizeFinal.Width * FlipRange;
                        currentLeft = sizeFinal.Width + oldLeft;
						break;
					case FlipDirection.TopToBottom:
						oldTop = sizeFinal.Height * FlipRange;
                        currentTop = sizeFinal.Height + oldTop;
						break;
					case FlipDirection.BottomToTop:
						oldTop = -sizeFinal.Height * FlipRange;
                        currentTop = sizeFinal.Height + oldTop;
						break;
				}
				old.Arrange(new Rect(oldLeft, oldTop, sizeFinal.Width, sizeFinal.Height));
				current.Arrange(new Rect(currentLeft, currentTop, sizeFinal.Width, sizeFinal.Height));
			}
			return sizeFinal;
		}
		
		private static void OnCurrentElementSet(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			SingleShowPanel ssp = (SingleShowPanel)d;
			ssp.OldElementIndex = (int)e.OldValue;
		}
	}
}