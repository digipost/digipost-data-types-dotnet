using System;
using System.Collections.Generic;
using System.Linq;
using Digipost.Api.Client.DataTypes.Core.Common;

namespace Digipost.Api.Client.DataTypes.Core
{
    public class Appointment : BaseDataType<Internal.Appointment>
    {
        public DateTime StartTime { get; }
        public DateTime EndTime { get; set; }
        public string ArrivalTime { get; set; }
        public string Place { get; set; }
        public Address Address { get; set; }
        public Language Language { get; set; }
        public List<Info> Infos { get; }
        public string SubTitle { get; set; }

        public ExternalLink Link { get; set; }

        public Appointment(DateTime startTime)
        {
            StartTime = startTime;
            Infos = new List<Info>();
            Language = Language.NB;
        }

        internal override Internal.Appointment ToDto()
        {
            var appointment = new Internal.Appointment
            {
                StartTime = DatetimeFormatter(StartTime),
                EndTime = DatetimeFormatter(EndTime),
                ArrivalTime = ArrivalTime,
                Place = Place,
                SubTitle = SubTitle
            };

            if (Link != null)
            {
                appointment.Link = Link.ToDto();
            }

            appointment.Language = Language.ToDto();

            if (Address != null)
            {
                appointment.Address = Address.ToDto();
            }

            foreach (var info in Infos.Select(info => info.ToDto()))
            {
                appointment.Info.Add(info);
            }

            return appointment;
        }
    }
}
