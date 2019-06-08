using LessplitCore.Comparators;

namespace LessplitCore.Run.RunFactory
{
    public interface IRunFactory
    {
        IRun Create(IComparisonGeneratorsFactory factory);
    }
}
