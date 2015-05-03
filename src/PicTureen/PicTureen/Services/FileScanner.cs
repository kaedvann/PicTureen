using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Database;

namespace PicTureen.Services
{
    public class FileScanner : IFileScanner
    {
        private readonly IImageWorker _imageWorker;
        private readonly string[] _supportedExtensions = {".png", ".jpg", ".bmp"};

        public FileScanner(IImageWorker imageWorker)
        {
            _imageWorker = imageWorker;
        }

        public Task<IEnumerable<string>> ScanDirectory(string directory)
        {
            return Task<IEnumerable<string>>.Factory.StartNew(() =>Directory.EnumerateFiles(directory, ".", SearchOption.AllDirectories).Where(s => _supportedExtensions.Contains(Path.GetExtension(s), StringComparer.OrdinalIgnoreCase)));
        }

        public async Task UpdateDatabase(ImagesDbContext context, IEnumerable<string> files)
        {
            await context.Images.LoadAsync();
            var toRemove = context.Images.ToList().Where(i => !files.Contains(i.Path));
            context.Images.RemoveRange(toRemove);
            var toAdd = files.Where(f => context.Images.All(i => i.Path != f)).Select(f => _imageWorker.GetImageInfo(f));
            context.Images.AddRange(toAdd);
            await context.SaveChangesAsync();
        }
    }
}