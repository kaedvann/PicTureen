using Database;

namespace PicTureen.Services
{
    public interface IContextProvider
    {
        ImagesDbContext GetDbContext();
    }
}