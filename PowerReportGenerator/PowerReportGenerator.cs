﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using PowerDataCommon.domain;

namespace PowerReportGenerator
{
    public class PowerReportGenerator : IPowerReportGenerator
    {
        private static readonly Lazy<log4net.ILog> log = new Lazy<log4net.ILog>(() => log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType));

        private string _reportTargetPath;
        private int _hoursOffset;

        public PowerReportGenerator(string reportPath, int hourOffset)
        {
            _reportTargetPath = reportPath ?? @"C:\temp\powerreport";
            _hoursOffset = hourOffset;
        }

        public void GenerateReport(IEnumerable<PowerReportDataPeriod> data)
        {
            if (!data.Any())
            {
                log.Value.Warn("Empty data, not creating a report");
                return;
            }
            var reportDate = DateTime.UtcNow;
            var destinationReportPath = Path.Combine(_reportTargetPath, $"PowerPosition_{reportDate.ToString("yyyyMMdd")}_{reportDate.ToString("HHmm")}.csv");
            using (StreamWriter streamWriter = new StreamWriter(new FileStream(destinationReportPath, FileMode.Create)))
            {
                streamWriter.WriteLine("LocalTime,Volume");
                foreach (var period in data)
                {
                    streamWriter.WriteLine($"{period.Tenor.AddHours(_hoursOffset):HHmm},{period.Volume}");
                }
            }
        }
    }
}
