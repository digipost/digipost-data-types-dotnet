using System;
using Digipost.Api.Client.DataTypes.Core.Internal;

namespace Digipost.Api.Client.DataTypes.Core
{
    public class StoppedShareDocumentsRequestEvent : BaseDataType<ShareDocumentsRequestEvent>
    {
        public DateTime Timestamp { get; } = DateTime.Now;

        private readonly ShareDocumentsRequestEventType _eventType = ShareDocumentsRequestEventType.SharingStopped;

        internal override ShareDocumentsRequestEvent ToDto()
        {
            return new ShareDocumentsRequestEvent
            {
                Timestamp = Timestamp,
                EventType = _eventType
            };
        }
    }
}
