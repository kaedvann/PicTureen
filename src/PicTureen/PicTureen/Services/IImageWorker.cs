using Database;

namespace PicTureen.Services
{
    public interface IImageWorker
    {
        Image GetImageInfo(string path);
    }
}