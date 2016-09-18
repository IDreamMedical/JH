using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace UniGuy.Controls.Panels
{
	public class RadialPanel : Panel
	{
		private double radius;
		private double innerEdgeFromCenter;
		private double outerEdgeFromCenter;
		private double dAngleEach;
		private Size sizeLargest;
		
		//	Dependency Property.
		public static readonly DependencyProperty ChildRotateAngleProperty
			= DependencyProperty.Register("ChildRotateAngle", typeof(double), typeof(RadialPanel),
				new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure));
		public static readonly DependencyProperty ShowPieLinesProperty
			= DependencyProperty.Register("ShowPieLines", typeof(bool), typeof(RadialPanel),
                new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnPieLinesNeedChange)));
		public static readonly DependencyProperty StartAngleProperty
			= DependencyProperty.Register("StartAngle", typeof(double), typeof(RadialPanel),
                new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsArrange, new PropertyChangedCallback(OnPieLinesNeedChange)));
		public static readonly DependencyProperty SweepAngleProperty
			= DependencyProperty.Register("SweepAngle", typeof(double), typeof(RadialPanel),
				new FrameworkPropertyMetadata(360.0, FrameworkPropertyMetadataOptions.AffectsArrange, OnPieLinesNeedChange));
		public static readonly DependencyProperty IsClockWiseProperty
			= DependencyProperty.Register("IsClockWise", typeof(bool), typeof(RadialPanel),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsArrange, OnPieLinesNeedChange));
		
		public double ChildRotateAngle
		{
			set{SetValue(ChildRotateAngleProperty, AdjustAngle(value));}
			get{return (double)GetValue(ChildRotateAngleProperty);}
		}
		public bool ShowPieLines
		{
			set{SetValue(ShowPieLinesProperty, value);}
			get{return (bool)GetValue(ShowPieLinesProperty);}
		}
		public double StartAngle
		{
			set{SetValue(StartAngleProperty, AdjustAngle(value));}
			get{return (double)GetValue(StartAngleProperty);}
		}
		public double SweepAngle
		{
			set{SetValue(SweepAngleProperty, value);}
            get { return (double)GetValue(SweepAngleProperty); }
		}
		public bool IsClockWise
		{
			set{SetValue(IsClockWiseProperty, value);}
			get{return (bool)GetValue(IsClockWiseProperty);}
		}
		
		private double AdjustAngle(double angle)
		{
			int i = (int)angle / 360;
			double r = angle - i * 360;
			if(r<=0)
				r += 360;
			return r;
		}
		
		//	Override of MeasureOverride.
		protected override Size MeasureOverride(Size sizeAvailable)
		{
			if(InternalChildren.Count == 0)
				return new Size(0, 0);
			//	angle for each child
            dAngleEach = (SweepAngle > 0 ? (SweepAngle) : (SweepAngle + 360)) / InternalChildren.Count;
			// size of largest child
			sizeLargest = new Size(0, 0);
			
			foreach(UIElement child in InternalChildren)
			{
				//	Call Measure for each child.
				child.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
				//	... and then examine DesiredSize property of child.
				double x = child.DesiredSize.Width;
				double y = child.DesiredSize.Height;
				double thita = 2*Math.PI*ChildRotateAngle/360;
				sizeLargest.Width = Math.Max(sizeLargest.Width, x*Math.Abs(Math.Cos(thita)) + y*Math.Abs(Math.Sin(thita)));
				sizeLargest.Height = Math.Max(sizeLargest.Height, x*Math.Abs(Math.Sin(thita)) + y*Math.Abs(Math.Cos(thita)));
			}
			//	Calculate the distance from the center to element edges.
			innerEdgeFromCenter = sizeLargest.Height / 2 /Math.Tan(Math.PI * dAngleEach / 360);
			outerEdgeFromCenter = innerEdgeFromCenter + sizeLargest.Width;
            // Calculate the radius of the circle based on the largest child.
            radius = Math.Sqrt(Math.Pow(sizeLargest.Height / 2, 2) +
                Math.Pow(outerEdgeFromCenter, 2));
				
			return new Size(2 * radius, 2* radius);
		}
        
		//	Override of ArrangeOverride.
		protected override Size ArrangeOverride(Size sizeFinal)
		{
			double angleChild = StartAngle;
			Point ptCenter = new Point(sizeFinal.Width/2, sizeFinal.Height/2);
			double multiplier2 = Math.Min(sizeFinal.Width / (2 * radius), sizeFinal.Height/(2*radius));
			
			foreach(UIElement child in InternalChildren)
			{
				double thita = 2*Math.PI*ChildRotateAngle/360;
                double x = child.DesiredSize.Width * Math.Abs(Math.Cos(thita)) + child.DesiredSize.Height * Math.Abs(Math.Sin(thita));
                double y = child.DesiredSize.Width * Math.Abs(Math.Sin(thita)) + child.DesiredSize.Height * Math.Abs(Math.Cos(thita));
                double multiplier1 = Math.Min(sizeLargest.Width / Math.Max(sizeLargest.Width, x), sizeLargest.Height / Math.Max(sizeLargest.Height, y));
                double dW = child.DesiredSize.Width / multiplier1;
                double dH = child.DesiredSize.Height / multiplier1;
                child.RenderTransform = Transform.Identity;
				// Position the child at the right.
                child.Arrange(
                    new Rect(ptCenter.X + multiplier2 * (innerEdgeFromCenter + outerEdgeFromCenter - dW) / 2,
                        ptCenter.Y - multiplier2 * dH / 2,
                        multiplier2 * dW,
                        multiplier2 * dH));
				//	Rotate the child around the center (relative to the child).
				Point pt = TranslatePoint(ptCenter, child);
                TransformGroup tg = new TransformGroup();
                tg.Children.Add(new RotateTransform(ChildRotateAngle, multiplier2 * dW / 2, multiplier2 * dH / 2));
				tg.Children.Add(new RotateTransform(-angleChild, pt.X, pt.Y));
                child.RenderTransform = tg;
				//	Increment the angle.
				if(IsClockWise)
					angleChild-=dAngleEach;
				else
					angleChild+=dAngleEach;
			}
			return sizeFinal;
		}
		//	Override OnRender to display optional pie lines.
		protected override void OnRender(DrawingContext dc)
		{
			base.OnRender(dc);
			
			if(ShowPieLines)
			{
				Point ptCenter = new Point(RenderSize.Width/2, RenderSize.Height/2);
				double multiplier = Math.Min(RenderSize.Width / (2 * radius), RenderSize.Height / (2 * radius));
                Pen pen = new Pen(SystemColors.WindowTextBrush, 1);
                pen.DashStyle = DashStyles.Dash;

                // Display circle.
                dc.DrawEllipse(null, pen, ptCenter, multiplier * radius, multiplier * radius);
                // Initialize angle.
                double angle = StartAngle - dAngleEach/2;

                // Loop through each child to draw radial lines from center.
                foreach (UIElement child in InternalChildren)
                {
                    dc.DrawLine(pen, ptCenter,
                        new Point(ptCenter.X + multiplier * radius *
                                    Math.Cos(2 * Math.PI * angle / 360),
                                  ptCenter.Y - multiplier * radius *
                                    Math.Sin(2 * Math.PI * angle / 360)));
					if(IsClockWise)
                    	angle -= dAngleEach;
					else
						angle +=dAngleEach;
                }
                if(dAngleEach*InternalChildren.Count!=360)
                    dc.DrawLine(pen, ptCenter,
                        new Point(ptCenter.X + multiplier * radius *
                                    Math.Cos(2 * Math.PI * angle / 360),
                                  ptCenter.Y - multiplier * radius *
                                    Math.Sin(2 * Math.PI * angle / 360)));
			}
		}
		
		private static void OnPieLinesNeedChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			RadialPanel rp = (RadialPanel)d;
			rp.InvalidateVisual();
		}
	}
}