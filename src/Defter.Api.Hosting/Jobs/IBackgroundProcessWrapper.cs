namespace Defter.Api.Hosting
{
    internal interface IBackgroundProcessWrapper : IBackgroundTask
    {
        IBackgroundTask InnerProcess { get; }
    }
}
