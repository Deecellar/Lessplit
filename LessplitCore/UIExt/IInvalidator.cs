using Eto.Drawing;

namespace LessplitCore.UIExt
{
    public interface IInvalidator
    {
        IMatrix Transform { get; set; }
        void Invalidate(float x, float y, float width, float height);
    }
}
