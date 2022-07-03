using System;
using IWebsocketClientLite.PCL;

namespace BeatSaverSharp.Websocket
{
    public interface IWebsocketClient
    {
        public event Action<IDataframe>? MessageRecievedEvent; 
    }
}