using System;
using System.Linq;
using System.Collections.Generic;
using Services;
using PowerDataAccess.dtoConverter;

namespace PowerDataAccess
{
    public class PowerDataAccess : IPowerDataAccess
    {
        private IPowerService _powerService;
        private IDtoConverter _dtoConverter;

        public PowerDataAccess(IPowerService powerService, IDtoConverter dtoConverter)
        {
            _powerService = powerService;
            _dtoConverter = dtoConverter;
        }

        public IEnumerable<PowerDataCommon.domain.PowerTrade> ReadData(DateTime tenor)
        {
            try
            {
                return _dtoConverter.TranslateToDomainObject(_powerService.GetTrades(tenor));
            }
            catch(PowerServiceException pex)
            {
                Console.WriteLine(pex.Message);
            }
            return Enumerable.Empty<PowerDataCommon.domain.PowerTrade>(); 
        }
    }
}
