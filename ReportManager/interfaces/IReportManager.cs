using System;
using System.Threading.Tasks;

namespace ReportManager
{
    public interface IReportManager
    {
        Task GeneratePowerDayAheadPositionsReportAsync(DateTime tenor);
    }
}
