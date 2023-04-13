using Microsoft.Xna.Framework.Input;

namespace Colin.Inputs
{
    /// <summary>
    /// 鼠标响应器.
    /// </summary>
    public sealed class MouseResponder : GameComponent
    {
        private static MouseResponder _instance;
        public static MouseResponder Instance
        {
            get
            {
                if( _instance == null )
                    _instance = new MouseResponder( );
                return _instance;
            }
        }

        internal MouseResponder( ) : base( EngineInfo.Engine )
        {
            MouseLeftDown = ( s, e ) => { };
            MouseLeftUp = ( s, e ) => { };
            MouseLeftClickBefore = ( s, e ) => { };
            MouseLeftClickAfter = ( s, e ) => { };
            MouseRightDown = ( s, e ) => { };
            MouseRightUp = ( s, e ) => { };
            MouseRightClickBefore = ( s, e ) => { };
            MouseRightClickAfter = ( s, e ) => { };
            MouseRelease = ( s, e ) => { };
            MousePulleySliding = ( s, e ) => { };

        }

        private bool _mouseLeftClickBeforeFlag;
        public bool MouseLeftClickBeforeFlag => _mouseLeftClickBeforeFlag;

        private bool _mouseLeftDownFlag;
        public bool MouseLeftDownFlag => _mouseLeftDownFlag;

        private bool _mouseLeftUpFlag;
        public bool MouseLeftUpFlag => _mouseLeftUpFlag;

        private bool _mouseLeftClickAfterFlag;
        public bool MouseLeftClickAfterFlag => _mouseLeftClickAfterFlag;

        private bool _mouseRightClickBeforeFlag;
        public bool MouseRightClickBeforeFlag => _mouseRightClickBeforeFlag;

        private bool _mouseRightDownFlag;
        public bool MouseRightDownFlag => _mouseRightDownFlag;

        private bool _mouseRightUpFlag;
        public bool MouseRightUpFlag => _mouseRightUpFlag;

        private bool _mouseRightClickAfterFlag;
        public bool MouseRightClickAfterFlag => _mouseRightClickAfterFlag;

        public bool Enable { get; set; }

        /// <summary>
        /// 事件: 发生于鼠标左键单击按下时.
        /// </summary>
        public EventHandler<MouseEventArgs> MouseLeftClickBefore;

        /// <summary>
        /// 事件: 发生于鼠标左键保持按下时.
        /// </summary>
        public EventHandler<MouseEventArgs> MouseLeftDown;

        /// <summary>
        /// 事件: 发生于鼠标左键保持松开时.
        /// </summary>
        public EventHandler<MouseEventArgs> MouseLeftUp;

        /// <summary>
        /// 事件: 发生于鼠标左键单击松开时.
        /// </summary>
        public EventHandler<MouseEventArgs> MouseLeftClickAfter;

        /// <summary>
        /// 事件: 发生于鼠标右键单击按下时.
        /// </summary>
        public EventHandler<MouseEventArgs> MouseRightClickBefore;

        /// <summary>
        /// 事件: 发生于鼠标右键保持按下时.
        /// </summary>
        public EventHandler<MouseEventArgs> MouseRightDown;

        /// <summary>
        /// 事件: 发生于鼠标右键保持松开时.
        /// </summary>
        public EventHandler<MouseEventArgs> MouseRightUp;

        /// <summary>
        /// 事件: 发生于鼠标右键单击松开时.
        /// </summary>
        public EventHandler<MouseEventArgs> MouseRightClickAfter;

        /// <summary>
        /// 事件: 发生于鼠标松开时.
        /// </summary>
        public EventHandler<MouseEventArgs> MouseRelease;

        /// <summary>
        /// 事件: 发生于鼠标滑轮滑动时.
        /// </summary>
        public EventHandler<MouseEventArgs> MousePulleySliding;

        /// <summary>
        /// 当前鼠标状态.
        /// </summary>
        public MouseState MouseState = new MouseState( );

