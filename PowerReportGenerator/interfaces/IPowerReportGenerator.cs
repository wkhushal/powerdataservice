using System;
using System.Collections.Generic;
using PowerDataCommon.domain;

namespace PowerReportGenerator
{
    public interface IPowerReportGenerator
    {
        void GenerateReport(IEnumerable<PowerReportDataPeriod> data);
    }
}
