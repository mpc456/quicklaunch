using QuickLaunch.Data.Access.Abstractions.Interfaces.Model;

namespace QuickLaunch.Operations.Abstractions.Interfaces.Services
{
    public interface IProcessRunner
    {
        void Run(ILaunchInformation launchInformation);
    }
}
