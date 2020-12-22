using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickLaunch.Data.Access.File.Strategies.Json;

namespace QuickLaunch.Data.Access.File.Test.Strategies.Json
{
    [TestClass]
    public class JsonFileDataAccessTests
    {
        [TestMethod]
        public void CanParseJsonData()
        {
            var config = new DataAccessFileConfig { FilePath = "Strategies/Json/TestData/launchData.json" };
            var dataAccess = new JsonFileDataAccess(config);
            var launchData = dataAccess.ReadFromFile();

            Assert.AreEqual(2, launchData.Count);
            Assert.IsTrue(launchData.ContainsKey("bbg"));
            Assert.AreEqual("https://www.bloomberg.com/", launchData["bbg"].FileName);
            Assert.IsTrue(launchData.ContainsKey("reuters"));
            Assert.AreEqual("https://uk.reuters.com/", launchData["reuters"].FileName);
        }
    }
}
