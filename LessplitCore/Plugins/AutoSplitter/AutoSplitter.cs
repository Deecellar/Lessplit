using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace LessplitCore.Plugins.AutoSplitter
{
    public enum AutoSplitterType
    {
        Component, Script
    }
    public class AutoSplitter : ICloneable
    {
        public string Description { get; set; }
        public IEnumerable<string> Games { get; set; }
        public bool IsActivated => Plugin != null;
        public List<string> URLs { get; set; }
        public string LocalPath => Path.GetFullPath(Path.Combine(PluginManager.BasePath ?? "", PluginManager.PATH_COMPONENTS, FileName));
        public string FileName => URLs.First().Substring(URLs.First().LastIndexOf('/') + 1);
        public AutoSplitterType Type { get; set; }
        public bool ShowInLayoutEditor { get; set; }
        public IPlugin Plugin { get; set; }
        public PluginFactory Factory { get; set; }
        public bool IsDownloaded => File.Exists(LocalPath);
        public string Website { get; set; }

        public void Activate(SplitterState state)
        {
            if (!IsActivated)
            {
                try
                {
                    if (!IsDownloaded || Type == AutoSplitterType.Script)
                        DownloadFiles();
                    if (Type == AutoSplitterType.Component)
                    {
                        Factory = PluginManager.PluginFactories[FileName];
                        Plugin = Factory.Create(state);
                    }
                    else
                    {
                        Factory = PluginManager.PluginFactories["LiveSplit.ScriptableAutoSplit.dll"];
                        Plugin = ((dynamic)Factory).Create(state, LocalPath);
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                    Log.Error("The Auto Splitter could not be activated. (" + ex.Message + ")", "Activation Failed");
                }
            }
        }

        private void DownloadFiles()
        {
            var client = new WebClient();

            foreach (var url in URLs)
            {
                var fileName = url.Substring(url.LastIndexOf('/') + 1);
                var tempFileName = fileName + "-temp";
                var localPath = Path.GetFullPath(Path.Combine(PluginManager.BasePath ?? "", PluginManager.PATH_COMPONENTS, fileName));
                var tempLocalPath = Path.GetFullPath(Path.Combine(PluginManager.BasePath ?? "", PluginManager.PATH_COMPONENTS, tempFileName));

                try
                {
                    // Download to temp file so the original file is kept if it fails downloading
                    client.DownloadFile(new Uri(url), tempLocalPath);
                    File.Copy(tempLocalPath, localPath, true);

                    if (url != URLs.First())
                    {
                        var factory = PluginManager.LoadFactory(localPath);
                        if (factory != null)
                            PluginManager.PluginFactories.Add(fileName, factory);
                    }
                }
                catch (WebException)
                {
                    Log.Error("Error downloading file from " + url);
                }
                catch (Exception ex)
                {
                    // Catch errors of File.Copy() if necessary
                    Log.Error(ex);
                }
                finally
                {
                    try
                    {
                        // This is not required to run the AutoSplitter, but should still try to clean up
                        File.Delete(tempLocalPath);
                    }
                    catch (Exception)
                    {
                        Log.Error($"Failed to delete temp file: {tempLocalPath}");
                    }
                }
            }

            if (Type == AutoSplitterType.Component)
            {
                var factory = PluginManager.LoadFactory(LocalPath);
                PluginManager.PluginFactories.Add(Path.GetFileName(LocalPath), factory);
            }
        }

        public void Deactivate()
        {
            if (IsActivated)
            {
                Plugin.Dispose();
                Plugin = null;
            }
        }

        public AutoSplitter Clone()
        {
            return new AutoSplitter()
            {
                Description = Description,
                Games = new List<string>(Games),
                URLs = new List<string>(URLs),
                Type = Type,
                ShowInLayoutEditor = ShowInLayoutEditor,
                Plugin = Plugin,
                Factory = Factory
            };
        }

        object ICloneable.Clone() => Clone();
    }
}
