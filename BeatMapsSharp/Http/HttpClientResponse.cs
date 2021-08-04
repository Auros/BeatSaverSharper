using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BeatMapsSharp.Http
{
    internal class HttpClientResponse : IHttpResponse
    {
        public int Code => (int)_httpResponseMessage.StatusCode;
        public bool Successful => _httpResponseMessage.IsSuccessStatusCode;

        private byte[]? _bytes;
        private string? _bodyAsString;
        private readonly HttpResponseMessage _httpResponseMessage;
        private static readonly JsonSerializer _jsonSerializer = new JsonSerializer();

        public HttpClientResponse(HttpResponseMessage httpResponseMessage)
        {
            _httpResponseMessage = httpResponseMessage;
        }

        public HttpClientResponse(byte[] bytes, HttpResponseMessage httpResponseMessage)
        {
            _bytes = bytes;
            _httpResponseMessage = httpResponseMessage;
        }

        public async Task<byte[]> ReadAsByteArrayAsync()
        {
            if (_bytes is null)
                _bytes = await _httpResponseMessage.Content.ReadAsByteArrayAsync();
            return _bytes;
        }

        public async Task<string> ReadAsStringAsync()
        {
            if (_bodyAsString is null)
            {
                if (_bytes is null)
                    _bytes = await ReadAsByteArrayAsync();
                _bodyAsString = Encoding.UTF8.GetString(_bytes);
            }
            return _bodyAsString;
        }

        public async Task<T> ReadAsObjectAsync<T>() where T : class
        {
            using StringReader reader = new StringReader(await ReadAsStringAsync());
            using JsonTextReader jsonTextReader = new JsonTextReader(reader);
            return _jsonSerializer.Deserialize<T>(jsonTextReader)!;
        }
    }
}