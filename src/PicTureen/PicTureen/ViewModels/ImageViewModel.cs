using Caliburn.PresentationFramework;
using Database;

namespace PicTureen.ViewModels
{
    public class ImageViewModel : PropertyChangedBase
    {
        public Image Image { get; private set; }
        public string Path { get { return Image.Path; } }
        public string Name { get { return System.IO.Path.GetFileName(Path); } }
        public ImageViewModel(Image image)
        {
            Image = image;
        }
    }
}
