using DataSenders.Requests.Interfaces;

namespace DataSenders.Managers
{
    public interface IRequestsManager
    {
        void AddRequest(IBaseRequestCommand request);
    }
}
