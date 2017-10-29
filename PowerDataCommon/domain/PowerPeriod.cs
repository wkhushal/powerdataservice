namespace PowerDataCommon.domain
{
    public class PowerPeriod
    {
        public PowerPeriod(int period, double volume)
        {
            Period = period;
            Volume = volume;
        }

        public int Period { get; set; }
        public double Volume { get; set; }
    }
}
