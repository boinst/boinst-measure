namespace Boinst.Measure.Time
{
    public abstract class Time : Measure, ITime
    {
        protected const double SecondsPerMinute = 60;

        protected const double MinutesPerHour = 60;

        protected const double HoursPerDay = 24;

        protected const double SecondsPerDay = HoursPerDay * MinutesPerHour * SecondsPerMinute;

        protected Time(double value)
            : base(value)
        {
        }
    }
}
