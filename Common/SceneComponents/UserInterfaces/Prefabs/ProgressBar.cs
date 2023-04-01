using Colin.Common.UserInterfaces.Renderers;

namespace Colin.Common.UserInterfaces.Prefabs
{
    public class ProgressBar : Container
    {
        public float Progress = 0;

        public Container Fill = new Container( );

        public override void ContainerInitialize( )
        {
            InteractiveInfo.CanDrag = true;
            Renderer = new PixelFillRenderer( );
            DesignInfo.SetColor( Color.Gray );
            Fill = new Container( );
            Fill.Renderer = new PixelFillRenderer( );
            Fill.DesignInfo.SetColor( Color.White );
            Register( Fill );
            base.ContainerInitialize( );
        }
        public override void LayoutInfoUpdate( ref LayoutInfo info )
        {
            Fill.LayoutInfo.SetHeight( info.Height );
            Fill.LayoutInfo.SetWidth( (int)(Progress * info.Width) );
            base.LayoutInfoUpdate( ref info );
        }
    }
}