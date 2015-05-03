using Caliburn.PresentationFramework;
using Database;

namespace PicTureen.ViewModels
{
    public class ImageViewModel : PropertyChangedBase
    {
        private readonly Image _image;
        public string Path { get { return _image.Path; } }
        public ImageViewModel(Image image)
        {
            _image = image;
        }
    }
}
