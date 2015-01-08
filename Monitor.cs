using Akka.Monitoring.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akka.AOP.Monitoring
{
    public class Monitor : AbstractActorMonitoringClient 
    {
        public override void DisposeInternal()
        {
            Console.WriteLine("DisposeInternal");
        }

        public override int MonitoringClientId
        {
            get { return 1; }
        }

        public override void UpdateCounter(string metricName, int delta, double sampleRate)
        {
            Console.WriteLine("Update Counter Metric Name: {0}, delta: {1}, sampleRate: {2}", metricName, delta.ToString(), sampleRate.ToString());
        }

        public override void UpdateGauge(string metricName, int value, double sampleRate)
        {
            Console.WriteLine("Update Gauge Metric Name: {0}, value: {1}, sampleRate: {2}", metricName, value.ToString(), sampleRate.ToString());
        }

        public override void UpdateTiming(string metricName, long time, double sampleRate)
        {
            Console.WriteLine("Update Timing Metric Name: {0}, time: {1}, sampleRate: {2}", metricName, time.ToString(), sampleRate.ToString());
        }
    }
}
