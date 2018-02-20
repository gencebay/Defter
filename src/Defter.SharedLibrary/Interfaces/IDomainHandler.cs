using System.Threading.Tasks;

namespace Defter.SharedLibrary
{
    public interface IDomainHandler
    {
        Task GetHandler(object evnt);
    }
}
