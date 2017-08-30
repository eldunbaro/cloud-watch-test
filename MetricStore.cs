using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.CloudWatch;
using Amazon.CloudWatch.Model;
using Newtonsoft.Json;
using static System.Console;

namespace cloud_watch
{
    public class MetricStore : IMetricStore
    {

        private readonly IAmazonCloudWatch _cloudWatch;

        public MetricStore(IAmazonCloudWatch cloudWatch)
        {
            _cloudWatch = cloudWatch;
        }
        
        public async Task AddHit(string metricName)
        {
            try {
                var metric1 = new MetricDatum
                {
                    Dimensions = new List<Dimension>(),
                    MetricName = "EndpointHitNew",
                    StatisticValues = new StatisticSet {
                        SampleCount = 50,
                        Sum = 50,
                        Minimum = 1,
                        Maximum = 1
                    },
                    Unit = StandardUnit.Count
                };
                string json = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(metric1));
                WriteLine(json);
                await _cloudWatch.PutMetricDataAsync(new PutMetricDataRequest {
                    Namespace = "MetricStoreTest",
                    MetricData = new List<MetricDatum> {
                        metric1
                    }
                });
            }
            catch (Exception ex)
            {
                WriteLine(ex.Message);
            }
        }
    }
}