namespace QuickLaunch.Model
{
    public class LaunchInformation : ILauchInformation
    {
        public string Name { get; set; }

        public string FileName { get; set; }

        public string Arguments { get; set; }

        public string Notes { get; set; }
    }
}
