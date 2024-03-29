﻿using LessplitCore.Run;
using LessplitCore.Timing;

namespace LessplitCore.Comparators
{
    public class NoneComparisonGenerator : IComparisonGenerator
    {
        public IRun Run { get; set; }

        public const string ComparisonName = "None";
        public string Name => ComparisonName;

        public NoneComparisonGenerator(IRun run)
        {
            Run = run;
        }

        public void Generate(ISettings settings)
        {
            foreach (var segment in Run)
            {
                segment.Comparisons[Name] = default(Time);
            }
        }
    }
}
