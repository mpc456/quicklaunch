using QuickLaunch.Data.Access.Interface.DataModel;

namespace QuickLaunch.Services
{
    public interface IProcessRunner
    {
        void Run(ILaunchInformation launchInformation);
    }
}
