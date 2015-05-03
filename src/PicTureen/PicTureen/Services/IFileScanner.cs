using System.Collections.Generic;
using System.Threading.Tasks;
using Database;

namespace PicTureen.Services
{
    public interface IFileScanner
    {
        Task<IEnumerable<string>> ScanDirectory(string directory);
        Task UpdateDatabase(ImagesDbContext context, IEnumerable<string> files);
    }
}
