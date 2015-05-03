using System;
using System.Collections.Generic;
using Database;
using PicTureen.EventArguments;
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
        public event EventHandler DbChanged;
        public event EventHandler<ImagesEventArgs> ImageDisplayRequested;

        public void NotifyDbChanged()
        {
            OnDbChanged();
        }

        public void RequestImagesDisplay(IEnumerable<Image> images)
        {
            OnImageDisplayRequested(new ImagesEventArgs(images));
        }

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

        protected virtual void OnDbChanged()
        {
            var handler = DbChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        protected virtual void OnImageDisplayRequested(ImagesEventArgs e)
        {
            var handler = ImageDisplayRequested;
            if (handler != null) handler(this, e);
        }
    }
}