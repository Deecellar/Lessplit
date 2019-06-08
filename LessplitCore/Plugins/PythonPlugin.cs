using Python.Runtime;
using System;

namespace LessplitCore.Plugins
{
    /// <summary>
    /// A naive implementation of a python plugin, the idea is that it 
    /// loads with pythonnet the code and just execute the respective python code
    /// <see cref="Plugin"/> as the Pyobject that represents the code
    /// <see cref="File"/> The file to load the code from
    /// <see cref="Lock"/> it's an Int pointer that PythonEngine Uses, use it to dispose the lock on the engine
    /// </summary>
    public class PythonPlugin : IPlugin
    {
        PyObject Plugin;
        string File;
        IntPtr Lock;
        public PythonPlugin(string File)
        {
            PythonEngine.Initialize();
            Lock = PythonEngine.AcquireLock();
            this.File = File;
        }

        public string Name { get; set; }
        public string Explanation { get; set; }

        public void Load()
        {
            if (Lock == null)
            {
                return;
            }
            using (Py.GIL())
            {
                Plugin = PythonEngine.ImportModule(File);
                Plugin.InvokeMethod("Load");
                Name = Plugin.GetAttr("Name").As<string>();
                Explanation = Plugin.GetAttr("Explanation").As<string>();
            }
        }

        public void Start()
        {
            if (Lock == null)
            {
                return;
            }
            using (Py.GIL())
            {
                Plugin.InvokeMethod("Start");

            }
        }

        public void Unload()
        {
            if (Lock == null)
            {
                return;
            }
            using (Py.GIL())
            {
                Plugin.InvokeMethod("Unload");

            }
            PythonEngine.ReleaseLock(Lock);
        }
    }
}
