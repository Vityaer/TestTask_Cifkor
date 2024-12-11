using Cysharp.Threading.Tasks;
using DataSenders.Messages.Interfaces;
using System.Threading;
using UnityEngine;

namespace DataSenders
{
    public interface IRequestSender
    {
        UniTask<string> GetData(string route, CancellationToken token);

        UniTask<Texture2D> GetRemoteTexture(string url, CancellationToken token);
    }
}