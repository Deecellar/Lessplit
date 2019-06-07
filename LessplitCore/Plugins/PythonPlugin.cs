using Python.Runtime;
using System;

namespace LessplitCore.Plugins
{
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
