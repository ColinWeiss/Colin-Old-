using System.Runtime.Serialization;

namespace Colin.Common.SceneComponents.UserInterfaces
{
    /// <summary>
    /// 布局信息.
    /// </summary>
    [Serializable]
    [DataContract( Name = "LayoutInfo" )]
    public struct LayoutInfo
    {
        /// <summary>
        /// 容器相对于父容器的位置.
        /// </summary>
        public Point Location => new Point( Left, Top );

        /// <summary>
        /// 容器相对于父容器的位置.
        /// </summary>
        public Vector2 LocationF => new Vector2( Left, Top );

        private Point _renderLocation;
        /// <summary>
        /// 容器渲染位置.
        /// </summary>
        public Point RenderLocation => _renderLocation;

        /// <summary>
        /// 容器渲染位置.
        /// </summary>
        public Vector2 RenderLocationF => _renderLocation.ToVector2( );

        /// <summary>
        /// 上边缘相对于父容器的坐标.
        /// </summary>
        [DataMember]
        public int Top;

        /// <summary>
        /// 左边缘相对于父容器的坐标.
        /// </summary>
        [DataMember]
        public int Left;

        /// <summary>
        /// 容器宽度.
        /// </summary>
        [DataMember]
        public int Width;

        /// <summary>
        /// 容器高度.
        /// </summary>
        [DataMember]
        public int Height;

        /// <summary>
        /// 容器外左边缘大小.
        /// </summary>
        [DataMember]
        public int MarginLeft;

        /// <summary>
        /// 容器外右边缘大小.
        /// </summary>
        [DataMember]
        public int MarginRight;

        /// <summary>
        /// 容器外顶边缘大小.
        /// </summary>
        [DataMember]
        public int MarginTop;

        /// <summary>
        /// 容器外底边缘大小.
        /// </summary>
        [DataMember]
        public int MarginBottom;

        /// <summary>
        /// 容器内左边缘大小.
        /// </summary>
        [DataMember]
        public int PaddingLeft;

        /// <summary>
        /// 容器内右边缘大小.
        /// </summary>
        [DataMember]
        public int PaddingRight;

        /// <summary>
        /// 容器内顶边缘大小.
        /// </summary>
        [DataMember]
        public int PaddingTop;

        /// <summary>
        /// 容器内底边缘大小.
        /// </summary>
        [DataMember]
        public int PaddingBottom;

        private Vector2 LocationPercent = Vector2.Zero;

        private Vector2 SizePercent = Vector2.Zero;

        /// <summary>
        /// 获取容器大小.
        /// </summary>
        public Point Size => new Point( Width, Height );

        /// <summary>
        /// 获取容器大小.
        /// </summary>
        public Vector2 SizeF => new Vector2( Width, Height );

        public Rectangle RenderRectangle
        {
            get
            {
                Rectangle result = new Rectangle( );
                result.X = RenderLocation.X;
                result.Y = RenderLocation.Y;
                result.Width = Width;
                result.Height = Height;
                return result;
            }
        }

        public int InteractiveX;

        public int InteractiveY;

        public LayoutInfo( )
        {
            _renderLocation = Point.Zero;
            Top = 0;
            Left = 0;
            Width = 0;
            Height = 0;
            MarginLeft = 0;
            MarginRight = 0;
            MarginTop = 0;
            MarginBottom = 0;
            PaddingLeft = 0;
            PaddingRight = 0;
            PaddingTop = 0;
            PaddingBottom = 0;
            InteractiveX = 0;
            InteractiveY = 0;
        }

        public Rectangle InteractiveRectangle
        {
            get
            {
                Rectangle result = new Rectangle( );
                result.X = InteractiveX;
                result.Y = InteractiveY;
                result.Width = Width;
                result.Height = Height;
                return result;
            }
        }

        /// <summary>
        /// 事件: 发生在容器大小发生变化时
        /// </summary>
        public event Action OnSizeChanged = ( ) => { };

        /// <summary>
        /// 事件: 发生在容器位置发生变化时.
        /// </summary>
        public event Action OnLocationChanged = ( ) => { };

        public void SetLocation( int x, int y )
        {
            Left = x;
            Top = y;
            OnLocationChanged.Invoke( );
        }

        public void SetLocation( Point point ) => SetLocation( point.X, point.Y );

        public void SetLocation( Vector2 position ) => SetLocation( (int)position.X, (int)position.Y );

        bool NeedUpdateLocationFromPercent = false;
        public void SetLocationPercent( float x, float y )
        {
            LocationPercent = new Vector2( x, y );
            NeedUpdateLocationFromPercent = true;
        }

        public void SetWidth( int width )
        {
            Width = width;
            OnSizeChanged.Invoke( );
        }

