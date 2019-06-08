using LessplitCore.Configuration;
using LessplitCore.Run;

namespace LessplitCore.Comparators
{
    public interface IComparisonGenerator
    {
        IRun Run { get; set; }
        string Name { get; }
        void Generate(ISettings settings);
    }
}