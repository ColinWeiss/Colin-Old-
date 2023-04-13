using Colin.Common.SceneComponents.UserInterfaces.Events;
using Colin.Common.SceneComponents.UserInterfaces.Prefabs.Forms;
using Colin.Common.SceneComponents.UserInterfaces.Renderers;
using Colin.Events;
using Colin.Extensions;
using Colin.Inputs;
using Colin.Resources;
using Microsoft.Xna.Framework.Input;
using System.Reflection.PortableExecutable;
using System.Runtime.Serialization;

namespace Colin.Common.SceneComponents.UserInterfaces
{
    /// <summary>
    /// 一个容器; 可用于在用户交互界面中表达具象.
    /// </summary>
    [Serializable]
    [DataContract( IsReference = true, Name = "Container" )]
    public class Container : IGetDeviceInputable
    {
        [DataMember]
        public bool Enable = true;

        [DataMember]
        public bool Visible = true;

        /// <summary>
        /// 容器名称.
        /// </summary>
        public string Name = "";

        /// <summary>
        /// 布局信息.
        /// </summary>
        [DataMember]
        public LayoutInfo LayoutInfo = new LayoutInfo( );

        /// <summary>
        /// 设计信息.
        /// </summary>
        [DataMember]
        public DesignInfo DesignInfo = new DesignInfo( );

        /// <summary>
        /// 交互信息.
        /// </summary>
        [DataMember]
        public InteractiveInfo InteractiveInfo = new InteractiveInfo( );

        /// <summary>
        /// 容器事件响应器.
        /// </summary>
        [DataMember]
        public ContainerEventResponder EventResponder;

        /// <summary>
        /// 指示该容器的父容器.
        /// </summary>
        [IgnoreDataMember]
        public Container Parent { get; internal set; } = null;

        /// <summary>
        /// 指示该容器的子容器集合.
        /// </summary>
        [DataMember]
        public List<Container> Sub { get; set; } = new List<Container>( );

        public virtual bool IsCanvas => false;

        /// <summary>
        /// 该容器绑定的 <see cref="UserInterfaces.UserInterface"/> 对象.
        /// </summary>
        [IgnoreDataMember]
        public UserInterface UserInterface;

        [IgnoreDataMember]
        public RenderTarget2D Canvas;

        /// <summary>
        /// 容器行为.
        /// </summary>
        public ContainerBehavior Behavior;

        /// <summary>
        /// 容器渲染器.
        /// <br>可自由定制自己的渲染方式.</br>
        /// </summary>
        [DataMember]
        public ContainerRenderer Renderer;

        /// <summary>
        /// 若容器的上级容器可寻到 <see cref="IsCanvas"/> 为 <see langword="true"/> 的容器, 
        /// <br>那么该值为可寻到的首个 <see cref="IsCanvas"/> 为 <see langword="true"/> 的容器.</br>
        /// <br>否则为 <see langword="null"/>.</br>
        /// <br>[!] 若容器本身 <see cref="IsCanvas"/> 为 <see langword="true"/>, 则该值为其本身.</br>
        /// </summary>
        [IgnoreDataMember]
        public Container CanvasParent { get; internal set; } = null;

        /// <summary>
        /// 指示该容器所属的 <see cref="ContainerState"/>.
        /// <br>[!] 一个容器页的 <see cref="Page"/> 是其本身.</br>
        /// </summary>
        [IgnoreDataMember]
        public ContainerState Page { get; internal set; } = null;

        public Container( )
        {
            EventResponder = new ContainerEventResponder( this );
            EventResponder.DragStart += Container_DragStart;
            EventResponder.Dragging += Container_DragDragging;
            EventResponder.DragEnd += Container_DragEnd;
            InteractiveInfo.Activation = false;
            InteractiveInfo.CanSeek = true;
            InteractiveInfo.CanDrag = false;
            DesignInfo.SetColor( Color.White );
            DesignInfo.SetScale( Vector2.One );
            DesignInfo.ColorConversionTime = 48;
            DesignInfo.ScaleConversionTime = 48;
            if( this is ContainerState page )
                Page = page;
        }

