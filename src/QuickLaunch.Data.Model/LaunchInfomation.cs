namespace QuickLaunch.Data.Model
{
    public class LaunchInformation : ILaunchInformation
    {
        public string Name { get; set; }

        public string FileName { get; set; }

        public string Arguments { get; set; }

        public string Notes { get; set; }
    }
}
