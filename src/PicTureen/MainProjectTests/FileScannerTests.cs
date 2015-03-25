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
            var scanner = new FileScanner();
            var result = scanner.ScanDirectory(currentDirectory).Result;
            Assert.AreEqual(0, result.Count());
        }
    }
}
