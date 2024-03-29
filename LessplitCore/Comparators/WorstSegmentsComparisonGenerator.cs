﻿using LessplitCore.Run;
using LessplitCore.Timing;
using System;

namespace LessplitCore.Comparators
{
    public class WorstSegmentsComparisonGenerator : IComparisonGenerator
    {
        public IRun Run { get; set; }
        public const string ComparisonName = "Worst Segments";
        public const string ShortComparisonName = "Worst";
        public string Name => ComparisonName;

        public WorstSegmentsComparisonGenerator(IRun run)
        {
            Run = run;
        }

        public void Generate(ISettings settings)
        {
            var realTimePredictions = new TimeSpan?[Run.Count + 1];
            var gameTimePredictions = new TimeSpan?[Run.Count + 1];
            SumOfWorst.CalculateSumOfWorst(Run, 0, Run.Count - 1, realTimePredictions, false, TimingMethod.RealTime);
            SumOfWorst.CalculateSumOfWorst(Run, 0, Run.Count - 1, gameTimePredictions, false, TimingMethod.GameTime);
            var index = 1;
            foreach (var segment in Run)
            {
                segment.Comparisons[Name] = new Time(
                    realTimePredictions[index],
                    gameTimePredictions[index]);
                index++;
            }
        }
    }
}