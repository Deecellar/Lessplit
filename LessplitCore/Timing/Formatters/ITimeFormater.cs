using System;

namespace LessplitCore.Timing.Formatters
{
    public interface ITimeFormatter
    {
        string Format(TimeSpan? time);
    }

    public class TimeFormatConstants
    {
        public static string MINUS = "−";
        public static string DASH = "-";
    }
}