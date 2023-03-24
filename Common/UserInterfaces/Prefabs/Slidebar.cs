using Colin.Common.Inputs;
using Colin.Developments;
using Colin.Common.UserInterfaces.Renderers;
using Colin.Extensions;
using Microsoft.Xna.Framework.Input;

namespace Colin.Common.UserInterfaces.Prefabs
{
    /// <summary>
    /// 滑动条.
    /// </summary>
    public class Slidebar : Container
    {
        /// <summary>
        /// 滑块.
        /// </summary>
        public Container Slider = new Container( );

        /// <summary>
        /// 每次滑动滚轮滑块移动的距离.
        /// </summary>
        public int ScrollValue = 2;

        /// <summary>
        /// 指示滑块到滑动条的位置百分比.
        /// </summary>
        public Vector2 Percentage
        {
            get
            {
                Vector2 denominator = LayoutInfo.SizeF - Slider.LayoutInfo.SizeF;
                Vector2 result = new Vector2( 0, 0 );
                if( denominator.X == 0 )
                    result.X = 0;
                else
                    result.X = Slider.LayoutInfo.Left / denominator.X;
                if( denominator.Y == 0 )
                    result.Y = 0;
                else
                    result.Y = Slider.LayoutInfo.Top / denominator.Y;
                return result;
            }
        }

        /// <summary>
        /// 指示受控的容器.
        /// </summary>
        public Container Controlled { get; private set; }

        /// <summary>
        /// 指示受控容器的对比容器.
        /// </summary>
        public Container ControlledStandard { get; private set; }

        private bool sliderDragState = false;

        public Direction Direction = Direction.Portrait;

        public override void ContainerInitialize( )
        {
            Slider.InteractiveInfo.CanSeek = true;
            Slider.InteractiveInfo.CanDrag = true;
            Register( Slider );
            EventResponder.MouseLeftClickBefore += ( s, e ) =>
            {
                sliderDragState = true;
            };
            base.ContainerInitialize( );
        }

        public override void LayoutInfoUpdate( ref LayoutInfo info )
        {
            if( Direction == Direction.Portrait )
                Slider.LayoutInfo.SetSize( info.Width, Slider.LayoutInfo.Height );
            else
                Slider.LayoutInfo.SetSize( Slider.LayoutInfo.Width, info.Height );

            Vector2 offset = Vector2.Zero;
            if( Controlled?.LayoutInfo.Height > ControlledStandard?.LayoutInfo.Height )
                offset.Y = ControlledStandard.LayoutInfo.Height - Controlled.LayoutInfo.Height;
            else
                offset.Y = 0;
            if( Controlled?.LayoutInfo.Width > ControlledStandard?.LayoutInfo.Width )
                offset.X = ControlledStandard.LayoutInfo.Width - Controlled.LayoutInfo.Width;
            else
                offset.X = 0;
            Vector2 setLocation = Percentage * offset;
            if( Controlled?.LayoutInfo.Width > 0 )
                setLocation.X = Math.Clamp( setLocation.X, ControlledStandard.LayoutInfo.Width - Controlled.LayoutInfo.Width, 0 );
            if( ControlledStandard.LayoutInfo.Height - Controlled.LayoutInfo.Height <= 0 )
                setLocation.Y = Math.Clamp( setLocation.Y, ControlledStandard.LayoutInfo.Height - Controlled.LayoutInfo.Height, 0 );

            Controlled?.LayoutInfo.SetLocation( setLocation );

            base.LayoutInfoUpdate( ref info );
        }

        public override void InteractiveInfoUpdate( ref InteractiveInfo info )
        {
            if( sliderDragState )
                Slider.LayoutInfo.SetLocation( EngineInfo.MousePositionF - LayoutInfo.InteractiveRectangle.Location.ToVector2( ) - Vector2.UnitY * Slider.LayoutInfo.Height / 2 );
            if( info.Activation || (ControlledStandard != null ? ControlledStandard.InteractiveInfo.Activation : false) )
            {
                if( KeyboardResponder.Instance.IsKeyDown( Keys.LeftShift ) )
                {
                    if( EngineInfo.MouseState.ScrollWheelValue > EngineInfo.MouseStateLast.ScrollWheelValue )
                        Slider.LayoutInfo.SetLeft( Slider.LayoutInfo.Left - ScrollValue );
                    else if( EngineInfo.MouseState.ScrollWheelValue < EngineInfo.MouseStateLast.ScrollWheelValue )
                        Slider.LayoutInfo.SetLeft( Slider.LayoutInfo.Left + ScrollValue );
                }
                else
                {
                    if( EngineInfo.MouseState.ScrollWheelValue > EngineInfo.MouseStateLast.ScrollWheelValue )
                        Slider.LayoutInfo.SetTop( Slider.LayoutInfo.Top - ScrollValue );
                    else if( EngineInfo.MouseState.ScrollWheelValue < EngineInfo.MouseStateLast.ScrollWheelValue )
                        Slider.LayoutInfo.SetTop( Slider.LayoutInfo.Top + ScrollValue );
                }
            }

            if( EngineInfo.MouseState.LeftButton == ButtonState.Released )
                sliderDragState = false;

            base.InteractiveInfoUpdate( ref info );
        }

        public override void SelfUpdate( )
        {
            Slider.LayoutInfo.SetLeft( Math.Clamp( Slider.LayoutInfo.Left, 0, LayoutInfo.Width - Slider.LayoutInfo.Width ) );
            Slider.LayoutInfo.SetTop( Math.Clamp( Slider.LayoutInfo.Top, 0, LayoutInfo.Height - Slider.LayoutInfo.Height ) );
            base.SelfUpdate( );
        }
        public void BindControlled( Container container )
        {
            Controlled = container;
        }

        public void BindControlledStandard( Container container )
        {
            ControlledStandard = container;
        }

        public Slidebar( )
        {
            Renderer = new PixelFillRenderer( );
            Slider.Renderer = new PixelFillRenderer( );
        }
    }
}