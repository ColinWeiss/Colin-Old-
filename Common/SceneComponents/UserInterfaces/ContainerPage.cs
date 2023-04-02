using Colin.Common.Inputs;
using Colin.Common.UserInterfaces.Prefabs;
using Colin.Common.UserInterfaces;
using Colin.Common.UserInterfaces.Renderers;
using Microsoft.Xna.Framework.Input;
using System.Runtime.Serialization;

namespace Colin.Common.UserInterfaces
{
    /// <summary>
    /// 指示一个容器页.
    /// </summary>
    [Serializable]
    [DataContract( IsReference = true )]
    public class ContainerPage : Container
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
            InteractiveInfo.CanSeek = false;
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

        public override void SelfUpdate( )
        {
            Seek( )?.EventResponder.UpdateEvent( );
            if( KeyboardResponder.Instance.IsKeyDown( Keys.LeftShift ) && KeyboardResponder.Instance.IsKeyClickBefore( Keys.U ) )
                OnDebug = !OnDebug;
            if( OnDebug && DebugText != null )
            {
                DebugText.Text = string.Concat(
                    "Current Container: ", SeekAll( )?.GetType( ).Name, "\n",
                    "    Size: ", SeekAll( )?.LayoutInfo.Size, "\n",
                    "    Absolute Location: ", SeekAll( )?.LayoutInfo.RenderLocation, "\n",
                    "    Location: ", SeekAll( )?.LayoutInfo.Location, "\n",
                    "    Interactive Rectangle: ", SeekAll( )?.LayoutInfo.InteractiveRectangle


                    );
                DebugText.Enable = true;
                DebugText.Visiable = true;
            }
            else if( DebugText != null )
            {
                DebugText.Enable = false;
                DebugText.Visiable = false;
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