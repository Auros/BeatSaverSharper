#if RELEASE_UNITY
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace BeatSaverSharp.Http
{
    public class UnityWebRequestService : IHttpService
    {
        public string? BaseURL { get; set; }

        public TimeSpan? Timeout { get; set; }

        public IDictionary<string, string> Headers { get; private set; } = new Dictionary<string, string>();

        public string? UserAgent
        {
            get
            {
                if (Headers.TryGetValue("User-Agent", out string value))
                    return value;
                else return null;
            }
            set
            {
                if (value is null)
                    if (Headers.ContainsKey("User-Agent"))
                        Headers.Remove("User-Agent");

                if (value != null)
                    if (Headers.ContainsKey("User-Agent"))
                        Headers["User-Agent"] = value;
                    else
                        Headers.Add("User-Agent", value);
            }
        }

        public Task<IHttpResponse> GetAsync(string url, CancellationToken token = default, IProgress<double>? progress = null)
        {
            return SendAsync(HTTPMethod.GET, url, null, null, progress, token);
        }

        public Task<IHttpResponse> PostAsync(string url, object? body = null, CancellationToken token = default)
        {
            return SendAsync(HTTPMethod.POST, url, JsonConvert.SerializeObject(body), null, null, token);
        }

        private async Task<IHttpResponse> SendAsync(HTTPMethod method, string url, string? body = null, IDictionary<string, string>? withHeaders = null, IProgress<double>? downloadProgress = null, CancellationToken cancellationToken = default)
        {
            if (BaseURL != null && !url.StartsWith("http"))
                url = Path.Combine(BaseURL, url);
            DownloadHandler? dHandler = new DownloadHandlerBuffer();

            using UnityWebRequest request = method == HTTPMethod.GET ? UnityWebRequest.Get(url) : (body == null ? UnityWebRequest.Post(url, body) : UnityWebRequest.Put(url, body));
            request.timeout = Timeout.HasValue ? (int)Timeout.Value.TotalSeconds : 0;

            foreach (var header in Headers)
                request.SetRequestHeader(header.Key, header.Value);

            if (withHeaders != null)
                foreach (var header in withHeaders)
                    request.SetRequestHeader(header.Key, header.Value);

            if (body != null)
                request.SetRequestHeader("Content-Type", "application/json");

            // some unity bull
            if (body != null)
            {
                request.method = "POST";
            }

            double _lastProgress = -1;
            AsyncOperation asyncOp = request.SendWebRequest();
            while (!asyncOp.isDone)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    request.Abort();
                    break;
                }
                if (downloadProgress != null && dHandler != null)
                {
                    float currentProgress = asyncOp.progress;
                    if (_lastProgress != currentProgress)
                    {
                        downloadProgress.Report(currentProgress);
                        _lastProgress = currentProgress;
                    }
                }
                await Task.Yield();
            }
            downloadProgress?.Report(1);
            bool successful = request.isDone && !request.isHttpError && !request.isNetworkError;
            return new UnityWebRequestHttpResponse(request, successful);
        }

        private enum HTTPMethod
        {
            GET,
            POST
        }
    }
}
#endif