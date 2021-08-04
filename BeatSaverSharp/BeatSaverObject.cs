using BeatSaverSharp.Exceptions;

namespace BeatSaverSharp
{
    public abstract class BeatSaverObject
    {
        internal bool HasClient => _client != null;

        private BeatSaver _client = null!;

        internal BeatSaver Client
        {
            get
            {
                if (_client == null)
                    throw new BeatSaverClientNullException();
                if (_client.IsDisposed)
                    throw new BeatSaverClientDisposedException();
                return _client!;
            }
            set => _client = value;
        }
    }
}