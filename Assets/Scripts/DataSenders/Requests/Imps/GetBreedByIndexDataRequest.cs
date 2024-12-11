using DataSenders.Requests.Abstactions;
using Models.ServerAnswers.Breeds;
using System.Threading;

namespace DataSenders.Requests.Imps
{
    public class GetBreedByIndexDataRequest : AbstractRequest<BreedByIndexServerIndexAnswer>
    {
        private readonly string _id;

        protected override string Route => string.Concat("https://dogapi.dog/api/v2/breeds/", _id);

        public GetBreedByIndexDataRequest(string index, CancellationTokenSource cancellationTokenSource)
        {
            _id = index;
            IternalCancellationTokenSource = cancellationTokenSource;
        }
    }
}
