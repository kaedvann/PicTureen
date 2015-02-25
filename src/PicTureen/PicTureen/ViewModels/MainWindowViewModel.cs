using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using PicTureen.Annotations;

namespace PicTureen.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int _menuHeight = (int) SystemParameters.PrimaryScreenHeight/21;
        public int MenuHeight
        {
            get { return _menuHeight; }
        }
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
