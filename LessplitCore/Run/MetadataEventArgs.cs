using System;

namespace LessplitCore.Run
{
    public class MetadataChangedEventArgs : EventArgs
    {
        public bool ClearRunID { get; set; }

        public MetadataChangedEventArgs(bool clearRunID)
        {
            ClearRunID = clearRunID;
        }
    }
}