        public void SetHeight( int height )
        {
            Height = height;
            OnSizeChanged.Invoke( );
        }

        public void SetSize( int width, int height )
        {
            Width = width;
            Height = height;
            OnSizeChanged.Invoke( );
        }

        public void SetSize( int size )
        {
            Width = size;
            Height = size;
            OnSizeChanged.Invoke( );
        }

        public void SetSize( Point size )
        {
            Width = size.X;
            Height = size.Y;
            OnSizeChanged.Invoke( );
        }

        public void SetSize( Vector2 size )
        {
            Width = (int)size.X;
            Height = (int)size.Y;
            OnSizeChanged.Invoke( );
        }

        bool NeedUpdateSizeFromPercent = false;
        public void SetSizePercent( float x, float y )
        {
            SizePercent = new Vector2( x, y );
            NeedUpdateSizeFromPercent = true;
        }

        public void SetTop( int top )
        {
            Top = top;
            OnLocationChanged.Invoke( );
        }

        public void SetLeft( int left )
        {
            Left = left;
            OnLocationChanged.Invoke( );
        }

        public void SetMarginLeft( int left )
        {
            MarginLeft = left;
        }

        public void SetMarginRight( int right )
        {
            MarginRight = right;
        }

        public void SetMarginTop( int top )
        {
            MarginTop = top;
        }

        public void SetMarginBottom( int bottom )
        {
            MarginBottom = bottom;
        }

        public void SetMargin( int left, int right, int top, int bottom )
        {
            SetMarginLeft( left );
            SetMarginRight( right );
            SetMarginTop( top );
            SetMarginBottom( bottom );
        }

        public void SetPaddingLeft( int left )
        {
            PaddingLeft = left;
        }

        public void SetPaddingRight( int right )
        {
            PaddingRight = right;
        }

        public void SetPaddingTop( int top )
        {
            PaddingTop = top;
        }

        public void SetPaddingBottom( int bottom )
        {
            PaddingBottom = bottom;
        }

        public void SetPadding( int left, int right, int top, int bottom )
        {
            SetPaddingLeft( left );
            SetPaddingRight( right );
            SetPaddingTop( top );
            SetPaddingBottom( bottom );
        }

        public void UpdateInfo( Container container )
        {
            if( container.Parent != null )
            {
                if( NeedUpdateLocationFromPercent )
                {
                    NeedUpdateLocationFromPercent = false;
                    SetLocation(
                        (int)(container.Parent.LayoutInfo.Width * LocationPercent.X),
                        (int)(container.Parent.LayoutInfo.Height * LocationPercent.Y) );
                    LocationPercent = Vector2.Zero;
                }
                if( NeedUpdateSizeFromPercent )
                {
                    NeedUpdateSizeFromPercent = false;
                    SetSize(
                        (int)(container.Parent.LayoutInfo.Width * SizePercent.X),
                        (int)(container.Parent.LayoutInfo.Height * SizePercent.Y) );
                    SizePercent = Vector2.Zero;
                }

                _renderLocation.X = container.Parent.LayoutInfo._renderLocation.X + Left;
                _renderLocation.Y = container.Parent.LayoutInfo._renderLocation.Y + Top;
                InteractiveX = container.Parent.LayoutInfo.InteractiveX + Left;
                InteractiveY = container.Parent.LayoutInfo.InteractiveY + Top;
                if( container.Parent.IsCanvas )
                {
                    _renderLocation.X -= container.Parent.LayoutInfo._renderLocation.X;
                    _renderLocation.Y -= container.Parent.LayoutInfo._renderLocation.Y;
                    InteractiveX = container.Parent.LayoutInfo._renderLocation.X + Left;
                    InteractiveY = container.Parent.LayoutInfo._renderLocation.Y + Top;
                }
                if( container.CanvasParent != null )
                {
                    InteractiveX = container.CanvasParent.LayoutInfo.InteractiveX + Left;
                    InteractiveY = container.CanvasParent.LayoutInfo.InteractiveY + Top;
                }
                if( container.CanvasParent != null && container.CanvasParent != container.Parent )
                {
                    InteractiveX = container.CanvasParent.LayoutInfo.InteractiveX + container.Parent.LayoutInfo._renderLocation.X + Left;
                    InteractiveY = container.CanvasParent.LayoutInfo.InteractiveY + container.Parent.LayoutInfo._renderLocation.Y + Top;
                }
            }
            else
            {
                _renderLocation.X = Left;
                _renderLocation.Y = Top;
                InteractiveX = Left;
                InteractiveY = Top;
            }
            //    InteractiveX -= container.DesignInfo.Origin.X / 2;
            //   InteractiveY -= container.DesignInfo.Origin.Y / 2;
        }

    }
}