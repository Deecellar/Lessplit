using LessplitCore.Run;
using System.Collections.Generic;

namespace LessplitCore.Comparators
{
    public class StandardComparisonGeneratorsFactory : IComparisonGeneratorsFactory
    {
        static StandardComparisonGeneratorsFactory()
        {
            CompositeComparisons.AddShortComparisonName(BestSegmentsComparisonGenerator.ComparisonName, BestSegmentsComparisonGenerator.ShortComparisonName);
            CompositeComparisons.AddShortComparisonName(Run.Run.PersonalBestComparisonName, "PB");
            CompositeComparisons.AddShortComparisonName(AverageSegmentsComparisonGenerator.ComparisonName, AverageSegmentsComparisonGenerator.ShortComparisonName);
            CompositeComparisons.AddShortComparisonName(WorstSegmentsComparisonGenerator.ComparisonName, WorstSegmentsComparisonGenerator.ShortComparisonName);
            CompositeComparisons.AddShortComparisonName(PercentileComparisonGenerator.ComparisonName, PercentileComparisonGenerator.ShortComparisonName);
            CompositeComparisons.AddShortComparisonName(LatestRunComparisonGenerator.ComparisonName, LatestRunComparisonGenerator.ShortComparisonName);
        }
        public IEnumerable<IComparisonGenerator> Create(IRun run)
        {
            yield return new BestSegmentsComparisonGenerator(run);
            yield return new AverageSegmentsComparisonGenerator(run);
        }

        public IEnumerable<IComparisonGenerator> GetAllGenerators(IRun run)
        {
            yield return new BestSegmentsComparisonGenerator(run);
            yield return new BestSplitTimesComparisonGenerator(run);
            yield return new AverageSegmentsComparisonGenerator(run);
            yield return new WorstSegmentsComparisonGenerator(run);
            yield return new PercentileComparisonGenerator(run);
            yield return new LatestRunComparisonGenerator(run);
            yield return new NoneComparisonGenerator(run);
        }
    }
}
