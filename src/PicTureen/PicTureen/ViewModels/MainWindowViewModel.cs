using System.Windows;
using System.Windows.Forms;
using Caliburn.PresentationFramework;
using Interfaces;
using Microsoft.Practices.Unity;
using PicTureen.Services;
using PicTureen.Support;
using PicTureen.ViewModels.Search;

namespace PicTureen.ViewModels
{
    public class MainWindowViewModel : PropertyChangedBase
    {
        private readonly INavigationService _navigationService;
        private readonly IFileScanner _fileScanner;
        private readonly IContextProvider _contextProvider;
        private readonly IUnityContainer _container;
        private readonly IAppState _appState;
        private readonly ISettingsService _settingsService;
        private readonly int _menuHeight = (int) SystemParameters.PrimaryScreenHeight/21;

        public int MenuHeight
        {
            get { return _menuHeight; }
        }

        public DelegateCommand ChooseDirectoryCommand { get; set; }

        public MainWindowViewModel(INavigationService navigationService, IFileScanner fileScanner, IContextProvider contextProvider, IUnityContainer container, IAppState appState, ISettingsService settingsService)
        {
            _navigationService = navigationService;
            _fileScanner = fileScanner;
            _contextProvider = contextProvider;
            _container = container;
            _appState = appState;
            _settingsService = settingsService;

            CreateCommands();
            InitializeViews();
            UpdateView();

            
        }

        private void InitializeViews()
        {
            ImagePanelViewModel = _container.Resolve<ImagePanelViewModel>();
            TreeViewModel = _container.Resolve<TreeViewModel>();
            PropertiesViewModel = _container.Resolve<PropertiesViewModel>();
            SearchViewModel = _container.Resolve<SearchViewModel>();
        }

        public TreeViewModel TreeViewModel { get; set; }

        public PropertiesViewModel PropertiesViewModel { get; set; }

        public ImagePanelViewModel ImagePanelViewModel { get; set; }
        public SearchViewModel SearchViewModel { get; set; }

        private void UpdateView()
        {
            

        }

        private void CreateCommands()
        {
            ChooseDirectoryCommand = new DelegateCommand(ChooseDirectory);
        }

        private async void ChooseDirectory()
        {
            using (var dialog = new FolderBrowserDialog())
            {
                var dialogResult = dialog.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    _settingsService.ImagesDirectory = dialog.SelectedPath;
                    var files = await _fileScanner.ScanDirectory(dialog.SelectedPath);
                    await _fileScanner.UpdateDatabase(_contextProvider.GetDbContext(), files);
                    _appState.NotifyDbChanged();
                }
            }
        }
    }
}