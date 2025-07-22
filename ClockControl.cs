using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Threading;
using System;

namespace TestAvalonia
{
    public class ClockControl : Control
    {
        private const int ClockSize = 300;
        private const int Center = ClockSize / 2;
        private const int FaceRadius = ClockSize / 2 - 10;

        private readonly Grid _container;
        private readonly Ellipse _clockFace;
        private readonly Line _hourHand;
        private readonly Line _minuteHand;
        private readonly Line _secondHand;
        private DispatcherTimer _timer;

        public ClockControl()
        {
            // Initialize container
            _container = new Grid
            {
                Width = ClockSize,
                Height = ClockSize,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            // Initialize clock components
            _clockFace = CreateClockFace();
            _hourHand = CreateHand(Brushes.Black, 6);
            _minuteHand = CreateHand(Brushes.Black, 4);
            _secondHand = CreateHand(Brushes.Red, 2);

            // Add components to container
            _container.Children.Add(_clockFace);
            _container.Children.Add(_hourHand);
            _container.Children.Add(_minuteHand);
            _container.Children.Add(_secondHand);

            // Add hour markers
            for (int i = 0; i < 12; i++)
            {
                _container.Children.Add(CreateHourMarker(i));
            }

            // Add container to visual tree
            VisualChildren.Add(_container);

            StartTimer();
        }

        private Ellipse CreateClockFace()
        {
            return new Ellipse
            {
                Width = ClockSize,
                Height = ClockSize,
                Stroke = Brushes.SteelBlue,
                StrokeThickness = 4,
                Fill = new RadialGradientBrush
                {
                    GradientStops =
                    {
                        new GradientStop(Colors.White, 0),
                        new GradientStop(Colors.LightBlue, 1)
                    }
                }
            };
        }

        private Line CreateHand(IBrush brush, double thickness)
        {
            return new Line
            {
                Stroke = brush,
                StrokeThickness = thickness,
                StrokeLineCap = PenLineCap.Round,
                StartPoint = new Point(Center, Center)
            };
        }

        private TextBlock CreateHourMarker(int hour)
        {
            double angle = (hour * 30 - 90) * Math.PI / 180;
            double x = Center + (FaceRadius - 20) * Math.Cos(angle) - 10;
            double y = Center + (FaceRadius - 20) * Math.Sin(angle) - 10;

            return new TextBlock
            {
                Text = (hour == 0 ? "12" : hour.ToString()),
                FontSize = 16,
                FontWeight = FontWeight.Bold,
                Foreground = Brushes.DarkBlue,
                RenderTransform = new TranslateTransform(x, y)
            };
        }

        private void StartTimer()
        {
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(50)
            };
            _timer.Tick += (s, e) => UpdateClock();
            _timer.Start();
            UpdateClock();
        }

        private void UpdateClock()
        {
            var now = DateTime.Now;
            double seconds = now.Second + now.Millisecond / 1000.0;
            double minutes = now.Minute + seconds / 60.0;
            double hours = now.Hour % 12 + minutes / 60.0;

            UpdateHand(_secondHand, seconds * 6, FaceRadius - 20);
            UpdateHand(_minuteHand, minutes * 6, FaceRadius - 30);
            UpdateHand(_hourHand, hours * 30, FaceRadius - 50);
        }

        private void UpdateHand(Line hand, double angle, double length)
        {
            double radians = (angle - 90) * Math.PI / 180;
            hand.EndPoint = new Point(
                Center + length * Math.Cos(radians),
                Center + length * Math.Sin(radians)
            );
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            return new Size(ClockSize, ClockSize);
        }
    }
}