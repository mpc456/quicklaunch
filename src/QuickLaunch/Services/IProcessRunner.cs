using QuickLaunch.Data.Access.Abstractions.Interfaces.Model;

namespace QuickLaunch.Services
{
    public interface IProcessRunner
    {
        void Run(ILaunchInformation launchInformation);
    }
}
