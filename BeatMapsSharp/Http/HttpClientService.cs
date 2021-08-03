using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BeatMapsSharp.Http
{
    internal class HttpClientService : IHttpService, IDisposable
    {
        private readonly HttpClient _httpClient;

        public HttpClientService(string baseURL, TimeSpan timeout, string userAgent)
        {
            HttpClientHandler handler = new HttpClientHandler
            {
                AutomaticDecompression = (DecompressionMethods.GZip | DecompressionMethods.Deflate)
            };
            _httpClient = new HttpClient(handler)
            {
                Timeout = timeout,
                BaseAddress = new Uri(baseURL)
            };
            _httpClient.DefaultRequestHeaders.Add("User-Agent", userAgent);
        }

        public async Task<IHttpResponse> GetAsync(string url, CancellationToken? token, IProgress<double>? progress = null)
        {
            CancellationToken reqToken = token ?? CancellationToken.None;
            // We read starting with the response headers so we can update the IProgress<double> if it exists, as well as stopping the body if necessary.
            HttpResponseMessage message = await _httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, reqToken).ConfigureAwait(false);

            if (reqToken.IsCancellationRequested)
                throw new TaskCanceledException();

            using MemoryStream ms = new MemoryStream();
            using Stream streamedBody = await message.Content.ReadAsStreamAsync().ConfigureAwait(false);

            long total = 0;
            byte[] buffer = new byte[8192];
            long? length = message.Content.Headers.ContentLength;
            progress?.Report(0);

            while (true)
            {
                int read = await streamedBody.ReadAsync(buffer).ConfigureAwait(false);
                if (read <= 0)
                    break;

                if (reqToken.IsCancellationRequested)
                    throw new TaskCanceledException();

                if (length.HasValue)
                    progress?.Report((double)total / length.Value);

                await ms.WriteAsync(buffer).ConfigureAwait(false);
                total += read;
            }
            progress?.Report(1);
            byte[] body = ms.ToArray();
            return new HttpClientResponse(body, message);
        }

        public async Task<IHttpResponse> PostAsync(string url, object? body = null, CancellationToken? token = null)
{
            CancellationToken reqToken = token ?? CancellationToken.None;
            var serializedBody = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
            HttpResponseMessage message = await _httpClient.PostAsync(url, serializedBody, reqToken).ConfigureAwait(false);
            return new HttpClientResponse(message);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            _httpClient.Dispose();
        }
    }
}