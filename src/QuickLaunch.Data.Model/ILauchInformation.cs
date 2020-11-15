namespace QuickLaunch.Data.Model
{
    public interface ILauchInformation
    {
        string Name { get; }
        string FileName { get; }
        string Arguments { get; }
        string Notes { get; }
    }
}
