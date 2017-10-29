using System;
using System.Collections.Generic;
using PowerDataCommon.domain;

namespace PowerDataAccess
{
    public interface IPowerDataAccess
    {
        IEnumerable<PowerDataCommon.domain.PowerTrade> ReadData(DateTime tenor);
    }
}
