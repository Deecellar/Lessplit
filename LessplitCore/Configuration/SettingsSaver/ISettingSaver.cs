using System.IO;

namespace LessplitCore.Configuration.SettingsSaver
{
    public interface ISettingsSaver
    {
        void Save(ISettings settings, Stream stream);
    }
}
