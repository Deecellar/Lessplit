using Eto.Forms;
using System;

namespace LessplitCore.Input
{
    public delegate void EventHandlerT<T>(object sender, T value);

    public class KeyOrButton
    {
        public bool IsButton { get; protected set; }
        public bool IsKey { get { return !IsButton; } set { IsButton = !value; } }

        public Keys Key { get; protected set; }

        public KeyOrButton(Keys key)
        {
            Key = key;
            IsKey = true;
        }


        public KeyOrButton(string stringRepresentation)
        {

            Key = (Keys)Enum.Parse(typeof(Keys), stringRepresentation, true);
            IsKey = true;

        }

        public override string ToString()
        {
            return Key.ToString();
        }

        public static bool operator ==(KeyOrButton a, KeyOrButton b)
        {
            if ((object)a == null && (object)b == null)
                return true;
            if ((object)a == null || (object)b == null)
                return false;

            if (a.IsKey && b.IsKey)
            {
                return a.Key == b.Key;
            }
            return false;
        }

        public static bool operator !=(KeyOrButton a, KeyOrButton b)
        {
            return !(a == b);
        }
    }

    public class CompositeHook
    {
        //protected LowLevelKeyboardHook KeyboardHook { get; set; }


        public event EventHandler<KeyEventArgs> KeyPressed;
        public event EventHandlerT<KeyOrButton> KeyOrButtonPressed;

        public CompositeHook()
        {
            //KeyboardHook = new LowLevelKeyboardHook();
            //KeyboardHook.KeyPressed += KeyboardHook_KeyPressed;
        }



        void KeyboardHook_KeyPressed(object sender, KeyEventArgs e)
        {
            KeyPressed?.Invoke(this, e);
            KeyOrButtonPressed?.Invoke(this, new KeyOrButton(e.Key | e.Modifiers));
        }



        //public void RegisterHotKey(Keys key)
        //{
        //    KeyboardHook.RegisterHotKey(key);
        //}





        //public void RegisterHotKey(KeyOrButton keyOrButton)
        //{
        //    if (keyOrButton.IsKey)
        //        RegisterHotKey(keyOrButton.Key);
        //}

        //public void Poll()
        //{
        //    KeyboardHook.Poll();
        //}

        //public void UnregisterAllHotkeys()
        //{
        //    RegisteredButtons.Clear();
        //    KeyboardHook.UnregisterAllHotkeys();
        //}
    }
}
