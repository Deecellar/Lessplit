using Eto.Drawing;

namespace LessplitCore.UIExt
{
    public static class ImageExtensions
    {
        public static Image ScaleIcon(this Image image)
        {
            return image.Scale(96);
        }

        public static Image Scale(this Image image, int maxDim)
        {
            if (image == null)
                return null;

            var width = image.Width;
            var height = image.Height;
            if (width <= maxDim && height <= maxDim)
                return image;

            using (image)
            {
                if (width > height)
                {
                    height = maxDim * height / width;
                    width = maxDim;
                }
                else
                {
                    width = maxDim * width / height;
                    height = maxDim;
                }

                var bitmap = new Bitmap(width, height, new Graphics(image.Handler as Graphics.IHandler));

                using (var graphics = new Graphics(bitmap))
                {

                    graphics.ImageInterpolation = ImageInterpolation.High;
                    graphics.DrawImage(
                        image,
                        new Rectangle(0, 0, width, height),
                        new Rectangle(0, 0, image.Width, image.Height));
                }


                return bitmap;
            }
        }
    }
}
