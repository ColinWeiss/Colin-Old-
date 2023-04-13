using Colin.Common.SceneComponents.UserInterfaces.Prefabs;
using Colin.Common.SceneComponents.UserInterfaces.Renderers;
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
    public class ContainerState : Container, Inputable
    {
        public override sealed bool IsCanvas => false;

        public Rectangle BaseRectangle => new Rectangle( LayoutInfo.RenderLocation, LayoutInfo.Size );

        public Input InputHandle { get; set; } = new Input( );

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
            EventResponder.MouseLeftClickBefore += ( s, e ) =>
            {
                Input.BindInput( this );
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

        public override void SelfUpdate( )
        {
            Seek( )?.EventResponder.UpdateEvent( );
            if( InputHandle.Keyboard.IsKeyDown( Keys.LeftShift ) && InputHandle.Keyboard.IsKeyClickBefore( Keys.U ) )
                OnDebug = !OnDebug;
            if( InputHandle.Keyboard.IsKeyDown( Keys.LeftShift ) && InputHandle.Keyboard.IsKeyClickBefore( Keys.L ) )
                DoInitialize( );

            if( OnDebug && DebugText != null )
            {
                DebugText.Text = string.Concat(
                    "Current Container: ", Seek( )?.GetType( ).Name, "\n",
                    "    Size: ", Seek( )?.LayoutInfo.Size, "\n",
                    "    Absolute Location: ", Seek( )?.LayoutInfo.RenderLocation, "\n",
                    "    Location: ", Seek( )?.LayoutInfo.Location, "\n",
                    "    Interactive Rectangle: ", Seek( )?.LayoutInfo.InteractiveRectangle
                    );
                DebugText.Enable = true;
                DebugText.Visible = true;
            }
            else if( DebugText != null )
            {
                DebugText.Enable = false;
                DebugText.Visible = false;
            }
            base.SelfUpdate( );
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