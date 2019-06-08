using System;
using System.Globalization;

namespace LessplitCore.Timing.Formatters
{
    /// <summary>
    /// A time formatter to put the time in the format hh:mm:ss.ff, mm:ss.ff, ss.ff
    /// </summary>
    class ShortTimeFormatter : ITimeFormatter
    {
        public string Format(TimeSpan? time)
        {
            string negative = "";
            if (time.HasValue)
            {
                if (time.Value < TimeSpan.Zero)
                {
                    time = TimeSpan.Zero - time;
                    negative = TimeFormatConstants.MINUS;
                }
                if (time.Value.TotalDays >= 1)
                {
                    return negative + (int)time.Value.TotalHours + time.Value.ToString(@"\:mm\:ss\.ff", CultureInfo.InvariantCulture);
                }
                else if (time.Value.TotalHours >= 1)
                {
                    return negative + time.Value.ToString(@"h\:mm\:ss\.ff", CultureInfo.InvariantCulture);

                }
                else if (time.Value.TotalMinutes >= 1)
                {
                    return negative + time.Value.ToString(@"m\:ss\.ff", CultureInfo.InvariantCulture);

                }
                else
                {
                    return negative + time.Value.ToString(@"s\.ff", CultureInfo.InvariantCulture);

                }
            }
            return "0.00";
        }
        public string Format(TimeSpan? time, Format format)
        {
            string negative = "";
            if (time.HasValue)
            {
                if (time.Value < TimeSpan.Zero)
                {
                    time = TimeSpan.Zero - time;
                    negative = TimeFormatConstants.MINUS;
                }
                if (format == Formatters.Format.Seconds)
                {
                    return negative + time.Value.ToString(@"s\.ff", CultureInfo.InvariantCulture);

                }
                else if (time.Value.TotalDays >= 1)
                {
                    return negative + (int)time.Value.TotalHours + time.Value.ToString(@"\:mm\:ss\.ff", CultureInfo.InvariantCulture);
                }
                else if (format == Formatters.Format.TenHours)
                {
                    return negative + time.Value.ToString(@"hh\:mm\:ss\.ff", CultureInfo.InvariantCulture);
                }
                else if (time.Value.TotalHours >= 1 || format == Formatters.Format.Hours)
                {
                    return negative + time.Value.ToString(@"h\:mm\:ss\:.", CultureInfo.InvariantCulture);

                }
                else if (time.Value.TotalMinutes >= 1 || format == Formatters.Format.Minutes)
                {
                    return negative + time.Value.ToString(@"m\:ss\.ff", CultureInfo.InvariantCulture);

                }
                else
                {
                    return negative + time.Value.ToString(@"s\.ff", CultureInfo.InvariantCulture);

                }
            }
            return "0.00";
        }
    }
}
