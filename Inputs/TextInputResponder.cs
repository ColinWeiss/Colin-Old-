using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Colin.Inputs
{
    public class TextInputResponder : GameComponent , ISingleton
    {
        public event EventHandler<TextInputEventArgs> OnTextInput;

        public TextInputResponder( ) : base( EngineInfo.Engine ) 
        {
            Game.Window.TextInput += Window_TextInput; ;
        }

        private void Window_TextInput( object sender, TextInputEventArgs e )
        {
            OnTextInput?.Invoke( sender , e );
        }
    }
}