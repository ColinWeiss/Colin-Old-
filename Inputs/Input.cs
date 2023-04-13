using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colin.Inputs
{
    public class Input
    {
        public static Inputable Current;

        public MouseResponder Mouse = new MouseResponder( );

        public KeyboardResponder Keyboard = new KeyboardResponder( );

        public void UpdateInput( GameTime gameTime )
        {
            Mouse.Update( gameTime );
            Keyboard.Update( gameTime );
        }

        /// <summary>
        /// 绑定输入.
        /// </summary>
        /// <param name="inputable">很喜欢Mod制作者的一句话: 啊?</param>
        public static void BindInput( Inputable inputable )
        {
            Current = inputable;
            if( inputable.InputHandle == null )
                inputable.InputHandle = new Input( );
            inputable.InputHandle.Mouse = new MouseResponder( );
            inputable.InputHandle.Keyboard = new KeyboardResponder( );
        }

    }
}