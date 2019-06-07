using System;
using System.Globalization;

namespace LessplitCore.Timing.Formatters
{
    public class DeltaTimeFormatter
    {
        public Accuracy Accuracy { get; set; }
        public bool DropDecimals { get; set; }

        public DeltaTimeFormatter()
        {
            Accuracy = Accuracy.Tenths;
            DropDecimals = true;
        }

        public string Format(TimeSpan? time)
        {
            if (!time.HasValue)
            {
                return TimeFormatConstants.DASH;
            }
            string minusString = "+";
            if (time.Value < TimeSpan.Zero)
            {
                minusString = TimeFormatConstants.MINUS;
                time = TimeSpan.Zero - time;
            }
            string totalString = time.Value.TotalDays >= 1
? minusString + (int)(time.Value.TotalHours) + time.Value.ToString(@"\:mm\:ss\.ff", CultureInfo.InvariantCulture)
: time.Value.TotalHours >= 1
? minusString + time.Value.ToString(@"h\:mm\:ss\.ff", CultureInfo.InvariantCulture)
: time.Value.TotalMinutes >= 1
? minusString + time.Value.ToString(@"m\:ss\.ff", CultureInfo.InvariantCulture)
: minusString + time.Value.ToString(@"s\.ff", CultureInfo.InvariantCulture);

            if ((DropDecimals && time.Value.TotalMinutes >= 1) || Accuracy == Accuracy.Seconds)
            {
                return totalString.Substring(0, totalString.Length - 3);
            }
            else if (Accuracy == Accuracy.Tenths)
            {
                return totalString.Substring(0, totalString.Length - 1);
            }

            return totalString;
        }
    }
}
