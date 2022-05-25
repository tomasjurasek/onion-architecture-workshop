namespace Review.Application.Contracts
{
    internal interface IMetricCollector
    {
        void Track(string metricName);
    }
}
