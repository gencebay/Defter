namespace Defter.SharedLibrary
{
    public interface IBackgroundTask
    {
        void Invoke(TaskContext context);
    }
}