using System.IO;
using System.Linq;
using NUnit.Framework;
using PicTureen.Services;

namespace MainProjectTests
{
    [TestFixture]
    public class FileScannerTests
    {
        [Test]
        public void TestScanning()
        {
            var currentDirectory = TestContext.CurrentContext.TestDirectory;
            var scanner = new FileScanner(null);
            var task = scanner.ScanDirectory(currentDirectory);
            task.Wait();
            var result = task.Result; 
            Assert.AreEqual(0, result.Count());
            SampleFile(currentDirectory, "test1", ".jpg");
            SampleFile(currentDirectory, "test2", ".png");
            SampleFile(currentDirectory, "test3", ".bmp");
            task = scanner.ScanDirectory(currentDirectory);
            task.Wait();
            result = task.Result;
            Assert.AreEqual(3, result.Count());
            SampleFile(currentDirectory, "test10", ".jpsdg");
            SampleFile(currentDirectory, "test20", ".pngfg");
            SampleFile(currentDirectory, "test30", ".bmdfp");
            task = scanner.ScanDirectory(currentDirectory);
            task.Wait();
            result = task.Result;
            Assert.AreEqual(3, result.Count());
        }

        void SampleFile(string directory, string name, string extension)
        {
            File.Create(Path.ChangeExtension(Path.Combine(directory, name), extension)).Dispose();
        }
    }
}
