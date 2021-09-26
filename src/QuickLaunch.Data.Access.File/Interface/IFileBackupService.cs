namespace QuickLaunch.Data.Access.File.Interface
{
    public interface IFileBackupService
    {
        void Write(byte[] contents);
    }
}