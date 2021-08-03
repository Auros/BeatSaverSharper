using System.Threading;
using System;
using System.Threading.Tasks;

namespace BeatMapsSharp.Http
{
    public interface IHttpService
    {
        Task<IHttpResponse> GetAsync(string url, CancellationToken? token, IProgress<double>? progress = null);
        Task<IHttpResponse> PostAsync(string url, object? body = null, CancellationToken? token = null);
    }
}