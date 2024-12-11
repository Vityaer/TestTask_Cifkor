using DataSenders.Requests.Abstactions;
using Models.ServerAnswers;
using System.Threading;

namespace DataSenders.Requests.Imps
{
    public class GetWeatherRequest : AbstractRequest<WeatherServerAnswer>
    {
        protected override string Route => "https://api.weather.gov/gridpoints/TOP/32,81/forecast";

        public GetWeatherRequest(CancellationTokenSource cancellationTokenSource)
        {
            IternalCancellationTokenSource = cancellationTokenSource;
        }
    }
}
