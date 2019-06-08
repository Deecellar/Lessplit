using LessplitCore.Configuration;
using LessplitCore.Plugins;
using System;
using System.Collections.Generic;

namespace LessplitCore.UIExt
{
    public interface ILayout : ICloneable
    {
        LayoutMode Mode { get; set; }
        int VerticalWidth { get; set; }
        int VerticalHeight { get; set; }
        int HorizontalWidth { get; set; }
        int HorizontalHeight { get; set; }
        int X { get; set; }
        int Y { get; set; }
        bool HasChanged { get; set; }
        string FilePath { get; set; }
        LayoutSettings Settings { get; set; }

        IList<IPlugin> LayoutPlugins { get; set; }
    }
}
