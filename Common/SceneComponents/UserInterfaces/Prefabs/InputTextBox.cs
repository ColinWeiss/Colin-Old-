using Colin.Inputs;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colin.Common.SceneComponents.UserInterfaces.Prefabs
{
    public class InputTextBox : CutContainer
    {
        public StringBuilder Text = new StringBuilder( );

        public Label Label = new Label( );

        public string DisplayText;

        public float Timer = 0;

        public bool CanEnter = false;

        public override void ContainerInitialize( )
        {
            Label.LayoutInfo.SetLocation( 4 , 4 );
            Singleton<TextInputResponder>.Instance.OnTextInput += Instance_OnTextInput;
            Register( Label );
            base.ContainerInitialize( );
        }

        private Keys[ ] sss = new Keys[ ]
        {
            Keys.Back,
            Keys.Enter,
            Keys.Tab
        };

        private void Instance_OnTextInput( object sender, TextInputEventArgs e )
        {
            if( UserInterface.CurrentFocu == this )
            {
                if( e.Key == Keys.Back && Text.ToString( ) != string.Empty )
                    Text.Remove( Text.Length - 1, 1 );
                else if( !sss.Contains( e.Key ) )
                    Text.Append( e.Character );
                else if( e.Key == Keys.Tab )
                    Text.Append( "    " );
                else if( e.Key == Keys.Enter && CanEnter )
                    Text.Append( "\n" );
            }
        }

        public override void SelfUpdate( GameTime gameTime )
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if( UserInterface.CurrentFocu == this )
            {
                Timer += dt;
                if( Timer >= 0 )
                    DisplayText = Text.ToString( );
                if( Timer >= 0.5f )
                    DisplayText = Text.ToString( ) + "|";
                if( Timer >= 1 )
                    Timer = 0;
                Label.Text = DisplayText;
            }
            else
            {
                DisplayText = Text.ToString( );
                Label.Text = DisplayText;
            }
            base.SelfUpdate( gameTime );
        }
    }
}