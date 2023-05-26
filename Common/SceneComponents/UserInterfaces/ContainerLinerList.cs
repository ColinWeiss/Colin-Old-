using Colin.Common.SceneComponents.UserInterfaces.Prefabs;
using Colin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colin.Common.SceneComponents.UserInterfaces
{
    /// <summary>
    /// 线性容器列表.
    /// </summary>
    public class ContainerLinerList : CutContainer
    {
        /// <summary>
        /// 容器项之间的间隔.
        /// </summary>
        public int Interval = 6;

        /// <summary>
        /// 指示线性容器列表的方向.
        /// <br>[!] 该对象仅应用 <see cref="Direction.Portrait"/> 与 <see cref="Direction.Transverse"/>.</br>
        /// </summary>
        public Direction Direction = Direction.Portrait;

        /// <summary>
        /// 指示起始项是否需要进行间隔计算.
        /// </summary>
        public bool FirstItemInterval = false;

        /// <summary>
        /// 指示该线性容器列表所绑定的滑动条.
        /// </summary>
        private Scrollbar _slideBar;

        public override void ContainerInitialize( )
        {
            InteractiveInfo.CanSeek = false;
            base.ContainerInitialize( );
        }

        public override void LayoutInfoUpdate( ref LayoutInfo info )
        {
            Point Offset = Point.Zero;
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
            if( _slideBar != null )
            {
                Offset.X = (int)(_slideBar.Percentage.X * (contentSize.X - info.Width));
                Offset.Y = (int)(_slideBar.Percentage.Y * (contentSize.Y - info.Height));
            }
            Offset.X = Math.Clamp( Offset.X , 0 , int.MaxValue );
            Offset.Y = Math.Clamp( Offset.Y, 0, int.MaxValue );

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
                sub.LayoutInfoUpdate( ref sub.LayoutInfo );
                sub.LayoutInfo.UpdateInfo( sub );
                if( sub.LayoutInfo.RenderRectangle.Intersects( LayoutInfo.RenderRectangle ) )
                    sub.Active( true );
                else
                    sub.Disactive( true );
            }
            base.LayoutInfoUpdate( ref info );
        }

        public void BindSlideBar( Scrollbar slideBar )
        {
            _slideBar = slideBar;
            _slideBar.BindControlledStandard( this );
        }

    }
}