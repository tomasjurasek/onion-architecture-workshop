namespace Review.Application.Contracts
{
    internal interface IMetric
    {
        void Track(string metricName);
    }
}
