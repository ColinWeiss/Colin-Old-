using Colin.Common.SceneComponents.UserInterfaces.Renderers;

namespace Colin.Common.SceneComponents.UserInterfaces.Prefabs
{
    public class ProgressBar : Container
    {
        public float Progress = 0;

        public Container Fill = new Container( );

        public override void ContainerInitialize( )
        {
            InteractiveInfo.CanDrag = true;
            if( Renderer == null )
            {
                Renderer = new PixelFillRenderer( );
                DesignInfo.SetColor( Color.Gray );
            }
            if( Fill.Renderer == null )
            {
                Fill = new Container( );
                Fill.Renderer = new PixelFillRenderer( );
                Fill.DesignInfo.SetColor( Color.White );
            }
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