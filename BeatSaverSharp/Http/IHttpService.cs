using System.Threading;
using System;
using System.Threading.Tasks;

namespace BeatSaverSharp.Http
{
    public interface IHttpService
    {
        Task<IHttpResponse> GetAsync(string url, CancellationToken token = default, IProgress<double>? progress = null);
        Task<IHttpResponse> PostAsync(string url, object? body = null, CancellationToken token = default);
    }
}