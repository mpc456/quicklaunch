using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickLaunch.Data.Access.File.Strategies.Json;

namespace QuickLaunch.Data.Access.File.Test
{
    [TestClass]
    public class JsonFileDataAccessTests
    {
        [TestMethod]
        public void CanParseJsonData()
        {
            var dataAccess = new JsonFileDataAccess();
            var config = new DataAccessFileConfig { FilePath = "Strategies/Json/TestData/launchData.json" };
            var launchData = dataAccess.GetLaunchInformation(config);
        }
    }
}
