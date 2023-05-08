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
        public int Top
        {
            get => _top;
            set => SetTop( value );
        }
        private int _top;

        /// <summary>
        /// 左边缘相对于父容器的坐标.
        /// </summary>
        [DataMember]
        public int Left
        {
            get => _left;
            set => SetLeft( value );
        }
        private int _left;

        /// <summary>
        /// 容器宽度.
        /// </summary>
        [DataMember]
        public int Width
        {
            get => _width;
            set => SetWidth( value );
        }
        private int _width;

        /// <summary>
        /// 容器高度.
        /// <br>直接对该值赋值将不引发 <see cref="OnSizeChanged"/> 事件.</br>
        /// </summary>
        [DataMember]
        public int Height
        {
            get => _height;
            set => SetHeight( value );
        }
        private int _height;

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

        public int InteractiveX { get; private set; }

        public int InteractiveY { get; private set; }

        public LayoutInfo( )
        {
            _renderLocation = Point.Zero;
            _top = 0;
            _left = 0;
            _width = 0;
            _height = 0;
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

        Vector2 _locationFromPercentOffset = Vector2.Zero;
        bool NeedUpdateLocationFromPercent = false;
        public void SetLocationPercent( float x, float y , float offsetX = 0 , float offsetY = 0 )
        {
            _locationFromPercentOffset = new Vector2( offsetX , offsetY );
            LocationPercent = new Vector2( x , y );
            NeedUpdateLocationFromPercent = true;
        }

        /// <summary>
        /// 设置容器宽度.
        /// </summary>
        /// <param name="width">要设置的宽度.</param>
        public void SetWidth( int width )
        {
            if( _width != width )
            {
                _width = width;
                OnSizeChanged.Invoke( );
            }
        }

        /// <summary>
        /// 设置容器高度.
        /// </summary>
        /// <param name="height">要设置的高度.</param>
        public void SetHeight( int height )
        {
            _height = height;
            OnSizeChanged.Invoke( );
        }

        /// <summary>
        /// 设置大小.
        /// <br>只引发一次 <see cref="OnSizeChanged"/> 事件.</br>
        /// </summary>
        /// <param name="width">宽度.</param>
        /// <param name="height">高度.</param>
        public void SetSize( int width, int height )
        {
            if( _width != width || _height != height )
            {
                _width = width;
                _height = height;
                OnSizeChanged.Invoke( );
            }
        }

        /// <summary>
        /// 设置大小.
        /// <br>只引发一次 <see cref="OnSizeChanged"/> 事件.</br>
        /// </summary>
        /// <param name="size">长宽.</param>
        public void SetSize( int size )
        {
            SetSize( size );
        }

        /// <summary>
        /// 设置大小.
        /// <br>只引发一次 <see cref="OnSizeChanged"/> 事件.</br>
        /// </summary>
        /// <param name="size">长宽.</param>
        public void SetSize( Point size )
        {
            SetSize( size.X , size.Y );
        }

        /// <summary>
        /// 设置大小.
        /// <br>只引发一次 <see cref="OnSizeChanged"/> 事件.</br>
        /// </summary>
        /// <param name="size">长宽.</param>
        public void SetSize( Vector2 size )
        {
            SetSize( (int)size.X, (int)size.Y );
        }

        bool NeedUpdateSizeFromPercent = false;
        public void SetSizePercent( float x, float y )
        {
            SizePercent = new Vector2( x, y );
            NeedUpdateSizeFromPercent = true;
        }

        /// <summary>
        /// 设置容器顶部相对于父容器的位置.
        /// </summary>
        /// <param name="top">要设置的位置.</param>
        public void SetTop( int top )
        {
            if( _top != top )
            {
                _top = top;
                OnLocationChanged.Invoke( );
            }
        }

        /// <summary>
        /// 设置容器左部相对于父容器的位置.
        /// </summary>
        /// <param name="left">要设置的位置.</param>
        public void SetLeft( int left )
        {
            if( _left != left)
            {
                _left = left;
                OnLocationChanged.Invoke( );
            }
        }

        /// <summary>
        /// 设置容器相对于父容器的位置.
        /// <br>只引发一次 <see cref="OnLocationChanged"/> 事件.</br>
        /// </summary>
        /// <param name="top">顶部位置.</param>
        /// <param name="left">左部位置.</param>
        public void SetLocation( int left, int top )
        {
            if( _left != left || _top != top )
            {
                _left = left;
                _top = top;
                OnLocationChanged.Invoke( );
            }
        }

        /// <summary>
        /// 设置容器相对于父容器的位置.
        /// <br>只引发一次 <see cref="OnLocationChanged"/> 事件.</br>
        /// </summary>
        /// <param name="point">位置.</param>
        public void SetLocation( Point point ) => SetLocation( point.X, point.Y );

        /// <summary>
        /// 设置容器相对于父容器的位置.
        /// <br>只引发一次 <see cref="OnLocationChanged"/> 事件.</br>
        /// </summary>
        /// <param name="position">位置.</param>
        public void SetLocation( Vector2 position ) => SetLocation( (int)position.X, (int)position.Y );

        public void UpdateInfo( Container container )
        {
            if( container.Parent != null )
            {
                if( NeedUpdateLocationFromPercent )
                {
                    NeedUpdateLocationFromPercent = false;
                    SetLocation(
                        (int)(container.Parent.LayoutInfo.Width * LocationPercent.X + _locationFromPercentOffset.X)  ,
                        (int)(container.Parent.LayoutInfo.Height * LocationPercent.Y + _locationFromPercentOffset.Y) );
                    LocationPercent = Vector2.Zero;
                    _locationFromPercentOffset = Vector2.Zero;
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