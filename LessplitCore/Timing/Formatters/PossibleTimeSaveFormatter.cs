using System;

namespace LessplitCore.Timing.Formatters
{
    public class PossibleTimeSaveFormatter : ITimeFormatter
    {
        Accuracy Accuracy { get; set; }

        public string Format(TimeSpan? time)
        {
            var formatter = new ShortTimeFormatter();
            if (time == null)
            {
                return TimeFormatConstants.DASH;
            }
            var TimeString = formatter.Format(time);
            if (Accuracy == Accuracy.Hundredths)
            {
                return TimeString;
            }
            else
            {
                return Accuracy == Accuracy.Tenths ? TimeString.Substring(0, TimeString.Length - 1) : TimeString.Substring(0, TimeString.Length - 3);
            }
        }
    }
}
