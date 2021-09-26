using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickLaunch.Data.Access.File.Strategies.Ini;
using QuickLaunch.Data.Access.File.Test.Mocks;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickLaunch.Data.Access.File.Test.Strategies.Ini
{
    [TestClass]
    public class QrsFileDataAccessTest
    {
        [TestMethod]
        public void CanReadQrsFile()
        {
            
            var config = new DataAccessFileConfig { FilePath = "Strategies/Ini/TestData/magicWords.qrs" };
            var mockBackupService = MockBackupServiceFactory.Create();
            var dataAccess = new QrsFileDataAccess(config, mockBackupService.Object);
            var launchData = dataAccess.ReadFromFile();

            Assert.AreEqual(2, launchData.Count);
            Assert.IsTrue(launchData.ContainsKey("babel"));
            Assert.AreEqual("https://translate.google.com/", launchData["babel"].FileName);
            Assert.IsTrue(launchData.ContainsKey("mail"));
            Assert.AreEqual("https://outlook.live.com/", launchData["mail"].FileName);

        }
    }
}
