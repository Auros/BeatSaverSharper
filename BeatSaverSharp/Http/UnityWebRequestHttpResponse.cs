#if RELEASE_UNITY
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Networking;

namespace BeatSaverSharp.Http
{
    internal class UnityWebRequestHttpResponse : IHttpResponse
    {
        public int Code { get; }
        public byte[] Bytes { get; }
        public bool Successful { get; }

        public UnityWebRequestHttpResponse(UnityWebRequest unityWebRequest, bool successful)
        {
            Successful = successful;
            Code = (int)unityWebRequest.responseCode;
            Bytes = unityWebRequest.downloadHandler.data;
        }

        public Task<byte[]> ReadAsByteArrayAsync()
        {
            return Task.FromResult(Bytes);
        }

        public Task<string> ReadAsStringAsync()
        {
            return Task.FromResult(Encoding.UTF8.GetString(Bytes));
        }

        public async Task<T> ReadAsObjectAsync<T>() where T : class
        {
            var str = await ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<T>(str)!;
        }

        public async Task PopulateObjectAsync(object target)
        {
            var str = await ReadAsStringAsync().ConfigureAwait(false);
            JsonConvert.PopulateObject(str, target);
        }
    }
}
#endif