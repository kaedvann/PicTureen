using System;
using System.Linq;
using Caliburn.PresentationFramework;
using PicTureen.Services;
using PicTureen.Support;

namespace PicTureen.ViewModels
{
    public class ImagePanelViewModel: PropertyChangedBase
    {
        private readonly IAppState _appState;
        private readonly IContextProvider _contextProvider;
        private readonly INavigationService _navigationService;
        private BindableCollection<ImageViewModel> _images;

        public BindableCollection<ImageViewModel> Images
        {
            get { return _images; }
            set { _images = value; }
        }

        public DelegateCommand OpenImageCommand { get; set; }

        public ImagePanelViewModel(IAppState appState, IContextProvider contextProvider, INavigationService navigationService)
        {
            _appState = appState;
            _contextProvider = contextProvider;
            _navigationService = navigationService;
            Images = new BindableCollection<ImageViewModel>();

            OpenImageCommand = new DelegateCommand(OpenImage);

            _appState.DbChanged += AppStateOnDbChanged;
        }

        private void OpenImage(object obj)
        {
            var image = (ImageViewModel) obj;
            _navigationService.ShowImage(image.Image);
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
