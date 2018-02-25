using Defter.SharedLibrary.Models;
using NetCoreStack.Data.Interfaces;

namespace Defter.Api.Hosting
{
    public class LogManager
    {
        private readonly IMongoUnitOfWork _unitOfWork;

        public LogManager(IMongoUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SaveLog(DefterLog model)
        {
            _unitOfWork.Repository<DefterLog>().SaveAllChanges(model);
        }
    }
}
