using Microsoft.Xna.Framework.Input;

namespace Colin.Inputs
{
    /// <summary>
    /// 鼠标响应器.
    /// <br>[!] 已继承 <see cref="ISingleton"/>.</br>
    /// </summary>
    public sealed class MouseResponder : GameComponent , ISingleton
    {
        public MouseResponder( ) : base( EngineInfo.Engine ) { }

        public static bool MouseLeftClickBeforeFlag =>
            State.LeftButton == ButtonState.Pressed &&
            stateLast.LeftButton == ButtonState.Released;

        public static bool MouseLeftDownFlag =>
            State.LeftButton == ButtonState.Pressed &&
            stateLast.LeftButton == ButtonState.Pressed;

        public static bool MouseLeftUpFlag =>
            State.LeftButton == ButtonState.Released &&
            stateLast.LeftButton == ButtonState.Released;

        public static bool MouseLeftClickAfterFlag =>
           State.LeftButton == ButtonState.Released &&
            stateLast.LeftButton == ButtonState.Pressed;

        public static bool MouseRightClickBeforeFlag =>
            State.RightButton == ButtonState.Pressed &&
            stateLast.RightButton == ButtonState.Released;

        public static bool MouseRightDownFlag =>
            State.RightButton == ButtonState.Pressed &&
            stateLast.RightButton == ButtonState.Pressed;

        public static bool MouseRightUpFlag =>
             State.RightButton == ButtonState.Released &&
             stateLast.RightButton == ButtonState.Released;

        public static bool MouseRightClickAfterFlag =>
            State.RightButton == ButtonState.Released &&
            stateLast.RightButton == ButtonState.Pressed;

        public bool Enable { get; set; }

        /// <summary>
        /// 当前鼠标状态.
        /// </summary>
        public static MouseState State = new MouseState( );

        /// <summary>
        /// 上一帧鼠标状态.
        /// </summary>
        public static MouseState stateLast = new MouseState( );

        /// <summary>
        /// 鼠标位置.
        /// </summary>
        public static Vector2 Position => State.Position.ToVector2( );

        public override void Update( GameTime gameTime )
        {
            stateLast = State;
            State = Mouse.GetState( );
            base.Update( gameTime );
        }
    }
}