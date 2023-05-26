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

        /// <summary>
        /// 指示当前选择的容器.
        /// </summary>
        public Container Selected;

        public int ContainerPointer = 0;

        public override sealed void ContainerInitialize( )
        {
            Renderer = new DefaultContainerRenderer( );
            InteractiveInfo.Activation = false;
            InteractiveInfo.CanDrag = false;
            InteractiveInfo.CanSeek = false;
            LayoutInfo.SetLocation( 0, 0 );
            LayoutInfo.SetSize( EngineInfo.ViewWidth, EngineInfo.ViewHeight );
            EngineInfo.Engine.Window.ClientSizeChanged += Window_ClientSizeChanged;
            InitializeContainers( );
            DebugText = new Label( );
            DebugText.Name = "ContainerPage DebugText";
            DebugText.DesignInfo.SetColor( Color.Red );
            DebugText.InteractiveInfo.CanSeek = false;
            Register( DebugText );

            base.ContainerInitialize( );
        }

        private void Window_ClientSizeChanged( object sender, EventArgs e )
        {
            LayoutInfo.SetLocation( 0, 0 );
            LayoutInfo.SetSize( EngineInfo.ViewWidth, EngineInfo.ViewHeight );
        }

        /// <summary>
        /// 在此处进行操作, 例如添加容器.
        /// </summary>
        public virtual void InitializeContainers( )
        {

        }

        public override void UpdateStart( )
        {
            if( ControllerResponder.state.IsConnected )
                Selected = GetCanSeekContainers( ).ToList( )[ContainerPointer];
            base.UpdateStart( );
        }

        public override void SelfUpdate( GameTime gameTime )
        {
            SeekInteractive( )?.EventResponder.UpdateEvent( );
            Selected?.EventResponder.UpdateEvent( );

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
                    "    Interactive Rectangle: ", SeekInteractive( )?.LayoutInfo.InteractiveRectangle , "\n",
                    "    Selected: " , Selected?.GetType( )
                    );
                DebugText.Enable = true;
                DebugText.Visible = true;

                if( ControllerResponder.DPad_Down_ClickBefore )
                {
                    ContainerPointer++;
                    ContainerPointer = Math.Clamp( ContainerPointer, 0, GetCanSeekContainers( ).Count( ) - 1 );
                    Selected = GetCanSeekContainers( ).ToList( )[ContainerPointer];
                    Input.SetInteractionPoint( Selected.LayoutInfo.InteractiveRectangle.Location + (Selected.LayoutInfo.InteractiveRectangle.Size.ToVector2( ) / 2).ToPoint( ) );
                }
                if( ControllerResponder.DPad_Up_ClickBefore )
                {
                    ContainerPointer--; 
                    ContainerPointer = Math.Clamp( ContainerPointer, 0, GetCanSeekContainers( ).Count( ) - 1 );
                    Selected = GetCanSeekContainers( ).ToList( )[ContainerPointer];
                    Input.SetInteractionPoint( Selected.LayoutInfo.InteractiveRectangle.Location + (Selected.LayoutInfo.InteractiveRectangle.Size.ToVector2( ) / 2).ToPoint( ) );
                }
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

        protected override void OnDispose( )
        {
            EngineInfo.Engine.Window.ClientSizeChanged -= Window_ClientSizeChanged;
            base.OnDispose( );
        }
    }
}