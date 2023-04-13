using Colin.Events;
using Microsoft.Xna.Framework.Input;

namespace Colin.Inputs
{
    public class KeyboardEventArgs : BasicEvent
    {
        /// <summary>
        /// 本次事件与之相关的键位.
        /// </summary>
        public Keys Keys;

        public KeyboardState KeyboardState;

        public KeyboardEventArgs( KeyboardState keyboardState )
        {
            KeyboardState = keyboardState;
        }

    }
}