        /// <summary>
        /// 上一帧鼠标状态.
        /// </summary>
        public MouseState MouseStateLast = new MouseState( );

        /// <summary>
        /// 鼠标位置.
        /// </summary>
        public Vector2 Position => MouseState.Position.ToVector2( );

        /// <summary>
        /// 当前鼠标事件.
        /// </summary>
        public MouseEventArgs MouseEvent;

        public override void Update( GameTime gameTime )
        {
            MouseStateLast = MouseState;
            MouseState = Mouse.GetState( );
            MouseEvent = new MouseEventArgs( MouseState );
            _mouseLeftClickBeforeFlag = false;
            if( MouseState.LeftButton == ButtonState.Pressed && MouseStateLast.LeftButton == ButtonState.Released )
            {
                _mouseLeftClickBeforeFlag = true;
                MouseEvent.Name = "Event_Mouse_MouseLeftClickBefore";
                MouseLeftClickBefore.Invoke( this, MouseEvent );
            }
            _mouseLeftDownFlag = false;
            if( MouseState.LeftButton == ButtonState.Pressed && MouseStateLast.LeftButton == ButtonState.Pressed )
            {
                _mouseLeftDownFlag = true;
                MouseEvent.Name = "Event_Mouse_MouseLeftDown";
                MouseLeftDown.Invoke( this, MouseEvent );
            }
            _mouseLeftClickAfterFlag = false;
            if( MouseState.LeftButton == ButtonState.Released && MouseStateLast.LeftButton == ButtonState.Pressed )
            {
                _mouseLeftClickAfterFlag = true;
                MouseEvent.Name = "Event_Mouse_MouseLeftClickAfter";
                MouseLeftClickAfter.Invoke( this, MouseEvent );
            }
            _mouseLeftUpFlag = false;
            if( MouseState.LeftButton == ButtonState.Released && MouseStateLast.LeftButton == ButtonState.Released )
            {
                _mouseLeftUpFlag = true;
                MouseEvent.Name = "Event_Mouse_MouseLeftUp";
                MouseLeftUp.Invoke( this, MouseEvent );
            }
            _mouseRightClickBeforeFlag = false;
            if( MouseState.RightButton == ButtonState.Pressed && MouseStateLast.RightButton == ButtonState.Released )
            {
                _mouseRightClickBeforeFlag = true;
                MouseEvent.Name = "Event_Mouse_MouseRightClickBefore";
                MouseRightClickBefore.Invoke( this, MouseEvent );
            }
            _mouseRightDownFlag = false;
            if( MouseState.RightButton == ButtonState.Pressed && MouseStateLast.RightButton == ButtonState.Pressed )
            {
                _mouseRightDownFlag = true;
                MouseEvent.Name = "Event_Mouse_MouseRightDown";
                MouseRightDown.Invoke( this, MouseEvent );
            }
            _mouseRightClickAfterFlag = false;
            if( MouseState.RightButton == ButtonState.Released && MouseStateLast.RightButton == ButtonState.Pressed )
            {
                _mouseRightClickAfterFlag = true;
                MouseEvent.Name = "Event_Mouse_MouseRightClickAfter";
                MouseRightClickAfter.Invoke( this, MouseEvent );
            }
            _mouseRightUpFlag = false;
            if( MouseState.RightButton == ButtonState.Released && MouseStateLast.RightButton == ButtonState.Released )
            {
                _mouseRightUpFlag = true;
                MouseEvent.Name = "Event_Mouse_MouseRightUp";
                MouseRightUp.Invoke( this, MouseEvent );
            }
            if( MouseState.ScrollWheelValue != MouseStateLast.ScrollWheelValue )
            {
                MouseEvent.Name = "Event_Mouse_MousePulleySliding";
                MousePulleySliding.Invoke( this, MouseEvent );
            }
            base.Update( gameTime );
        }
    }
}