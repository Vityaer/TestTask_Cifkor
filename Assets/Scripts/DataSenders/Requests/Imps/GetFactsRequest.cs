using DataSenders.Requests.Abstactions;
using Models.ServerAnswers.Breeds;
using System.Threading;

namespace DataSenders.Requests.Imps
{
    public class GetFactsRequest : AbstractRequest<BreedsServerAnswer>
    {
        protected override string Route => "https://dogapi.dog/api/v2/breeds";

        public GetFactsRequest(CancellationTokenSource cancellationTokenSource)
        {
            IternalCancellationTokenSource = cancellationTokenSource;
        }
    }
}
