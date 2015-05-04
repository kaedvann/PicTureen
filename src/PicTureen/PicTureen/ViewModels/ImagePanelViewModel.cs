using System;
using System.Collections.Generic;
using System.Linq;
using Caliburn.PresentationFramework;
using Database;
using PicTureen.EventArguments;
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
        private ImageViewModel _selectedImage;

        public BindableCollection<ImageViewModel> Images
        {
            get { return _images; }
            set { _images = value; }
        }

        public DelegateCommand OpenImageCommand { get; set; }

        public ImageViewModel SelectedImage
        {
            get { return _selectedImage; }
            set
            {
                _selectedImage = value;
                
                _appState.CurrentImage = _selectedImage == null? null : _selectedImage.Image;
                NotifyOfPropertyChange(()=>SelectedImage);
            }
        }

        public ImagePanelViewModel(IAppState appState, IContextProvider contextProvider, INavigationService navigationService)
        {
            _appState = appState;
            _contextProvider = contextProvider;
            _navigationService = navigationService;
            Images = new BindableCollection<ImageViewModel>();

            OpenImageCommand = new DelegateCommand(OpenImage);

            //_appState.DbChanged += AppStateOnDbChanged;
            _appState.ImageDisplayRequested += AppStateOnImageDisplayRequested;
        }

        private void AppStateOnImageDisplayRequested(object sender, ImagesEventArgs imagesEventArgs)
        {
            UpdateImages(imagesEventArgs.Images);
        }

        private void OpenImage(object obj)
        {
            var image = (ImageViewModel) obj;
            _navigationService.ShowImage(image.Image);
        }

        private void UpdateImages(IEnumerable<Image> images)
        {
            Images.IsNotifying = false;
            Images.Clear();
            Images.AddRange(images.Select(i => new ImageViewModel(i)));
            Images.IsNotifying = true;
            Images.Refresh();
        }

        private void AppStateOnDbChanged(object sender, EventArgs eventArgs)
        {
            UpdateImages(_contextProvider.GetDbContext().Images.ToList());
        }
    }
}
