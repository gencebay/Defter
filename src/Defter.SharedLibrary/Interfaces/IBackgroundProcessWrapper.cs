namespace Defter.SharedLibrary
{
    public interface IBackgroundProcessWrapper : IBackgroundTask
    {
        IBackgroundTask InnerProcess { get; }
    }
}