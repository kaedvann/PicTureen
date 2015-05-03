using Database;
using Microsoft.Practices.Unity;
using PicTureen.Annotations;
using PicTureen.ViewModels;
using PicTureen.Views;

namespace PicTureen.Services
{
    [UsedImplicitly]
    public class NavigationService : INavigationService
    {
        private readonly IUnityContainer _container;
        private MainWindow _mainWindow;

        private MainWindow MainWindow
        {
            get
            {
                return _mainWindow ?? (_mainWindow = new MainWindow
                {
                    DataContext = _container.Resolve<MainWindowViewModel>()
                });
            }
            set { _mainWindow = value; }
        }

        public NavigationService(IUnityContainer container)
        {
            _container = container;
        }

        public void ShowMainWindow()
        {
            MainWindow.Show();
        }

        public void ShowImage(Image image)
        {
            var vm = _container.Resolve<FullImageViewModel>();
            vm.Image = image;
            var window = new FullImageView
            {
                DataContext = vm,
                Owner = MainWindow
            };
            window.ShowDialog();
        }
    }
}