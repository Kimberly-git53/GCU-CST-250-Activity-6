using Microsoft.VisualBasic.Devices;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FileIO
{
    public class RangeTrackBar : Control // extends Control which will put this item in the Toolbox as a UI item. 
    {
        // Declare the properties for the RangeTrackBar control. These properties will be used to set the values of the track bar.
        private int lowerValue = 10;
        private int upperValue = 90;
        private int minValue = 0;
        private int maxValue = 100;
        private int tickFrequency = 10;

        public Color LowerColor { get; set; } = Color.DarkRed;
        public Color UpperColor { get; set; } = Color.Teal;
        public Color TrackColor { get; set; } = Color.LightGray;
        public Color TickColor { get; set; } = Color.LightGray;
        public Color LabelColor { get; set; } = Color.Gray;

        private Rectangle lowerThumb;
        private Rectangle upperThumb;

        private bool draggingLower;
        private bool draggingUpper;

        // Declare the event using EventHandler 
        // this allows the event to be handled externally from a method in Form1.cs 
        public event EventHandler MouseUp;

        // custom setter for TickFrequency. Ensures that the value is always positive. Makes the property visible in the Properties window 
        public int TickFrequency
        {
            get => tickFrequency;
            set
            {
                if (value > 0) // Ensuring that TickFrequency is always a positive value 
                {
                    tickFrequency = value;
                    if (tickFrequency > maxValue)
                        tickFrequency = maxValue;
                    Invalidate(); // Redraw the control to reflect the change
                }
            }
        } 

        // custom setter for MinValue. Ensures that the value is always positive. Makes the property visible in the Properties window 
        public int MinValue
        {
            get => minValue;
            set
            {
                minValue = value;
                if (minValue > lowerValue)
                {
                    lowerValue = minValue;
                }
                if (minValue > upperValue)
                {
                    upperValue = minValue;
                }
                // Redraw the control to reflect the change. Invalidate() forces the control to be redrawn 
                Invalidate();
            }
        }

        // custom setter for MaxValue. Ensures that the value is always positive. Makes the property visible in the Properties window 
        public int MaxValue
        {
            get => maxValue;
            set
            {
                maxValue = value;
                // Ensure that the maxValue is always greater than the lowerValue and upperValue
                if (maxValue < lowerValue)
                {
                    lowerValue = maxValue;
                }
                if (maxValue < upperValue)
                {
                    upperValue = maxValue;
                }
                Invalidate();
            }
        }

        // custom setter for LowerValue. Ensures that the value is always positive. Makes the property visible in the Properties window 
        public int LowerValue
        {
            get => lowerValue;
            set
            {
                // Ensure there's at least a difference of 1 
                int newVal = Math.Min(Math.Max(value, minValue), upperValue - 1);
                // Ensure that the lowerValue is always less than the upperValue
                if (lowerValue != newVal)
                {
                    lowerValue = newVal;
                    UpdateThumbPositions();
                    Invalidate();
                }
            }
        }

        // custom setter for UpperValue. Ensures that the value is always positive. Makes the property visible in the Properties window 
        public int UpperValue
        {
            get => upperValue;
            set
            {   
                // Ensure there's at least a difference of 1
                int newVal = Math.Max(Math.Min(value, maxValue), lowerValue + 1);
                // Ensure that the upperValue is always greater than the lowerValue
                if (upperValue != newVal)
                {
                    upperValue = newVal;
                    UpdateThumbPositions();
                    Invalidate();
                }
            }
        }
        // Constructor for the RangeTrackBar class. This constructor initializes the control and sets its properties.
        public RangeTrackBar()
        {
            DoubleBuffered = true;
            MinimumSize = new Size(100, 50);
            UpdateThumbPositions();
        }
        // this method is called when the control is first displayed. It sets the size of the control and the position of the thumbs
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            draggingLower = false;
            draggingUpper = false;

            // Manually invoke the OnClick event after mouse up 
            OnClick(new EventArgs());

            // Trigger an event that can be handled externally, if needed 
            MouseUp?.Invoke(this, e);
        }

        // this method draws the control. It is called when the control is first displayed and whenever it needs to be redrawn 
        // the control has two polygons that represent the thumbs. The thumbs are drawn using the DrawThumb method  
        protected override void OnPaint(PaintEventArgs e)
        {
            // Call the base class implementation before proceeding with the custom drawing code 
            base.OnPaint(e);
            int trackPadding = 10; // Padding on each side of the track 
            Rectangle trackRect = new Rectangle(trackPadding, 5, Width - 2 * trackPadding, 1);

            using (Brush trackBrush = new SolidBrush(TrackColor))
            using (Pen tickPen = new Pen(TickColor))
            using (Brush lowerBrush = new SolidBrush(LowerColor))
            using (Brush upperBrush = new SolidBrush(UpperColor))
            using (Brush labelBrush = new SolidBrush(LabelColor))
            {
                e.Graphics.FillRectangle(trackBrush, trackRect);
                DrawThumb(e.Graphics, lowerThumb, lowerBrush);
                DrawThumb(e.Graphics, upperThumb, upperBrush);
                DrawTicks(e.Graphics, tickPen, trackRect);
                DrawLabels(e.Graphics, labelBrush);
            }
        }
        // this method draws the thumbs. The thumbs are drawn as polygons with five points. The color of the thumbs is set using the LowerColor and UpperColor properties
        private void DrawThumb(Graphics g, Rectangle thumbRect, Brush color)
        {
            // set of points defines the shape of the thumb. The thumb is drawn as a polygon with five points 
            Point[] points = new Point[]
            {
                new Point(thumbRect.X, thumbRect.Y),
                new Point(thumbRect.X + thumbRect.Width, thumbRect.Y),
                new Point(thumbRect.X + thumbRect.Width, thumbRect.Y + thumbRect.Height),
                new Point(thumbRect.X + thumbRect.Width / 2, thumbRect.Y + thumbRect.Height + 10),
                new Point(thumbRect.X, thumbRect.Y + thumbRect.Height)
            };
            g.FillPolygon(color, points);
        }
        // this method draws the ticks on the track. The ticks are drawn using the DrawTicks method. The color of the ticks is set using the TickColor property
        private void DrawTicks(Graphics g, Pen tickPen, Rectangle trackRect)
        {
            int range = maxValue - minValue;
            // the range is the difference between the maximum and minimum values. The ticks are drawn at intervals of tickFrequency
            if (range <= 0) return;

            int tickNum = range / tickFrequency;
            // the number of ticks is the range divided by the tick frequency. The number of ticks is used to determine the position of the ticks on the track
            if (tickNum == 0) tickNum = 1;
            // the number of ticks is at least 1. The ticks are drawn at intervals of tickFrequency
            for (int i = 0; i <= tickNum; i++)
            {
                int x = trackRect.Left + (int)(i * (trackRect.Width / (double)tickNum));
                g.DrawLine(tickPen, x, trackRect.Top + 8, x, trackRect.Bottom + 10);
            }
        }

        // two numbers are drawn below the thumbs. The numbers represent the current value of the lower and upper thumbs 
        private void DrawLabels(Graphics g, Brush labelBrush)
        {
            // the labels are drawn using the DrawString method. The labels are drawn below the thumbs
            string lowerLabel = LowerValue.ToString();
            string upperLabel = UpperValue.ToString();
            // the size of the labels is determined using the MeasureString method. The size of the labels is used to determine the position of the labels
            SizeF lowerSize = g.MeasureString(lowerLabel, Font);
            SizeF upperSize = g.MeasureString(upperLabel, Font);
            // the position of the labels is determined using the MeasureString method. The position of the labels is used to determine the position of the labels
            float lowerX = lowerThumb.Left + (lowerThumb.Width / 2) - (lowerSize.Width / 2);
            float upperX = upperThumb.Left + (upperThumb.Width / 2) - (upperSize.Width / 2);
            // the labels are drawn using the DrawString method. The labels are drawn below the thumbs
            g.DrawString(lowerLabel, Font, labelBrush, new PointF(lowerX, lowerThumb.Bottom + 6));
            g.DrawString(upperLabel, Font, labelBrush, new PointF(upperX, upperThumb.Bottom + 6));
        }

        // this method is called when the user clicks on the control. It checks if the user clicked on one of the thumbs or the track 
        protected override void OnMouseDown(MouseEventArgs e)
        {
            // Call the base class implementation before proceeding with the custom mouse handling code
            if (lowerThumb.Contains(e.Location))
            {
                draggingLower = true;
            }
            else if (upperThumb.Contains(e.Location))
            {
                draggingUpper = true;
            }
            else
            {
                // mouse is down on the track, so move the closest thumb 
                int value = PositionToValue(e.X, 10);
                if (value < lowerValue)
                {
                    LowerValue = value;
                    draggingLower = true;
                }
                else if (value > upperValue)
                {
                    UpperValue = value;
                    draggingUpper = true;
                }
            }
            // if both sliders are at 0, then set the upper slider to be dragged 
            if (upperValue == 0 && draggingLower == true)
            {
                draggingLower = false;
                draggingUpper = true;
            }
        }


        // this method is called when the user moves the mouse. It updates the position of the thumb that is being dragged 
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            // Call the base class implementation before proceeding with the custom mouse handling code
            if (!draggingLower && !draggingUpper) return;

            int value = PositionToValue(e.X, 10);
            if (draggingLower)
            {
                LowerValue = value;  // Use property to trigger ValueChanged 
            }
            else if (draggingUpper)
            {
                UpperValue = value;  // Use property to trigger ValueChanged 
            }
        }

        // this method is called when the control is resized. It updates the position of the thumbs 
        private void SetLowerValue(int value)
        {
            lowerValue = Math.Min(Math.Max(value, minValue), upperValue);
            UpdateThumbPositions();
            Invalidate();
        }

        // this method is called when the control is resized. It updates the position of the thumbs 
        private void SetUpperValue(int value)
        {
            upperValue = Math.Max(Math.Min(value, maxValue), lowerValue);
            UpdateThumbPositions();
            Invalidate();
        }

        // this method is used to determine the location of the mouse pointer in relation to the track. It returns the value that corresponds to the position of the 
        // mouse pointer
        private int PositionToValue(int position, int padding)
        {
            double scale = (Width - 2 * padding) / (double)(maxValue - minValue);
            return Math.Max(minValue, Math.Min(maxValue, (int)((position - padding) / scale) + minValue));
        }
        // this method is used to update the position of the thumbs. It calculates the position of the thumbs based on the current values of the lower and upper thumbs
        private void UpdateThumbPositions()
        {
            int padding = 10;
            lowerThumb = new Rectangle(ValueToPosition(lowerValue, padding) - 5, 0, 10, 10);
            upperThumb = new Rectangle(ValueToPosition(upperValue, padding) - 5, 0, 10, 10);
        }
        // this method is used to determine the position of the thumbs. It returns the position that corresponds to the value of the thumbs
        private int ValueToPosition(int value, int padding)
        {
            double scale = (Width - 2 * padding) / (double)(maxValue - minValue);
            return (int)(padding + (value - minValue) * scale);
        }
    }
}
