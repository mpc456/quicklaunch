namespace QuickLaunch.Data.Access.Interface.DataModel
{
    public interface ILaunchInformation
    {
        string Name { get; }
        string FileName { get; }
        string Arguments { get; }
        string Notes { get; }
    }
}
