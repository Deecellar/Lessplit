using System;
using System.Collections.Generic;

namespace LessplitCore.Plugins
{
    public class PluginManager
    {
        public static string BasePath { get; internal set; }
        public static string PATH_COMPONENTS { get; internal set; }
        public static IDictionary<string, PluginFactory> PluginFactories { get; internal set; }

        internal static PluginFactory LoadFactory(string localPath)
        {
            throw new NotImplementedException();
        }
    }
}
