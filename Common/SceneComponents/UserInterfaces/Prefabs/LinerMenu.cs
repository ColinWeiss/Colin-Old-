namespace Colin.Common.SceneComponents.UserInterfaces.Prefabs
{
    public class LinerMenu : Container
    {
        /// <summary>
        /// 项与项之间的间隔.
        /// </summary>
        public int Interval = 6;

        public Direction Direction = Direction.Portrait;

        public bool Border = false;

        public void RefreshSize( )
        {
            int width = Direction == Direction.Portrait ? LayoutInfo.Width : 0;
            int height = Direction == Direction.Portrait ? 0 : LayoutInfo.Height;
            foreach( var item in Sub )
            {
                if( Direction == Direction.Portrait )
                {
                    if( width < item.LayoutInfo.Width )
                        width = item.LayoutInfo.Width;
                    height += item.LayoutInfo.Height + Interval;
                }
                else
                {
                    if( height < item.LayoutInfo.Height )
                        height = item.LayoutInfo.Height;
                    width += item.LayoutInfo.Width + Interval;
                }
            }
            LayoutInfo.SetWidth( width + (Border ? 2 * Interval : 0) );
            LayoutInfo.SetHeight( height + (Border ? 2 * Interval : 0) );
        }

        public override void ContainerInitialize( )
        {
            RefreshSize( );
            base.ContainerInitialize( );
        }

        public override void LayoutInfoUpdate( ref LayoutInfo info )
        {
            Container sub = null;
            Container lastSub = null;
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
                        sub.LayoutInfo.SetTop( 0 );
                }
                if( Direction == Direction.Transverse )
                {
                    sub.LayoutInfo.SetTop( 0 );
                    if( lastSub != null )
                        sub.LayoutInfo.SetLeft( lastSub.LayoutInfo.Width + lastSub.LayoutInfo.Left + Interval );
                    else
                        sub.LayoutInfo.SetLeft( 0 );
                }
            }
            base.LayoutInfoUpdate( ref info );
        }

        public override void SelfUpdate( GameTime gameTime )
        {
            RefreshSize( );
            base.SelfUpdate( gameTime );
        }

        public override void Register( Container container )
        {
            base.Register( container );
            RefreshSize( );
        }

    }
}