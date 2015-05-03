using System;
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
        void NotifyDbChanged();
    }
}
