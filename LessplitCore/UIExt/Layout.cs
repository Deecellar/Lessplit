using LessplitCore.Configuration;
using LessplitCore.Plugins;
using System.Collections.Generic;

namespace LessplitCore.UIExt
{
    public class Layout : ILayout
    {
        public const int InvalidSize = -1;

        public LayoutSettings Settings { get; set; }
        public IList<IPlugin> LayoutPlugins { get; set; }

        public LayoutMode Mode { get; set; }

        public Layout()
        {
            LayoutPlugins = new List<IPlugin>();
        }

        public int VerticalWidth { get; set; }
        public int VerticalHeight { get; set; }
        public int HorizontalWidth { get; set; }
        public int HorizontalHeight { get; set; }

        public string FilePath { get; set; }

        public int X { get; set; }
        public int Y { get; set; }
        public bool HasChanged { get; set; }

        public object Clone()
        {
            return new Layout()
            {
                LayoutPlugins = new List<IPlugin>(LayoutPlugins),
                VerticalWidth = VerticalWidth,
                VerticalHeight = VerticalHeight,
                HorizontalWidth = HorizontalWidth,
                HorizontalHeight = HorizontalHeight,
                FilePath = FilePath,
                X = X,
                Y = Y,
                HasChanged = HasChanged,
                Settings = (LayoutSettings)Settings.Clone(),
                Mode = Mode
            };
        }
    }
}
