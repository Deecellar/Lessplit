using Eto.Drawing;
using LessplitCore.Comparators;
using System;

namespace LessplitCore.Timing
{
    public interface ISegment : ICloneable
    {
        Image Icon { get; set; }
        string Name { get; set; }
        Time PersonalBestSplitTime { get; set; }
        IComparisons Comparisons { get; set; }
        Time BestSegmentTime { get; set; }
        Time SplitTime { get; set; }
        SegmentHistory SegmentHistory { get; set; }
    }
}
