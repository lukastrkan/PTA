using Autofac;
using System.Windows;

namespace PTA
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        IContainer Container;
        public App()
        {

        }

        private async void Application_Startup(object sender, StartupEventArgs e)
        {
            Container = ConfigureContainer.Configure();
            using (var scope = Container.BeginLifetimeScope())
            {
                var app = scope.Resolve<MainWindow>();
                var context = scope.Resolve<ApplicationDbContext>();
                await context.Database.EnsureCreatedAsync();
                app.Show();
            }
        }
    }
}
