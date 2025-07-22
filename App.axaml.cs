using Avalonia;

namespace TestAvalonia
{
    public class App : Application
    {
        public override void OnFrameworkInitializationCompleted()
        {
            base.OnFrameworkInitializationCompleted();
            new MainWindow().Show();
        }
    }
}