        /// <summary>
        /// 指示该容器是否已经执行过一次 <see cref="DoInitialize"/>;
        /// </summary>
        public bool InitializeComplete = false;

        public void DoInitialize( )
        {
            ContainerInitialize( );
            Renderer?.RendererInit( );
            LayoutInfo.UpdateInfo( this ); //刷新一下.

            Behavior?.SetDefault( );
            DoSubInitialize( );
            if( IsCanvas )
            {
                Canvas = RenderTargetExt.CreateDefault( LayoutInfo.Width, LayoutInfo.Height );
                LayoutInfo.OnSizeChanged += ( ) =>
                {
                    Canvas = RenderTargetExt.CreateDefault( LayoutInfo.Width, LayoutInfo.Height );
                };
            }
            InitializeComplete = true;
        }

        public void DoSubInitialize( )
        {
            Container _sub;
            for( int count = 0; count < Sub.Count; count++ )
            {
                _sub = Sub[count];
                if( UserInterface != null )
                    _sub.UserInterface = UserInterface;
                if( CanvasParent != null )
                    _sub.CanvasParent = CanvasParent;
                if( IsCanvas )
                    _sub.CanvasParent = this;
                _sub.DoInitialize( );
                _sub.LayoutInfo.UpdateInfo( _sub );
            }
        }

        /// <summary>
        /// 设置渲染器.
        /// 该方法无法在容器初始化完成前使用.
        /// </summary>
        /// <param name="containerRenderer">要设置的渲染器.</param>
        public void SetRenderer( ContainerRenderer containerRenderer )
        {
            if( InitializeComplete )
            {
                Renderer = containerRenderer;
                containerRenderer.RendererInit( );
            }
            else
                throw new Exception( "在容器初始化完成前设置初始化器." );
        }

        private Point _cachePos = new Point( -1, -1 );

        private void Container_DragStart( object o, ContainerEvent e )
        {
            if( Parent != null )
            {
                Point mouseForParentLocation = MouseState.Position - Parent.LayoutInfo.Location;
                _cachePos = mouseForParentLocation - LayoutInfo.Location;
            }
            else
            {
                _cachePos = MouseState.Position - LayoutInfo.RenderLocation;
            }
        }
        private void Container_DragDragging( object o, ContainerEvent e )
        {
            if( Parent != null )
            {
                Point _resultLocation = MouseState.Position - Parent.LayoutInfo.Location - _cachePos;
                LayoutInfo.SetLocation( _resultLocation.X, _resultLocation.Y );
            }
            else
            {
                Point _resultLocation = MouseState.Position - _cachePos;
                LayoutInfo.SetLocation( _resultLocation.X, _resultLocation.Y );
            }
        }
        private void Container_DragEnd( object o, ContainerEvent e )
        {
            _cachePos = new Point( -1, -1 );
        }
        public virtual void ContainerInitialize( )
        {

        }

        private MouseState MouseState => Mouse.GetState( );

