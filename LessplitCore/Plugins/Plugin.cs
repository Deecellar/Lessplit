namespace LessplitCore.Plugins
{
    /// <summary>
    /// Interface for the implementation on any kind of plugin
    /// <see cref="Name"/> is to define the plugin name, Explanation is a little explanation that represents the plugin
    /// <see cref="Start"/>initializes everything you need for the plugin to work
    /// <see cref="Load"/> Load Makes the plugin work (adds the functionality you need to add)
    /// <see cref="Unload"/>  Unload it's to finish the plugin lifetime, I.E: Stop a server, kill a process, etc
    /// </summary>
    public interface IPlugin
    {

        string Name { get; set; }
        string Explanation { get; set; }
        void Start();
        void Load();
        void Unload();
    }
}
