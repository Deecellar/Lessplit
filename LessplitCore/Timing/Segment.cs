﻿using Eto.Drawing;
using LessplitCore.Comparators;
using System;
namespace LessplitCore.Timing
{
    [Serializable]
    public class Segment : ISegment
    {
        public Image Icon { get; set; }
        public string Name { get; set; }
        public Time PersonalBestSplitTime
        {
            get => Comparisons[Run.Run.PersonalBestComparisonName];
            set => Comparisons[Run.Run.PersonalBestComparisonName] = value;
        }
        public IComparisons Comparisons { get; set; }
        public Time BestSegmentTime { get; set; }
        public Time SplitTime { get; set; }
        public SegmentHistory SegmentHistory { get; set; }

        public Segment(
            string name, Time pbSplitTime = default(Time),
            Time bestSegmentTime = default(Time), Image icon = null,
            Time splitTime = default(Time))
        {
            Comparisons = new CompositeComparisons();
            Name = name;
            PersonalBestSplitTime = pbSplitTime;
            BestSegmentTime = bestSegmentTime;
            SplitTime = splitTime;
            Icon = icon;
            SegmentHistory = new SegmentHistory();
        }

        public Segment Clone()
        {
            var newSegmentHistory = SegmentHistory.Clone();

            return new Segment(Name)
            {
                BestSegmentTime = BestSegmentTime,
                SplitTime = SplitTime,
                Icon = Icon,
                SegmentHistory = newSegmentHistory,
                Comparisons = (IComparisons)Comparisons.Clone()
            };
        }

        object ICloneable.Clone() => Clone();
    }
}
