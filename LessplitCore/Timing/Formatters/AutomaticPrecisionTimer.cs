using System;

namespace LessplitCore.Timing.Formatters
{
    public class AutomaticPrecisionTimeFormatter : ITimeFormatter
    {
        readonly RegularTimeFormatter InternalFormatter;

        public AutomaticPrecisionTimeFormatter()
        {
            InternalFormatter = new RegularTimeFormatter();
        }

        public string Format(TimeSpan? time)
        {
            if (time.HasValue)
            {
                double totalSeconds = time.Value.TotalSeconds;
                InternalFormatter.Accuracy = totalSeconds % 1 == 0 ? Accuracy.Seconds : (10 * totalSeconds) % 1 == 0 ? Accuracy.Tenths : Accuracy.Hundredths;
            }

            return InternalFormatter.Format(time);
        }
    }
}
