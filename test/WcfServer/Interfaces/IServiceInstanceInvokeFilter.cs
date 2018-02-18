namespace WcfServer
{
    public interface IServiceInstanceInvokeFilter
    {
        void Invoke(InstanceInvokeContext context);
    }
}