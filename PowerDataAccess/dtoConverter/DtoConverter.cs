using System;
using System.Linq;
using System.Collections.Generic;
using PowerDataCommon.domain;
using Services;

namespace PowerDataAccess.dtoConverter
{
    public class DtoConverter : IDtoConverter
    {
        public IEnumerable<PowerDataCommon.domain.PowerTrade> TranslateToDomainObject(IEnumerable<Services.PowerTrade> powerTrades)
        {
            return powerTrades.Select(x => new PowerDataCommon.domain.PowerTrade(x.Date,
                            x.Periods.Select(p => new PowerDataCommon.domain.PowerPeriod(p.Period, p.Volume))));
        }
    }
}
