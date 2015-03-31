using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using PicTureen.Annotations;
using PicTureen.Services;

namespace PicTureen.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly INavigationService _navigationService;
        private readonly IFileScanner _fileScanner;
        private readonly int _menuHeight = (int) SystemParameters.PrimaryScreenHeight/21;

        public int MenuHeight
        {
            get { return _menuHeight; }
        }

        public MainWindowViewModel(INavigationService navigationService, IFileScanner fileScanner)
        {
            _navigationService = navigationService;
            _fileScanner = fileScanner;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}