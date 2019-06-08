using System;

namespace LessplitCore.UIExt
{
    public class RenameEventArgs : EventArgs
    {
        public string OldName { get; set; }
        public string NewName { get; set; }
    }
}
