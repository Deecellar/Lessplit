namespace LessplitCore.Plugins
{
    public interface IPlugin
    {
        string Name { get; set; }
        string Explanation { get; set; }
        void Start();
        void Load();
        void Unload();
    }
}
