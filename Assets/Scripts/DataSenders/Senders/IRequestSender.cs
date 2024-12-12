using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace DataSenders.Senders
{
    public interface IRequestSender
    {
        UniTask<string> GetData(string route, CancellationToken token);
        UniTask<Texture2D> GetRemoteTexture(string url, CancellationToken token);
    }
}