using System;
using System.Collections.Generic;
using PowerDataCommon.domain;
using System.Threading.Tasks;

namespace PowerDataAccess
{
    public interface IPowerDataAccess
    {
        Task<IEnumerable<PowerDataCommon.domain.PowerTrade>> ReadDataAsync(DateTime tenor);
    }
}
