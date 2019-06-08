using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace LessplitCore.Plugins
{
    /// <summary>
    /// This is the plugin loader, for now it's a singleton class, since we only want to use it as a plugin manager
    /// for now it only have a list of plugins to Manage
    /// </summary>
    public class Loader
    {
        public static Loader Instance = new Loader();
        public List<IPlugin> Plugins { get; set; }
        private Loader()
        {

        }
        /// <summary>
        /// LoadPlugins Does just exactly what its name says, it loads .dll extensions and .py files, for now it cannot be extended
        /// But in the future we may want to support more kinds of plugins
        /// </summary>
        public void LoadPlugins()
        {
            Plugins = new List<IPlugin>();
            var PythonPlugins = new List<IPlugin>();
            //Load the DLLs from the Plugins directory
            if (Directory.Exists(PluginConstants.FolderName))
            {
                string[] files = Directory.GetFiles(PluginConstants.FolderName);
                foreach (string file in files)
                {
                    if (file.EndsWith(".dll"))
                    {
                        Assembly.LoadFile(Path.GetFullPath(file));
                    }
                    if (file.EndsWith(".py"))
                    {
                        PythonPlugins.Add(new PythonPlugin(file));
                    }
                }
                Type interfaceType = typeof(IPlugin);
                //Fetch all types that implement the interface IPlugin and are a class
                Type[] types = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(a => a.GetTypes())
                    .Where(p => interfaceType.IsAssignableFrom(p) && p.IsClass)
                    .Concat(PythonPlugins.Select(py => py.GetType()))
                    .ToArray();
                foreach (Type type in types)
                {
                    //Create a new instance of all found types
                    Plugins.Add((IPlugin)Activator.CreateInstance(type));
                }
            }
        }
    }
}
