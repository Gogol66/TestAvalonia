using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Styling;
using Avalonia.Themes.Fluent;
namespace TestAvalonia
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.AttachDevTools();
            BuildUI();
            Content = new ClockControl();
        }

        private void BuildUI()
        {

            var mainPanel = new StackPanel
            {
                Margin = new Thickness(10)
            };

            var titleText = new TextBlock
            {
                Text = "Hello, Avalonia!",
                Margin = new Thickness(10),
                FontSize = 24
            };
            mainPanel.Children.Add(titleText);


            var centerButton = new Button
            {
                Content = "Click Me",
                Margin = new Thickness(10),
                HorizontalAlignment = HorizontalAlignment.Center
            };
            mainPanel.Children.Add(centerButton);


            var textBox = new TextBox
            {
                Margin = new Thickness(10),
                Width = 200,
                Watermark = "Type here..."
            };
            mainPanel.Children.Add(textBox);


            var exitButton = new Button
            {
                Content = "Exit",
                Margin = new Thickness(10),
                HorizontalAlignment = HorizontalAlignment.Right
            };
            exitButton.Click += (sender, e) => Close();
            mainPanel.Children.Add(exitButton);


            Content = mainPanel;

            this.Width = 400;
            this.Height = 300;
            this.Styles.Add(new FluentTheme());
        }
    }
}