using System;
using System.Globalization;

namespace LessplitCore.Timing.Formatters
{
    public class RegularTimeFormatter : ITimeFormatter
    {
        public Accuracy Accuracy { get; set; }

        public RegularTimeFormatter(Accuracy accuracy = Accuracy.Seconds)
        {
            this.Accuracy = accuracy;
        }

        public string Format(TimeSpan? time)
        {
            if (time.HasValue)
            {
                if (Accuracy == Accuracy.Hundredths)
                {
                    if (time.Value.TotalDays >= 1)
                        return (int)(time.Value.TotalHours) + time.Value.ToString(@"\:mm\:ss\.ff", CultureInfo.InvariantCulture);
                    else if (time.Value.TotalHours >= 1)
                        return time.Value.ToString(@"h\:mm\:ss\.ff", CultureInfo.InvariantCulture);
                    return time.Value.ToString(@"m\:ss\.ff", CultureInfo.InvariantCulture);
                }
                else if (Accuracy == Accuracy.Seconds)
                {
                    if (time.Value.TotalDays >= 1)
                        return (int)(time.Value.TotalHours) + time.Value.ToString(@"\:mm\:ss", CultureInfo.InvariantCulture);
                    else if (time.Value.TotalHours >= 1)
                        return time.Value.ToString(@"h\:mm\:ss", CultureInfo.InvariantCulture);
                    return time.Value.ToString(@"m\:ss", CultureInfo.InvariantCulture);
                }
                else
                {
                    if (time.Value.TotalDays >= 1)
                        return (int)(time.Value.TotalHours) + time.Value.ToString(@"\:mm\:ss\.f", CultureInfo.InvariantCulture);
                    else if (time.Value.TotalHours >= 1)
                        return time.Value.ToString(@"h\:mm\:ss\.f", CultureInfo.InvariantCulture);
                    return time.Value.ToString(@"m\:ss\.f", CultureInfo.InvariantCulture);
                }
            }
            if (Accuracy == Accuracy.Seconds)
                return "0";
            if (Accuracy == Accuracy.Tenths)
                return "0.0";
            return "0.00";
        }
    }
}
