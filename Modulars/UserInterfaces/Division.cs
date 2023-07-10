﻿using Colin.Extensions;
using Colin.Inputs;
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.XInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Colin.Modulars.UserInterfaces
{
    /// <summary>
    /// 指代用户交互界面中的一个划分元素.
    /// </summary>
    public class Division
    {
        /// <summary>
        /// 划分元素的名称.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// 指示划分元素是否可见.
        /// </summary>
        public bool IsVisible;

        /// <summary>
        /// 划分元素的布局样式
        /// </summary>
        public LayoutStyle Layout;

        /// <summary>
        /// 划分元素的剪裁样式.
        /// </summary>
        public ScissorStyle ScissorStyle;

        /// <summary>
        /// 划分元素的交互样式.
        /// </summary>
        public InteractStyle Interact;

        /// <summary>
        /// 划分元素的设计样式.
        /// </summary>
        public DesignStyle Design;

        /// <summary>
        /// 划分元素的事件响应器.
        /// </summary>
        public DivisionEventResponder EventResponder;

        private DivisionRenderer _renderer;
        /// <summary>
        /// 划分元素的渲染器.
        /// </summary>
        public DivisionRenderer Renderer => _renderer;
        public T BindRenderer<T>( ) where T : DivisionRenderer, new()
        {
            _renderer = new T( );
            _renderer._division = this;
            _renderer.RendererInit( );
            return _renderer as T;
        }
        public T GetRenderer<T>( ) where T : DivisionRenderer
        {
            if( _renderer is T )
                return _renderer as T;
            else
                return null;
        }

        public DivisionController Controller;

        /// <summary>
        /// 划分元素的父元素.
        /// </summary>
        public Division Parent;

        /// <summary>
        /// 划分元素的子元素列表.
        /// </summary>
        public List<Division> Children;

        public RenderTarget2D Canvas;

        internal UserInterface _interface;
        public UserInterface Interface => _interface;

        public virtual bool IsCanvas => false;

        /// <summary>
        /// 实例化一个划分元素, 并用名称加以区分.
        /// <br>[!] 虽然此处的名称可重复, 但该名称的作用是利于调试, 故建议使用不同的、可辨识的名称加以区分.</br>
        /// </summary>
        /// <param name="name">划分元素的名称.</param>
        public Division( string name )
        {
            Name = name;
            EventResponder = new DivisionEventResponder( this );
            EventResponder.DragStart += Container_DragStart;
            EventResponder.Dragging += Container_DragDragging;
            EventResponder.DragEnd += Container_DragEnd;
            Interact.IsInteractive = true;
            Design.Color = Color.White;
            Design.Scale = Vector2.One;
            IsVisible = true;
            Children = new List<Division>( );
            Controller = new DivisionController( this );
        }

        /// <summary>
        /// 执行划分元素的初始化内容.
        /// </summary>
        public void DoInitialize( )
        {
            OnInit( );
            _renderer?.RendererInit( );
            if( Parent != null )
                Layout.Calculation( Parent.Layout ); //刷新一下.
            ForEach( child => child.DoInitialize( ) );
        }
        /// <summary>
        /// 发生于划分元素执行 <see cref="DoInitialize"/> 时, 可于此自定义初始化操作.
        /// </summary>
        public virtual void OnInit( ) { }
        private Point _cachePos = new Point( -1, -1 );
        private void Container_DragStart( object o, DivisionEvent e )
        {
            if( Parent != null )
            {
                Point mouseForParentLocation = MouseResponder.State.Position - Parent.Layout.Location;
                _cachePos = mouseForParentLocation - Layout.Location;
            }
            else
            {
                _cachePos = MouseResponder.State.Position - Layout.Location;
            }
        }
        private void Container_DragDragging( object o, DivisionEvent e )
        {
            if( Parent != null )
            {
                Point _resultLocation = MouseResponder.State.Position - Parent.Layout.Location - _cachePos;
                Layout.Left = _resultLocation.X;
                Layout.Top = _resultLocation.Y;
            }
            else
            {
                Point _resultLocation = MouseResponder.State.Position - _cachePos;
                Layout.Left = _resultLocation.X;
                Layout.Top = _resultLocation.Y;
            }
        }
        private void Container_DragEnd( object o, DivisionEvent e )
        {
            _cachePos = new Point( -1, -1 );
        }

        /// <summary>
        /// 执行划分元素的逻辑刷新.
        /// </summary>
        /// <param name="time">游戏计时状态快照.</param>
        public void DoUpdate( GameTime time )
        {
            PreUpdate( time );
            if( !IsVisible )
                return;
            EventResponder.Independent( );
            Controller?.Layout( ref Layout );
            if( Parent != null )
                Layout.Calculation( Parent.Layout );
            Controller?.Interact( ref Interact );
            Controller?.Design( ref Design );
            OnUpdate( time );
            UpdateChildren( time );
        }
        /// <summary>
        /// 发生于 <see cref="DoUpdate"/> 执行时, 但不受 <see cref="IsVisible"/> 控制.
        /// <br>相较于 <see cref="UpdateChildren"/> 与 <see cref="OnUpdate"/> 最先执行.</br>
        /// </summary>
        /// <param name="time">游戏计时状态快照.</param>
        public virtual void PreUpdate( GameTime time ) { }
        /// <summary>
        /// 发生于 <see cref="DoUpdate"/> 执行时, 受 <see cref="IsVisible"/> 控制.
        /// <br>相较于 <see cref="UpdateChildren"/> 更快执行.</br>
        /// </summary>
        /// <param name="time">游戏计时状态快照.</param>
        public virtual void OnUpdate( GameTime time ) { }
        /// <summary>
        /// 为 <see cref="Children"/> 内元素执行其 <see cref="DoUpdate"/>.
        /// </summary>
        /// <param name="time">游戏计时状态快照.</param>
        public virtual void UpdateChildren( GameTime time )
        {
            Children.ForEach( child => { child?.DoUpdate( time ); } );
        }

        /// <summary>
        /// 执行划分元素的渲染.
        /// </summary>
        /// <param name="time">游戏计时状态快照.</param>
        public void DoRender( SpriteBatch batch )
        {
            if( !IsVisible && !Layout.IsHidden )
                return;
            if( IsCanvas )
            {
                batch.End( );
                EngineInfo.Graphics.GraphicsDevice.SetRenderTarget( Canvas );
                batch.Begin( SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, transformMatrix: Layout.CanvasTransform );
            }
            var overflowHiddenRasterizerState = new RasterizerState
            {
                CullMode = CullMode.None,
                ScissorTestEnable = true
            };
            var gd = batch.GraphicsDevice;
            var scissorRectangle = gd.ScissorRectangle;
            if( ScissorStyle.Enable )
            {
                batch.End( );
                batch.GraphicsDevice.RasterizerState = overflowHiddenRasterizerState;
                if( ScissorStyle.Scissor != Rectangle.Empty )
                    gd.ScissorRectangle = Rectangle.Intersect( gd.ScissorRectangle, ScissorStyle.Scissor );
                else
                    gd.ScissorRectangle = Rectangle.Intersect( gd.ScissorRectangle, Layout.TotalHitBox );
                batch.Begin( SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, overflowHiddenRasterizerState );
            }
            _renderer?.DoRender( batch );//渲染器进行渲染.
            RenderChildren( batch );
            if( ScissorStyle.Enable )
            {
                batch.End( );
                gd.RasterizerState = overflowHiddenRasterizerState;
                gd.ScissorRectangle = scissorRectangle;
                batch.Begin( SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, overflowHiddenRasterizerState );
            }
            if( IsCanvas )
            {
                batch.End( );
                if( Parent.IsCanvas )
                    EngineInfo.Graphics.GraphicsDevice.SetRenderTarget( Parent.Canvas );
                else
                    EngineInfo.Graphics.GraphicsDevice.SetRenderTarget( Interface.SceneRt );
                batch.Begin( SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp );
                batch.Draw( Canvas, Layout.LocationF + Design.Anchor, null, Design.Color, 0f, Design.Anchor, Design.Scale, SpriteEffects.None, 0f );
            }
        }
        /// <summary>
        /// 为 <see cref="Children"/> 内元素执行其 <see cref="DoRender"/>.
        /// </summary>
        /// <param name="time">游戏计时状态快照.</param>
        public virtual void RenderChildren( SpriteBatch spriteBatch )
        {
            Children.ForEach( child => { child?.DoRender( spriteBatch ); } );
        }

        /// <summary>
		/// 添加子元素.
		/// </summary>
		/// <param name="division">需要添加的划分元素.</param>
		/// <param name="doInit">执行添加划分元素的初始化.</param>
		/// <returns>若添加成功, 返回 <see langword="true"/>, 否则返回 <see langword="false"/>.</returns>
		public virtual bool Register( Division division, bool doInit = false )
        {
            if( division == null || Children.Contains( division ) || division.Parent != null )
                return false;
            division.Parent = this;
            if( doInit )
            {
                division.DoInitialize( );
                division.Layout.Calculation( Layout );
            }
            Children.Add( division );
            division._interface = Interface;
            return true;
        }

        /// <summary>
		/// 移除子元素.
		/// </summary>
		/// <param name="element">需要移除的划分元素.</param>
		/// <returns>若移除成功, 返回 <see langword="true"/>, 否则返回 <see langword="false"/>.</returns>
		public virtual bool Remove( Division element )
        {
            if( element == null || !Children.Contains( element ) || element.Parent == null )
                return false;
            element.Parent = null;
            return Children.Remove( element );
        }

        /// <summary>
        /// 移除所有子元素.
        /// </summary>
        public virtual void RemoveAll( )
        {
            Children.ForEach( child => child.Parent = null );
            Children.Clear( );
        }

        /// <summary>
		/// 遍历划分元素, 并执行传入方法.
		/// </summary>
		/// <param name="action">要执行的方法.</param>
		public void ForEach( Action<Division> action )
        {
            Children.ForEach( child => action( child ) );
        }

        /// <summary>
		/// 获取裁切过的碰撞箱.
		/// </summary>
		/// <returns>裁切过的碰撞箱.</returns>
		public virtual Rectangle GetScissorCalculation( )
        {
            if( Parent == null )
                return Rectangle.Intersect( new Rectangle( 0, 0, EngineInfo.ViewWidth, EngineInfo.ViewHeight ), Layout.HitBox );
            return Rectangle.Intersect( Rectangle.Intersect( Layout.HitBox, Parent.ScissorStyle.Scissor ), Parent.GetScissorCalculation( ) );
        }

        /// <summary>
		/// 获取此元素与父元素是否开启溢出隐藏的值.
		/// </summary>
		/// <returns>如果有则返回 <see langword="true"/>，否则返回 <see langword="false"/>.</returns>
		public bool GetDivsHidden( )
        {
            if( ScissorStyle.Enable )
                return true;
            if( Parent == null )
                return false;
            return Parent.GetDivsHidden( );
        }

        /// <summary>
		/// 判断该划分元素是否包含指定点.
		/// </summary>
		/// <param name="point">输入的点.</param>
		/// <returns>如果包含则返回 <see langword="true"/>，否则返回 <see langword="false"/>.</returns>
		public virtual bool ContainsPoint( Point point ) => (GetDivsHidden( ) ? GetScissorCalculation( ) : Layout.TotalHitBox).Contains( point );

        /// <summary>
        /// 返回该划分元素下最先可进行交互的元素.
        /// </summary>
        /// <returns>如果寻找到非该划分元素之外的元素, 则返回寻找到的元素; 否则返回自己.</returns>
        public virtual Division Seek( )
        {
            Division target = null;
            Division child;
            for( int sub = Children.Count - 1; sub >= 0; sub-- )
            {
                child = Children[sub];
                if( child.Seek( ) == null )
                    target = null;
                else if( child.Seek( ) != null && child.IsVisible )
                {
                    target = child.Seek( );
                    return target;
                }
            }
            if( Interact.IsInteractive && IsVisible && Interact.Interaction )
                return this;
            return target;
        }
    }
}