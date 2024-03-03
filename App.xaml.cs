using System.Windows;

namespace hashViewer
{
    public partial class App : Application
    {
        void App_Startup(object sender, StartupEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            mainWindow.setFile(e.Args.Length == 0 ? null : e.Args[0]);
        }
    }
}