namespace Colin.Common.SceneComponents.UserInterfaces.Prefabs.Lists
{
    public class UIListGeneralView : ScRecContainer
    {
        /// <summary>
        /// 项与项之间的间隔.
        /// </summary>
        public int Interval = 6;

        public Direction Direction = Direction.Portrait;

        public bool Border = false;

        public Point Offset = Point.Zero;

        public Slidebar SlideBar;

        public override void LayoutInfoUpdate( ref LayoutInfo info )
        {
            Container sub = null;
            Container lastSub = null;
            Point contentSize = Point.Zero;
            for( int count = 0; count < Sub.Count; count++ )
            {
                sub = Sub[count];
                if( Direction == Direction.Portrait )
                    contentSize.Y += sub.LayoutInfo.Height + Interval;
                else
                    contentSize.X += sub.LayoutInfo.Width + Interval;
            }
            if( SlideBar != null )
            {
                Offset.X = (int)(SlideBar.Percentage.X * (contentSize.X - info.Width));
                Offset.Y = (int)(SlideBar.Percentage.Y * (contentSize.Y - info.Height));
            }
            for( int count = 0; count < Sub.Count; count++ )
            {
                if( count > 0 )
                    lastSub = Sub[count - 1];
                sub = Sub[count];
                if( Direction == Direction.Portrait )
                {
                    sub.LayoutInfo.SetLeft( 0 );
                    if( lastSub != null )
                        sub.LayoutInfo.SetTop( lastSub.LayoutInfo.Height + lastSub.LayoutInfo.Top + Interval );
                    else
                        sub.LayoutInfo.SetTop( -Offset.Y );
                }
                if( Direction == Direction.Transverse )
                {
                    sub.LayoutInfo.SetTop( 0 );
                    if( lastSub != null )
                        sub.LayoutInfo.SetLeft( lastSub.LayoutInfo.Width + lastSub.LayoutInfo.Left + Interval );
                    else
                        sub.LayoutInfo.SetLeft( -Offset.X );
                }

                if( !sub.LayoutInfo.RenderRectangle.Intersects( info.RenderRectangle ) )
                {
                    sub.LayoutInfo.UpdateInfo( sub );
                    sub.Enable = false;
                    sub.Visible = false;
                }
                else
                {
                    sub.Enable = true;
                    sub.Visible = true;
                }
            }
            base.LayoutInfoUpdate( ref info );
        }

        public void BindSlideBar( Slidebar slideBar )
        {
            SlideBar = slideBar;
        }

    }
}