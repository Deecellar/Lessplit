using LessplitCore.Timing;
using System;
using System.Collections.Generic;

namespace LessplitCore.Comparators
{
    public interface IComparisons : IDictionary<string, Time>, ICloneable
    {
    }
}