        private bool Started = false;
        public void DoUpdate( GameTime time )
        {
            float dt = (float)time.ElapsedGameTime.TotalSeconds;
            if( !Enable )
                return;
            if( !Started )
            {
                UpdateStart( );
                Started = true;
            }
            LayoutInfoUpdate( ref LayoutInfo );
            LayoutInfo.UpdateInfo( this );

            InteractiveInfo.ActivationLast = InteractiveInfo.Activation;
            DesignInfoUpdate( ref DesignInfo );

            Rectangle _interactiveRec = LayoutInfo.InteractiveRectangle;
            InteractiveInfo.Activation = _interactiveRec.IntersectMouse( );
            InteractiveInfoUpdate( ref InteractiveInfo );
            EventResponder.UpdateIndependentEvent( );

            SelfUpdate( );
            Behavior?.UpdateFormStyle( );
            if( Behavior != null && Behavior.CloseState )
                Behavior?.UpdateCloseState( );

            if( DesignInfo.ScaleConversionTimer < DesignInfo.ScaleConversionTime )
                DesignInfo.ScaleConversionTimer += 120f * dt;
            DesignInfo.CurrentScale.GetCloserVector2( DesignInfo.TargetScale, DesignInfo.ScaleConversionTimer, DesignInfo.ScaleConversionTime );

            if( DesignInfo.ColorConversionTimer < DesignInfo.ColorConversionTime )
                DesignInfo.ColorConversionTimer += 120f * dt;
            DesignInfo.CurrentColor.GetCloserColor( DesignInfo.TargetColor, DesignInfo.ColorConversionTimer, DesignInfo.ColorConversionTime );
            if( DesignInfo.CurrentColor.A >= 254 )
                DesignInfo.CurrentColor.A = 255;
            SubUpdate( time );
        }
        public virtual void UpdateStart( ) { }
        public virtual void LayoutInfoUpdate( ref LayoutInfo info ) { }
        public virtual void DesignInfoUpdate( ref DesignInfo info ) { }
        public virtual void InteractiveInfoUpdate( ref InteractiveInfo info ) { }
        public virtual void SelfUpdate( ) { }
        public virtual void SubUpdate( GameTime time )
        {
            Container _sub;
            for( int count = Sub.Count - 1; count >= 0; count-- )
            {
                _sub = Sub[count];
                _sub.Page = Page;
                if( _sub.Enable )
                    _sub.DoUpdate( time );
            }
        }

        public void DoRender( )
        {
            if( !Visible )
                return;
            if( IsCanvas )
            {
                EngineInfo.SpriteBatch.End( );
                EngineInfo.Graphics.GraphicsDevice.SetRenderTarget( Canvas );
                EngineInfo.Graphics.GraphicsDevice.Clear( Color.Transparent );
                EngineInfo.SpriteBatch.Begin( SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap );
            }
            Renderer?.Render( this );
            PreRender( );
            RenderSubs( );
            PostRender( );
            if( IsCanvas )
            {
                EngineInfo.SpriteBatch.End( );
                if( Parent != null && Parent.IsCanvas )
                    EngineInfo.Graphics.GraphicsDevice.SetRenderTarget( Parent.Canvas );
                else if( CanvasParent != null )
                    EngineInfo.Graphics.GraphicsDevice.SetRenderTarget( CanvasParent.Canvas );
                else
                    EngineInfo.Graphics.GraphicsDevice.SetRenderTarget( UserInterface.RenderTarget );

                (UserInterface as IRenderableSceneComponent).BatchBegin( );
                if( Parent != null && Parent.IsCanvas )
                    EngineInfo.SpriteBatch.Draw( Canvas, LayoutInfo.LocationF + DesignInfo.OriginF, new Rectangle( 0, 0, LayoutInfo.Width, LayoutInfo.Height ), DesignInfo.CurrentColor, 0f, DesignInfo.OriginF, DesignInfo.CurrentScale, SpriteEffects.None, 1f );
                if( CanvasParent != null )
                    EngineInfo.SpriteBatch.Draw( Canvas, LayoutInfo.RenderLocationF + DesignInfo.OriginF, new Rectangle( 0, 0, LayoutInfo.Width, LayoutInfo.Height ), DesignInfo.CurrentColor, 0f, DesignInfo.OriginF, DesignInfo.CurrentScale, SpriteEffects.None, 1f );
                else
                    EngineInfo.SpriteBatch.Draw( Canvas, LayoutInfo.RenderLocationF + DesignInfo.OriginF, new Rectangle( 0, 0, LayoutInfo.Width, LayoutInfo.Height ), DesignInfo.CurrentColor, 0f, DesignInfo.OriginF, DesignInfo.CurrentScale, SpriteEffects.None, 1f );
            }
        }
        public virtual void PreRender( ) { }
        public virtual void RenderSubs( )
        {
            Container _sub;
            for( int count = 0; count < Sub.Count; count++ )
            {
                _sub = Sub[count];
                if( _sub.Visible )
                    _sub.DoRender( );
            }
        }
        public virtual void PostRender( ) { }

