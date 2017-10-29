using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerDataAccess.dtoConverter
{
    public interface IDtoConverter
    {
        IEnumerable<PowerDataCommon.domain.PowerTrade> TranslateToDomainObject(IEnumerable<Services.PowerTrade> powerTrades);
    }
}
