using BeatMapsSharp.Exceptions;

namespace BeatMapsSharp
{
    public abstract class BeatSaverObject
    {
        internal bool HasClient => _client != null;

        private BeatSaver _client = null!;

        public BeatSaver Client
        {
            get
            {
                if (_client == null)
                    throw new BeatSaverClientNullException();
                if (_client.IsDisposed)
                    throw new BeatSaverClientDisposedException();
                return _client!;
            }
            internal set => _client = value;
        }
    }
}