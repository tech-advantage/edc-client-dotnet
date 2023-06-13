using edcClientDotnet.io;
using edcClientDotnet.model;
using NLog;
using System.Collections.ObjectModel;

namespace edcClientDotnet.internalImpl
{
    public class InformationManagerImpl : IInformationManager
    {
        private readonly IEdcReader _reader;
        private readonly Dictionary<String, IInformation> _information = new();
        private readonly static Logger _logger = LogManager.GetCurrentClassLogger();

        public InformationManagerImpl(IEdcReader reader){ _reader = reader; }

        public void LoadInformation()
        {
            foreach (KeyValuePair<String, IInformation> entry in _reader.GetInformations())
            {
                if (!_information.ContainsKey(entry.Key))
                {
                    _information.Add(entry.Key, entry.Value);
                } 
            }
        }

        public void ForceReload()
        {
            _information.Clear();
            _logger.Debug("Information cleared, will be ");
        }

        public ReadOnlyDictionary<string, IInformation> GetPublicationInformation()
        {
            return new ReadOnlyDictionary<string, IInformation>(_information);
        }

    }
}
