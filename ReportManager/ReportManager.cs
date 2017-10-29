using System;
using System.Linq;
using System.Collections.Generic;
using PowerDataAccess;
using PowerReportGenerator;
using PowerDataCommon.domain;

namespace ReportManager
{
    public class ReportManager : IReportManager
    {
        private IPowerReportGenerator _powerReportGenerator;
        private IPowerDataAccess _powerDataAccess;

        public ReportManager(IPowerReportGenerator reportGenerator, IPowerDataAccess dataAccess)
        {
            _powerReportGenerator = reportGenerator;
            _powerDataAccess = dataAccess;
        }

        public void GeneratePowerDayAheadPositionsReport(DateTime tenor)
        {
            var data = _powerDataAccess.ReadData(tenor);
            var collatedData = CollateData(data);
            _powerReportGenerator.GenerateReport(collatedData);
        }

        private IEnumerable<PowerReportDataPeriod> CollateData(IEnumerable<PowerTrade> data)
        {
            return data.SelectMany(trade => trade.Periods, (trade, period) => new { trade.Date, period.Period, period.Volume})
                            .GroupBy(item => item.Period)
                            .Select(row => 
                            new PowerReportDataPeriod (row.First().Date.AddHours(row.First().Period - 1), row.Sum(s => s.Volume)));

        }
    }
}
