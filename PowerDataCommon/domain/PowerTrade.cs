using System;
using System.Collections.Generic;
using System.Linq;

namespace PowerDataCommon.domain
{
    // for now I am creating a copy of power trade type, but it should be different
    // idea is to create local domain objects so that we can change the service and the code doesnt have to change
    public class PowerTrade
    {
        public PowerTrade(DateTime date, IEnumerable<PowerPeriod> periods)
        {
            Date = date;
            Periods = periods.ToArray();
        }

        public DateTime Date { get; }
        public PowerPeriod[] Periods { get; }
    }
}
