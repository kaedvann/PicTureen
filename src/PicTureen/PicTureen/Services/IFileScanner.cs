using System.Collections.Generic;
using System.Threading.Tasks;

namespace PicTureen.Services
{
    public interface IFileScanner
    {
        Task<IEnumerable<string>> ScanDirectory(string directory);
    }
}