        public virtual void Active( bool subActive )
        {
            Enable = true;
            Visible = true;
            Behavior?.OnActive( );
            if( subActive )
            {
                Container _sub;
                for( int count = 0; count < Sub.Count; count++ )
                {
                    _sub = Sub[count];
                    _sub.Active( subActive );
                }
            }
        }

        public virtual void Disactive( bool subDisactive )
        {
            Enable = false;
            Visible = false;
            Behavior?.OnDisactive( );
            if( subDisactive )
            {
                Container _sub;
                for( int count = 0; count < Sub.Count; count++ )
                {
                    _sub = Sub[count];
                    _sub.Disactive( subDisactive );
                }
            }
        }

        public virtual void Register( Container container )
        {
            container.Page = Page;
            container.Parent = this;
            if( !Sub.Contains( container ) )
                Sub.Add( container );
        }

        public IEnumerable<Container> GetContainers( )
        {
            IEnumerable<Container> result = Sub;
            Container _sub;
            for( int count = 0; count < Sub.Count; count++ )
            {
                _sub = Sub[count];
                result = result.Concat( _sub.GetContainers( ) );
            }
            return result;
        }

        /// <summary>
        /// 寻找允许交互的、当前最先交互元素.
        /// </summary>
        /// <returns>当前最先可交互元素.</returns>
        public Container SeekInteractive( )
        {
            Container target = null;
            Container _sub;
            for( int sub = Sub.Count - 1; sub >= 0; sub-- )
            {
                _sub = Sub[sub];
                if( _sub.SeekInteractive( ) == null )
                {
                    target = null;
                }
                else if( _sub.SeekInteractive( ) != null && _sub.Enable )
                {
                    target = _sub.SeekInteractive( );
                    return target;
                }
            }
            if( InteractiveInfo.Activation && InteractiveInfo.CanSeek && Enable )
                return this;
            return target;
        }

        /// <summary>
        /// 寻找最顶层元素.
        /// </summary>
        public Container SeekTopmost( )
        {
            Container target = null;
            Container _sub;
            for( int sub = Sub.Count - 1; sub >= 0; sub-- )
            {
                _sub = Sub[sub];
                if( _sub.SeekTopmost( ) == null )
                {
                    target = null;
                }
                else if( _sub.SeekTopmost( ) != null && _sub.Enable )
                {
                    target = _sub.SeekTopmost( );
                    return target;
                }
            }
            if( InteractiveInfo.CanSeek && Enable )
                return this;
            return target;
        }

        /// <summary>
        /// 寻找当前最先交互元素, 无论其 <see cref="InteractiveInfo.CanSeek"/> 是否为 <see langword="true"/>.
        /// </summary>
        /// <returns></returns>
        public Container SeekAll( )
        {
            Container target = null;
            Container _sub;
            for( int sub = Sub.Count - 1; sub >= 0; sub-- )
            {
                _sub = Sub[sub];
                if( _sub.SeekInteractive( ) == null )
                {
                    target = null;
                }
                else if( _sub.SeekInteractive( ) != null && _sub.Enable )
                {
                    target = _sub.SeekInteractive( );
                    return target;
                }
            }
            if( InteractiveInfo.Activation && Enable )
                return this;
            return target;
        }

        public virtual InputEvent GetDeviceInput( )
        {
            InputEvent target = null;
            InputEvent _sub;
            for( int sub = Sub.Count - 1; sub >= 0; sub-- )
            {
                _sub = Sub[sub].GetDeviceInput( );
                if( _sub == null )
                {
                    target = null;
                }
                else if( _sub != null )// && _sub.Enable )
                {
                    target = _sub;
                    return target;
                }
            }
            if( InteractiveInfo.CanSeek && Enable )
            {
                InputEvent inputEvent = new InputEvent( );
                inputEvent.Keyboard = KeyboardResponder.Instance;
                inputEvent.Mouse = MouseResponder.Instance;
                return inputEvent;
            }
            return target;
        }
    }
}