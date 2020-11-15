using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickLaunch.Data.Access.File.Strategies.Ini;
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
            var dataAccess = new QrsFileDataAccess();
            var config = new DataAccessFileConfig { FilePath = "Strategies/Ini/TestData/magicWords.qrs" };
            var launchData = dataAccess.GetLaunchInformation(config);

            Assert.AreEqual(2, launchData.Count);
            Assert.IsTrue(launchData.ContainsKey("babel"));
            Assert.AreEqual("https://translate.google.com/", launchData["babel"].FileName);
            Assert.IsTrue(launchData.ContainsKey("mail"));
            Assert.AreEqual("https://outlook.live.com/", launchData["mail"].FileName);

        }
    }
}
