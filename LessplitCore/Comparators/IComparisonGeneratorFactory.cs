using LessplitCore.Run;
using System.Collections.Generic;

namespace LessplitCore.Comparators
{
    public interface IComparisonGeneratorsFactory
    {
        IEnumerable<IComparisonGenerator> Create(IRun run);
    }
}
