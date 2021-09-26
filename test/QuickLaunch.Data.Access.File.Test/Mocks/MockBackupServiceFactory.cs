using Moq;
using QuickLaunch.Data.Access.File.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickLaunch.Data.Access.File.Test.Mocks
{
    public static class MockBackupServiceFactory
    {
        public static Mock<IFileBackupService> Create()
        {
            var mock = new Mock<IFileBackupService>(MockBehavior.Strict);
            mock.Setup(x => x.Write(It.IsAny<byte[]>())).Verifiable();
            return mock;
        }
    }
}
