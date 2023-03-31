using Colin.Common;
using Colin.Common.UserInterfaces.Events;
using Colin.Common.UserInterfaces.Renderers;
using Colin.Developments;
using Colin.Extensions;
using Colin.Common.UserInterfaces.Prefabs;
using Microsoft.Xna.Framework.Input;
using System.ComponentModel;
using System.Runtime.Serialization;
using System;
using Colin.Common.UserInterfaces.Prefabs.Forms;

namespace Colin.Common.UserInterfaces
{
    /// <summary>
    /// 一个容器; 可用于在用户交互界面中表达具象.
    /// </summary>
    [Serializable]
    [DataContract( IsReference = true, Name = "Container" )]
    public class Container : IBehavior
    {
        [DataMember]
        public bool Enable { get; set; } = true;

        [DataMember]
        public bool Visiable { get; set; } = true;

        /// <summary>
        /// 可见性初始值: 若它的值为 false, 那么涉及到可见性性能优化相关的计算该容器将不会参与.
        /// </summary>
        public bool RealVisiable { get; set; } = true;

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
        /// 指示该容器所属的 <see cref="ContainerPage"/>.
        /// <br>[!] 一个容器页的 <see cref="Page"/> 是其本身.</br>
        /// </summary>
        [IgnoreDataMember]
        public ContainerPage Page { get; internal set; } = null;

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
            if( this is ContainerPage page )
                Page = page;
        }

        public void INI( )
        {
            EventResponder = new ContainerEventResponder( this );
            EventResponder.DragStart += Container_DragStart;
            EventResponder.Dragging += Container_DragDragging;
            EventResponder.DragEnd += Container_DragEnd;
            if( this is ContainerPage page )
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

            if( DesignInfo.ScaleConversionTimer < DesignInfo.ScaleConversionTime )
                DesignInfo.ScaleConversionTimer += 120f * dt;
            DesignInfo.CurrentScale.GetCloserVector2( DesignInfo.TargetScale, DesignInfo.ScaleConversionTimer, DesignInfo.ScaleConversionTime );

            if( DesignInfo.ColorConversionTimer < DesignInfo.ColorConversionTime )
                DesignInfo.ColorConversionTimer += 120f * dt;
            DesignInfo.CurrentColor.GetCloserColor( DesignInfo.TargetColor, DesignInfo.ColorConversionTimer, DesignInfo.ColorConversionTime );
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
                if( UserInterface != null )
                    _sub.UserInterface = UserInterface;
                if( CanvasParent != null )
                    _sub.CanvasParent = CanvasParent;
                if( IsCanvas )
                    _sub.CanvasParent = this;
                _sub.Page = Page;
                _sub.Parent = this;
                _sub.DoUpdate( time );
            }
        }

        public void DoRender( )
        {
            if( !Visiable )
                return;
            if( IsCanvas )
            {
                EngineInfo.SpriteBatch.End( );
                EngineInfo.Graphics.GraphicsDevice.SetRenderTarget( Canvas );
                EngineInfo.Graphics.GraphicsDevice.Clear( Color.Transparent );
                EngineInfo.SpriteBatch.Begin( SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap );
            }
            Renderer?.RenderSelf( this );
            Container _sub;
            for( int count = 0; count < Sub.Count; count++ )
            {
                _sub = Sub[count];
                _sub.DoRender( );
            }
            if( IsCanvas )
            {
                EngineInfo.SpriteBatch.End( );
                if( Parent != null && Parent.IsCanvas )
                    EngineInfo.Graphics.GraphicsDevice.SetRenderTarget( Parent.Canvas );
                else if( CanvasParent != null )
                    EngineInfo.Graphics.GraphicsDevice.SetRenderTarget( CanvasParent.Canvas );
                else
                    EngineInfo.Graphics.GraphicsDevice.SetRenderTarget( UserInterface.RenderTarget );

                (UserInterface as IRenderableSceneMode).BatchBegin( );
                if( Parent != null && Parent.IsCanvas )
                    EngineInfo.SpriteBatch.Draw( Canvas, LayoutInfo.LocationF + DesignInfo.OriginF, new Rectangle( 0, 0, LayoutInfo.Width, LayoutInfo.Height ), DesignInfo.CurrentColor, 0f, DesignInfo.OriginF, DesignInfo.CurrentScale, SpriteEffects.None, 1f );
                if( CanvasParent != null )
                    EngineInfo.SpriteBatch.Draw( Canvas, LayoutInfo.RenderLocationF + DesignInfo.OriginF, new Rectangle( 0, 0, LayoutInfo.Width, LayoutInfo.Height ), DesignInfo.CurrentColor, 0f, DesignInfo.OriginF, DesignInfo.CurrentScale, SpriteEffects.None, 1f );
                else
                    EngineInfo.SpriteBatch.Draw( Canvas, LayoutInfo.RenderLocationF + DesignInfo.OriginF, new Rectangle( 0, 0, LayoutInfo.Width, LayoutInfo.Height ), DesignInfo.CurrentColor, 0f, DesignInfo.OriginF, DesignInfo.CurrentScale, SpriteEffects.None, 1f );
            }
        }

        public virtual void Register( Container container )
        {
            container.Page = Page;
            container.Parent = this;
            Sub.Add( container );
        }

        /// <summary>
        /// 寻找允许交互的、当前最先交互元素.
        /// </summary>
        /// <returns>当前最先可交互元素.</returns>
        public Container Seek( )
        {
            Container target = null;
            Container _sub;
            for( int sub = Sub.Count - 1; sub >= 0; sub-- )
            {
                _sub = Sub[sub];
                if( _sub.Seek( ) == null )
                {
                    target = null;
                }
                else if( _sub.Seek( ) != null && _sub.Enable )
                {
                    target = _sub.Seek( );
                    return target;
                }
            }
            if( InteractiveInfo.Activation && InteractiveInfo.CanSeek && Enable )
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
                if( _sub.Seek( ) == null )
                {
                    target = null;
                }
                else if( _sub.Seek( ) != null && _sub.Enable )
                {
                    target = _sub.Seek( );
                    return target;
                }
            }
            if( InteractiveInfo.Activation && Enable )
                return this;
            return target;
        }

    }
}