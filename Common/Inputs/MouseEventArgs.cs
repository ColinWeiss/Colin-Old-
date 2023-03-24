using Colin.Common.Events;
using Microsoft.Xna.Framework.Input;

namespace Colin.Common.Inputs
{
    /// <summary>
    /// 鼠标事件类.
    /// </summary>
    public class MouseEventArgs : BasicEvent
    {
        /// <summary>
        /// 鼠标当前状态信息.
        /// </summary>
        public MouseState MouseState;

        public MouseEventArgs( MouseState mouseState )
        {
            MouseState = mouseState;
        }

    }
}