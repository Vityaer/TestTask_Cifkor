using DataSenders.Requests.Interfaces;
using System.Threading;

namespace DataSenders.Managers
{
    public interface IRequestsManager
    {
        void AddRequest(IBaseRequestCommand request);
    }
}
