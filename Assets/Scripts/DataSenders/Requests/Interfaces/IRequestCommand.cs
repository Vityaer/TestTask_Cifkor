using DataSenders.Requests.Interfaces;
using System;

namespace DataSenders.Messages.Interfaces
{
    public interface IRequestCommand<T> : IBaseRequestCommand
    {
        IObservable<T> OnRequestDone { get; }
    }
}
