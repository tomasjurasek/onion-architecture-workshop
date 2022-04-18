namespace Review.Domain.Contracts
{
    internal interface IMetricCollector
    {
        void Track(string metricName);
    }
}
