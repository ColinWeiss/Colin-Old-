using Colin.Common.UserInterfaces.Renderers;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colin.Common.UserInterfaces.Prefabs
{
    /// <summary>
    /// 拖动进度条.
    /// </summary>
    public class DragProgressBar : Container
    {
        public float Progress
        {
            get
            {
                return (float)Fill.LayoutInfo.Width / LayoutInfo.Width;
            }
            set
            {
                Fill.LayoutInfo.SetWidth( (int)(LayoutInfo.Width * value) );
            }
        }

        public Container Fill = new Container( );

        private bool dragState = false;

        public override void ContainerInitialize( )
        {
            if( Renderer == null )
                Renderer = new PixelFillRenderer( );
            DesignInfo.SetColor( Color.Gray );
            Fill.InteractiveInfo.CanSeek = false;
            if( Fill.Renderer == null )
                Fill.Renderer = new PixelFillRenderer( );
            Fill.DesignInfo.SetColor( Color.White );
            Register( Fill );

            EventResponder.MouseLeftClickBefore += ( s, e ) =>
            {
                dragState = true;
            };

            base.ContainerInitialize( );
        }

        public override void LayoutInfoUpdate( ref LayoutInfo info )
        {
            Fill.LayoutInfo.SetHeight( info.Height );
            base.LayoutInfoUpdate( ref info );
        }
        public override void InteractiveInfoUpdate( ref InteractiveInfo info )
        {
            if( dragState )
                Fill.LayoutInfo.SetWidth( (int)(EngineInfo.MousePositionF.X - LayoutInfo.InteractiveRectangle.Location.ToVector2( ).X) );
            Fill.LayoutInfo.SetWidth( Math.Clamp( Fill.LayoutInfo.Width, 0, LayoutInfo.Width ) );
            if( EngineInfo.MouseState.LeftButton == ButtonState.Released )
                dragState = false;
            base.InteractiveInfoUpdate( ref info );
        }
    }
}