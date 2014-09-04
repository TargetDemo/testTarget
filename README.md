testTarget
==========
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
                     SampleRate = TimeSpan.FromSeconds(1)  //Gets or sets the rate at which to sample the performance counter, rounded up to the nearest second. 
                 };
            // Set the diagnostic monitor configuration.
            config.PerformanceCounters.DataSources.Add(cpuUtilizationCounter);
            config.PerformanceCounters.DataSources.Add(memoryUtilizationCounter);
            DiagnosticMonitor.Start("Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString", config);
