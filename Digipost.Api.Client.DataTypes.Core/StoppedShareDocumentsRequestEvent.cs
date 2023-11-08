using System;
using Digipost.Api.Client.DataTypes.Core.Internal;

namespace Digipost.Api.Client.DataTypes.Core
{
    public class StoppedShareDocumentsRequestEvent : DataType<ShareDocumentsRequestEvent>
    {
        public StoppedShareDocumentsRequestEvent()
        {
            Timestamp = DateTime.Now;
            _eventType = ShareDocumentsRequestEventType.SHARING_STOPPED;
        }

        public DateTime Timestamp { get; }
        public String Event => _eventType.ToString();

        private readonly ShareDocumentsRequestEventType _eventType;

        protected override ShareDocumentsRequestEvent ToDto()
        {
            return new ShareDocumentsRequestEvent
            {
                Timestamp = Timestamp,
                Event_Type = _eventType
            };
        }
    }
}
