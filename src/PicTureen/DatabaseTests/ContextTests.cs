using System.Data.SQLite;
using System.Linq;
using Database;
using NUnit.Framework;

namespace DatabaseTests
{
    [TestFixture]
    public class ContextTests
    {
        private const string Datasource = "FullUri=file::memory:?cache = shared;Version=3;";

        SQLiteConnection InMemoryConnection()
        {
            var creatingConnection = new SQLiteConnection(Datasource);
            new DbHelper().CreateTables(creatingConnection);
            return creatingConnection;
        }

        [Test]
        public void TestMappings()
        {
            var testImage = new Image
            {
                Description = "Test Image",
                Id = 9000,
                Path = "test.jpg",
                Rating = 5,
                Tags =
                {
                    new Tag()
                    {
                        Id = 100500,
                        Name = "TestRock"
                    },
                    new Tag()
                    {
                        Id = 100501,
                        Name = "TestMetal"
                    }
                }
            };
            var connection = InMemoryConnection();
            using (var arrangeContext = new ImagesDbContext(connection, false))
            {
                arrangeContext.Images.Add(testImage);
                arrangeContext.SaveChanges();
            }
            using (var assertContext = new ImagesDbContext(connection, false))
            {
                var savedImage = assertContext.Images.First();
                Assert.AreEqual(testImage.Path, savedImage.Path);
                Assert.AreEqual(testImage.Description, savedImage.Description);
                Assert.AreEqual(testImage.Rating, savedImage.Rating);
                Assert.AreEqual(testImage.Tags.Count, savedImage.Tags.Count);
                Assert.That(savedImage.Tags.SequenceEqual(testImage.Tags));
            }
        }
    }
}
