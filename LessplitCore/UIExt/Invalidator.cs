using Eto.Drawing;
using Eto.Forms;
using System;

namespace LessplitCore.UIExt
{
    public class Invalidator : IInvalidator
    {
        public Form Form { get; protected set; }
        public IMatrix Transform { get; set; }
        protected const double Offset = 0.535;

        public Invalidator(Form form)
        {
            Transform = Matrix.Create();
            Form = form;
        }

        public void Restart()
        {
            try
            {
                Transform?.Dispose();
            }
            catch { }

            Transform = Matrix.Create();
        }

        public void Invalidate(float x, float y, float width, float height)
        {
            var points = new[]
            {
                new PointF(x, y),
                new PointF(x+width, y+height)
            };
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = Transform.TransformPoint(points[i]);
            }
            var offsetX = points[0].X - Offset;
            var offsetY = points[0].Y - Offset;
            var rect = new Rectangle(
                (int)Math.Ceiling(offsetX),
                (int)Math.Ceiling(offsetY),
                (int)Math.Ceiling(points[1].X - offsetX - Offset),
                (int)Math.Ceiling(points[1].Y - offsetY - Offset));
            Form.Invalidate(rect);
        }
    }
}
