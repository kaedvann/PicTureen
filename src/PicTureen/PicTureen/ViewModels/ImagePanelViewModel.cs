using System;
using System.Linq;
using Caliburn.PresentationFramework;
using Database;
using PicTureen.Services;

namespace PicTureen.ViewModels
{
    public class ImagePanelViewModel: PropertyChangedBase
    {
        private readonly IAppState _appState;
        private readonly IContextProvider _contextProvider;
        private BindableCollection<ImageViewModel> _images;

        public BindableCollection<ImageViewModel> Images
        {
            get { return _images; }
            set { _images = value; }
        }

        public ImagePanelViewModel(IAppState appState, IContextProvider contextProvider)
        {
            _appState = appState;
            _contextProvider = contextProvider;
            Images = new BindableCollection<ImageViewModel>();

            _appState.DbChanged += AppStateOnDbChanged;
        }

        private void AppStateOnDbChanged(object sender, EventArgs eventArgs)
        {
            Images.IsNotifying = false;
            Images.Clear();
            Images.AddRange(_contextProvider.GetDbContext().Images.ToList().Select(i => new ImageViewModel(i)));
            Images.IsNotifying = true;
            Images.Refresh();
        }
    }
}
