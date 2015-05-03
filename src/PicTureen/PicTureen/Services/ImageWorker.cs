using System.IO;
using Database;

namespace PicTureen.Services
{
    public class ImageWorker : IImageWorker
    {
        public Image GetImageInfo(string path)
        {
            var image = new Image {Path = path};
            using (var stream = new FileStream(path, FileMode.Open))
            {
                using (var im = System.Drawing.Image.FromStream(stream, false, false))
                {
                    image.Height = im.Height;
                    image.Width = im.Width;
                }
            }
            return image;
        }
    }
}
