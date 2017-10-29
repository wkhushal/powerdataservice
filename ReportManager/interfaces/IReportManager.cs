using System;

namespace ReportManager
{
    public interface IReportManager
    {
        void GeneratePowerDayAheadPositionsReport(DateTime tenor);
    }
}
