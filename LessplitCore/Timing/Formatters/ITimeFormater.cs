using System;

namespace LessplitCore.Timing.Formatters
{
    /// <summary>
    /// Generic Interface for Time Formatters
    /// </summary>
    public interface ITimeFormatter
    {
        string Format(TimeSpan? time);
    }
    /// <summary>
    /// Constants of strings to add the minus or dash to the formmatters
    /// </summary>
    public class TimeFormatConstants
    {
        public static string MINUS = "−";
        public static string DASH = "-";
    }
}