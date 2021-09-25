using QuickLaunch.Data.Access.Abstractions.Interfaces.Model;

namespace QuickLaunch.Data.Access.Abstractions.Model
{
    public class LaunchInformation : ILaunchInformation
    {
        public string Name { get; set; }

        public string FileName { get; set; }

        public string Arguments { get; set; }

        public string Notes { get; set; }

        public int LaunchCount { get; set; } = 0;
    }
}
