using QuickLaunch.Data.Model;

namespace QuickLaunch.Services
{
    public interface IProcessRunner
    {
        void Run(ILaunchInformation launchInformation);
    }
}
