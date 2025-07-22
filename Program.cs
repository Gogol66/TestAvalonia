using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using TestAvalonia;

namespace TestAvalonia
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            BuildAvaloniaApp()
                .UsePlatformDetect()
                .With(new X11PlatformOptions
                {
                    RenderingMode = new[] { X11RenderingMode.Software }, // Force CPU rendering
                    UseDBusMenu = true, // Better Linux integration
                    EnableMultiTouch = false // Disable unused features
                })
                .LogToTrace()
                .StartWithClassicDesktopLifetime(args);
        }

        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>();
    }
}