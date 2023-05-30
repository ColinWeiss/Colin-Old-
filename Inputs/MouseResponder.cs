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
            state.LeftButton == ButtonState.Pressed &&
            stateLast.LeftButton == ButtonState.Released;

        public static bool MouseLeftDownFlag =>
            state.LeftButton == ButtonState.Pressed &&
            stateLast.LeftButton == ButtonState.Pressed;

        public static bool MouseLeftUpFlag =>
            state.LeftButton == ButtonState.Released &&
            stateLast.LeftButton == ButtonState.Released;

        public static bool MouseLeftClickAfterFlag =>
           state.LeftButton == ButtonState.Released &&
            stateLast.LeftButton == ButtonState.Pressed;

        public static bool MouseRightClickBeforeFlag =>
            state.RightButton == ButtonState.Pressed &&
            stateLast.RightButton == ButtonState.Released;

        public static bool MouseRightDownFlag =>
            state.RightButton == ButtonState.Pressed &&
            stateLast.RightButton == ButtonState.Pressed;

        public static bool MouseRightUpFlag =>
             state.RightButton == ButtonState.Released &&
             stateLast.RightButton == ButtonState.Released;

        public static bool MouseRightClickAfterFlag =>
            state.RightButton == ButtonState.Released &&
            stateLast.RightButton == ButtonState.Pressed;

        public bool Enable { get; set; }

        /// <summary>
        /// 当前鼠标状态.
        /// </summary>
        internal static MouseState state = new MouseState( );

        /// <summary>
        /// 上一帧鼠标状态.
        /// </summary>
        internal static MouseState stateLast = new MouseState( );

        /// <summary>
        /// 鼠标位置.
        /// </summary>
        public Vector2 Position => state.Position.ToVector2( );

        public override void Update( GameTime gameTime )
        {
            stateLast = state;
            state = Mouse.GetState( );

            base.Update( gameTime );
        }
    }
}