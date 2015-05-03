using System;
using PicTureen.ViewModels;

namespace PicTureen.Services
{
    public class AppState : IAppState
    {
        private string _currentDirectory;
        private ImageViewModel _currentImage;

        public string CurrentDirectory
        {
            get { return _currentDirectory; }
            set
            {
                _currentDirectory = value;
                OnCurrentDirectoryChanged();
            }
        }

        public ImageViewModel CurrentImage
        {
            get { return _currentImage; }
            set
            {
                _currentImage = value; 
                OnCurrentImageChanged();
            }
        }

        public event EventHandler CurrentImageChanged;
        public event EventHandler CurrentDirectoryChanged;

        protected virtual void OnCurrentDirectoryChanged()
        {
            var handler = CurrentDirectoryChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        protected virtual void OnCurrentImageChanged()
        {
            var handler = CurrentImageChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}