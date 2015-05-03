using System.Windows;
using Database;
using Interfaces;
using Microsoft.Practices.Unity;
using PicTureen.Services;

namespace PicTureen
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IUnityContainer _container = new UnityContainer();
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            RegisterDependencies();
            _container.Resolve<INavigationService>().ShowMainWindow();
        }

        private void RegisterDependencies()
        {
            _container.RegisterInstance(_container);
            _container.RegisterType<INavigationService, NavigationService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IFileScanner, FileScanner>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IContextProvider, ContextProvider>(new ContainerControlledLifetimeManager());
            _container.RegisterType<ISettingsService, SettingsService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IDbHelper, DbHelper>(new ContainerControlledLifetimeManager());
        }
    }
}
