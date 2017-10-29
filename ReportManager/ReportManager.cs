using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using PowerDataAccess;
using PowerReportGenerator;
using PowerDataCommon.domain;

namespace ReportManager
{
    public class ReportManager : IReportManager
    {
        private static readonly Lazy<log4net.ILog> log = new Lazy<log4net.ILog>(() => log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType));

        private IPowerReportGenerator _powerReportGenerator;
        private IPowerDataAccess _powerDataAccess;

        public ReportManager(IPowerReportGenerator reportGenerator, IPowerDataAccess dataAccess)
        {
            _powerReportGenerator = reportGenerator;
            _powerDataAccess = dataAccess;
        }

        public async Task GeneratePowerDayAheadPositionsReportAsync(DateTime tenor)
        {
            var data = await _powerDataAccess.ReadDataAsync(tenor).ConfigureAwait(false);
            var collatedData = CollateData(data);
            _powerReportGenerator.GenerateReport(collatedData);
        }

        private IEnumerable<PowerReportDataPeriod> CollateData(IEnumerable<PowerTrade> data)
        {
            return data.SelectMany(trade => trade.Periods, (trade, period) => new { trade.Date, period.Period, period.Volume })
                            .GroupBy(item => item.Period)
                            .Select(row =>
                            new PowerReportDataPeriod(row.First().Date.AddHours(row.First().Period - 1), row.Sum(s => s.Volume)));

        }
    }
}
