using Colin.Developments;
using Colin.Common.UserInterfaces;
using Colin.Common.UserInterfaces.Renderers;

namespace Colin.Common.UserInterfaces.Prefabs.Lists
{
    public class UIList : Container
    {
        public Canvas View = new Canvas( );

        public LinerMenu LinerMenu = new LinerMenu( );

        public Slidebar Slidebar = new Slidebar( );

        public Direction Direction = Direction.Portrait;

        public override void ContainerInitialize( )
        {
            InteractiveInfo.CanSeek = false;
            if( Renderer == null )
                Renderer = new PixelFillRenderer( );
            View.LayoutInfo.SetSize( LayoutInfo.Size );
            View.LayoutInfo.SetLocation( 4, 4 );
            base.Register( View );

            LinerMenu.LayoutInfo.SetLocation( 0, 0 );
            LinerMenu.LayoutInfo.SetSize( LayoutInfo.Size );
            View.Register( LinerMenu );

            if( Direction == Direction.Portrait )
            {
                if( Slidebar.Slider.LayoutInfo.Height <= 0 )
                    Slidebar.Slider.LayoutInfo.SetHeight( 32 );
            }
            else
            {
                if( Slidebar.Slider.LayoutInfo.Width <= 0 )
                    Slidebar.Slider.LayoutInfo.SetWidth( 32 );
            }
            if( Direction == Direction.Portrait )
                Slidebar.LayoutInfo.SetLocation( View.LayoutInfo.Width + 8, 4 );
            else
                Slidebar.LayoutInfo.SetLocation( 4, View.LayoutInfo.Height + 8 );
            if( Direction == Direction.Portrait )
            {
                if( Slidebar.LayoutInfo.Width <= 0 )
                    Slidebar.LayoutInfo.SetSize( 16, LayoutInfo.Height );
            }
            else
            {
                if( Slidebar.LayoutInfo.Height <= 0 )
                    Slidebar.LayoutInfo.SetSize( LayoutInfo.Width, 16 );
            }
            Slidebar.BindControlled( LinerMenu );
            Slidebar.BindControlledStandard( View );
            base.Register( Slidebar );
            RefreshSize( );
            base.ContainerInitialize( );
        }

        /// <summary>
        /// 手动进行大小刷新.
        /// </summary>
        public void RefreshSize( )
        {
            if( Direction == Direction.Portrait )
                LayoutInfo.SetSize( LayoutInfo.Width + Slidebar.LayoutInfo.Width + 12, LayoutInfo.Height + 8 );
            else
                LayoutInfo.SetSize( LayoutInfo.Width + 8, LayoutInfo.Height + Slidebar.LayoutInfo.Height + 12 );
        }

        public override void Register( Container container ) => LinerMenu.Register( container );

    }
}