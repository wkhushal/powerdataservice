using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Services;
using PowerDataAccess.dtoConverter;

namespace PowerDataAccess
{
    public class PowerDataAccess : IPowerDataAccess
    {
        private static readonly Lazy<log4net.ILog> log = new Lazy<log4net.ILog>(() => log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType));

        private IPowerService _powerService;
        private IDtoConverter _dtoConverter;

        public PowerDataAccess(IPowerService powerService, IDtoConverter dtoConverter)
        {
            _powerService = powerService;
            _dtoConverter = dtoConverter;
        }

        public async Task<IEnumerable<PowerDataCommon.domain.PowerTrade>> ReadDataAsync(DateTime tenor)
        {
            try
            {
                return _dtoConverter.TranslateToDomainObject(await _powerService.GetTradesAsync(tenor).ConfigureAwait(false));
            }
            catch(PowerServiceException pex)
            {
                log.Value.Error(pex);
            }
            return Enumerable.Empty<PowerDataCommon.domain.PowerTrade>(); 
        }
    }
}
