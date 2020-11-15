using QuickLaunch.Model;

namespace QuickLaunch.Services
{
    public interface IProcessRunner
    {
        void Run(ILauchInformation launchInformation);
    }
}
