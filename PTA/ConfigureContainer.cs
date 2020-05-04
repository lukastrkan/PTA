using Autofac;
using PTA.Services;

namespace PTA
{
    public static class ConfigureContainer
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ApplicationDbContext>().SingleInstance();

            builder.RegisterType<MainWindow>();

            builder.RegisterType<ImageService>().As<IImageService>();

            builder.RegisterType<GameService>().As<IGameService>();

            return builder.Build();
        }
    }
}
