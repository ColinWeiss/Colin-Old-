using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Colin.Inputs
{
    public class TextInputResponder : GameComponent
    {
        private static TextInputResponder _instance;
        public static TextInputResponder Instance
        {
            get
            {
                if( _instance == null )
                    _instance = new TextInputResponder( EngineInfo.Engine );
                return _instance;
            }
        }

        public event EventHandler<TextInputEventArgs> OnTextInput;

        public TextInputResponder( Game game ) : base( game ) 
        {
            game.Window.TextInput += Window_TextInput; ;
        }

        private void Window_TextInput( object sender, TextInputEventArgs e )
        {
            OnTextInput?.Invoke( sender , e );
        }
    }
}