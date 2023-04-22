using Colin.Common.SceneComponents.UserInterfaces.Prefabs;
using Colin.Common.SceneComponents.UserInterfaces.Renderers;
using Colin.Events;
using Colin.Inputs;
using Colin.Resources;
using Microsoft.Xna.Framework.Input;
using System.Runtime.Serialization;

namespace Colin.Common.SceneComponents.UserInterfaces
{
    /// <summary>
    /// 指示一个容器页.
    /// </summary>
    [Serializable]
    [DataContract( IsReference = true )]
    public class ContainerState : Container 
    {
        public override sealed bool IsCanvas => false;

        public Rectangle BaseRectangle => new Rectangle( LayoutInfo.RenderLocation, LayoutInfo.Size );

        public bool OnDebug = false;

        public Label DebugText;

        public override sealed void ContainerInitialize( )
        {
            Renderer = new DefaultContainerRenderer( );
            InteractiveInfo.Activation = false;
            InteractiveInfo.CanDrag = false;
            InteractiveInfo.CanSeek = true;
            LayoutInfo.SetLocation( 0, 0 );
            LayoutInfo.SetSize( EngineInfo.ViewWidth, EngineInfo.ViewHeight );
            EngineInfo.Engine.Window.ClientSizeChanged += ( s, e ) =>
            {
                LayoutInfo.SetLocation( 0, 0 );
                LayoutInfo.SetSize( EngineInfo.ViewWidth, EngineInfo.ViewHeight );
            };
            InitializeContainers( );

            DebugText = new Label( );
            DebugText.Name = "ContainerPage DebugText";
            DebugText.DesignInfo.SetColor( Color.Red );
            DebugText.InteractiveInfo.CanSeek = false;
            Register( DebugText );

            base.ContainerInitialize( );
        }

        /// <summary>
        /// 在此处进行操作, 例如添加容器.
        /// </summary>
        public virtual void InitializeContainers( )
        {

        }

        public override void SelfUpdate( GameTime gameTime )
        {
            SeekInteractive( )?.EventResponder.UpdateEvent( );
            if( KeyboardResponder.Instance.IsKeyDown( Keys.LeftShift ) && KeyboardResponder.Instance.IsKeyClickBefore( Keys.U ) )
                OnDebug = !OnDebug;
            if( KeyboardResponder.Instance.IsKeyDown( Keys.LeftShift ) && KeyboardResponder.Instance.IsKeyClickBefore( Keys.L ) )
                DoInitialize( );

            if( OnDebug && DebugText != null )
            {
                DebugText.Text = string.Concat(
                    "Current Container: ", SeekInteractive( )?.GetType( ).Name, "\n",
                    "    Size: ", SeekInteractive( )?.LayoutInfo.Size, "\n",
                    "    Absolute Location: ", SeekInteractive( )?.LayoutInfo.RenderLocation, "\n",
                    "    Location: ", SeekInteractive( )?.LayoutInfo.Location, "\n",
                    "    Interactive Rectangle: ", SeekInteractive( )?.LayoutInfo.InteractiveRectangle
                    );
                DebugText.Enable = true;
                DebugText.Visible = true;
            }
            else if( DebugText != null )
            {
                DebugText.Enable = false;
                DebugText.Visible = false;
            }
            base.SelfUpdate( gameTime );
        }

        public override void Register( Container container )
        {
            container.UserInterface = UserInterface;
            base.Register( container );
        }

        public void SetTop( Container container )
        {
            if( Sub.Remove( container ) )
            {
                Sub.Remove( DebugText );
                Register( container );
                Register( DebugText );
            }
        }

    }
}