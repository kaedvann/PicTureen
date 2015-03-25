using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PicTureen.Services
{
    public interface IFileScanner
    {
        Task<IEnumerable<string>> ScanDirectory(string directory);
    }

    public class FileScanner : IFileScanner
    {
        private readonly string[] _supportedExtensions = {".png", ".jpg", ".bmp"};

        public Task<IEnumerable<string>> ScanDirectory(string directory)
        {
            return Task<IEnumerable<string>>.Factory.StartNew(() =>Directory.EnumerateFiles(directory, ".", SearchOption.AllDirectories).Where(s => _supportedExtensions.Contains(Path.GetExtension(s), StringComparer.OrdinalIgnoreCase)));
        }
    }
}
