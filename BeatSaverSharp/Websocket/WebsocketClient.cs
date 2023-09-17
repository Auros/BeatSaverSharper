using System;
using System.Collections.Generic;
using IWebsocketClientLite.PCL;
using WebsocketClientLite.PCL;

namespace BeatSaverSharp.Websocket
{
    internal class WebsocketClient : IWebsocketClient, IDisposable
    {
        private readonly MessageWebsocketRx _websocketLiteClient;
        private readonly IDisposable _observable;
        public event Action<IDataframe>? MessageRecievedEvent;
        
        public WebsocketClient(string url, string userAgent)
        { 
            _websocketLiteClient = new MessageWebsocketRx()
            {
                Headers = new Dictionary<string, string> {{ "User-Agent", userAgent }}
            };
            
            var websocketConnectionObservable = _websocketLiteClient.WebsocketConnectObservable(new Uri(url));
            _observable = websocketConnectionObservable.Subscribe(OnNext);
        }

        private void OnNext(IDataframe? dataframe)
        {
            if (dataframe != null)
            {
                MessageRecievedEvent?.Invoke(dataframe);
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            _websocketLiteClient.Dispose();
            _observable.Dispose();
        }
    }
}