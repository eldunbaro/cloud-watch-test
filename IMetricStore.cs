using System.Threading.Tasks;

namespace cloud_watch
{
    public interface IMetricStore
    {
        Task AddHit(string metricName);
    }
}