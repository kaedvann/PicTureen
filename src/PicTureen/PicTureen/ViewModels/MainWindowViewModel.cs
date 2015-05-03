using System.Windows;
using System.Windows.Forms;
using Caliburn.PresentationFramework;
using PicTureen.Services;
using PicTureen.Support;

namespace PicTureen.ViewModels
{
    public class MainWindowViewModel : PropertyChangedBase
    {
        private readonly INavigationService _navigationService;
        private readonly IFileScanner _fileScanner;
        private readonly IContextProvider _contextProvider;
        private readonly int _menuHeight = (int) SystemParameters.PrimaryScreenHeight/21;

        public int MenuHeight
        {
            get { return _menuHeight; }
        }

        public DelegateCommand ChooseDirectoryCommand { get; set; }

        public MainWindowViewModel(INavigationService navigationService, IFileScanner fileScanner, IContextProvider contextProvider)
        {
            _navigationService = navigationService;
            _fileScanner = fileScanner;
            _contextProvider = contextProvider;

            CreateCommands();
            UpdateView();

            
        }

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
                    var files = await _fileScanner.ScanDirectory(dialog.SelectedPath);
                }
            }
        }
    }
}