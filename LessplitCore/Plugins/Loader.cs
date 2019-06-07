using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace LessplitCore.Plugins
{
    public class Loader
    {
        public static Loader Instance = new Loader();
        public List<IPlugin> Plugins { get; set; }
        private Loader()
        {

        }
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
