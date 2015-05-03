using System;
using System.Collections.Generic;
using Database;
using PicTureen.EventArguments;
using PicTureen.ViewModels;

namespace PicTureen.Services
{
    public interface IAppState
    {
        string CurrentDirectory { get; set; }
        ImageViewModel CurrentImage { get; set; }
        event EventHandler CurrentImageChanged;
        event EventHandler CurrentDirectoryChanged;
        event EventHandler DbChanged;
        event EventHandler<ImagesEventArgs> ImageDisplayRequested; 
        void NotifyDbChanged();
        void RequestImagesDisplay(IEnumerable<Image> images);
    }
}