using System;

namespace PowerDataCommon.domain
{
    public class PowerReportDataPeriod
    {
        public PowerReportDataPeriod(DateTime tenor, double volume)
        {
            Tenor = tenor;
            Volume = volume;
        }

        public DateTime Tenor { get; }
        public double Volume { get; }

    }
}
