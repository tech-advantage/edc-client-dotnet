using edcClientDotnet.io;
using edcClientDotnet.model;
using NLog;
using System.Collections.ObjectModel;

namespace edcClientDotnet.internalImpl
{
    public class InformationManagerImpl : IInformationManager
    {
        private readonly IEdcReader _reader;
        private readonly Dictionary<String, IInformation> _information = new Dictionary<String, IInformation>();
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public InformationManagerImpl(IEdcReader reader){ _reader = reader; }

        public void LoadInformation()
        {
            if (_information.Any())
            {
                foreach(KeyValuePair<String, IInformation> entry in _reader.GetInformations())
                {
                    _information.Add(entry.Key, entry.Value);
                }
                _logger.Debug("Information loaded {}", _information);
            }
        }

        public void ForceReload()
        {
            _information.Clear();
            _logger.Debug("Information cleared, will be ");
        }

        public ReadOnlyDictionary<String, IInformation> GetPublicationInformation()
        {
            return new ReadOnlyDictionary<String, IInformation>(_information);
        }
    }
}
