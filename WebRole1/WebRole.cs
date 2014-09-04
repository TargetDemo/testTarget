using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace WebRole1
{
    public class WebRole : RoleEntryPoint
    {
        public override bool OnStart()
        {
            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.
            var config = DiagnosticMonitor.GetDefaultInitialConfiguration();

            // Transfer diagnostic information once every minute.
            TimeSpan transferPeriod = TimeSpan.FromSeconds(30);
            config.PerformanceCounters.ScheduledTransferPeriod = transferPeriod;
            PerformanceCounterConfiguration cpuUtilizationCounter =
                    new PerformanceCounterConfiguration()
                    {
                        CounterSpecifier = @"\Processor(_Total)\% Processor Time",
                        //define the sample internal for the specific performance counter
                        SampleRate = TimeSpan.FromSeconds(1)
                    };
            PerformanceCounterConfiguration memoryUtilizationCounter =
                 new PerformanceCounterConfiguration()
                 {
                     CounterSpecifier = @"\Memory\Available MBytes",
                     //define the sample internal for the specific performance counter
                     SampleRate = TimeSpan.FromSeconds(1)
                 };
            // Set the diagnostic monitor configuration.
            config.PerformanceCounters.DataSources.Add(cpuUtilizationCounter);
            config.PerformanceCounters.DataSources.Add(memoryUtilizationCounter);
            DiagnosticMonitor.Start("Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString", config);

            return base.OnStart();
        }
    }
}
