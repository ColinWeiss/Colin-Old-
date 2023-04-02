using Microsoft.Xna.Framework.Input;

namespace Colin.Common.Inputs
{
    /// <summary>
    /// 键盘响应器.
    /// <br>另两个键盘事件请使用 <see cref="Game.Window"/> 中的 <see cref="GameWindow.KeyDown"/> 与 <see cref="GameWindow.KeyUp"/>.</br>
    /// </summary>
    public sealed class KeyboardResponder : GameComponent
    {
        private static KeyboardResponder _instance;
        public static KeyboardResponder Instance
        {
            get
            {
                if( _instance == null )
                    _instance = new KeyboardResponder( );
                return _instance;
            }
        }

        internal KeyboardResponder( ) : base( EngineInfo.Engine ) { }

        /// <summary>
        /// 事件: 发生于键盘上任何键单击按下时.
        /// </summary>
        public EventHandler<KeyboardEventArgs> KeyClickBefore = ( s, e ) => { };

        /// <summary>
        /// 事件: 发生于键盘上任何键单击松开时.
        /// </summary>
        public EventHandler<KeyboardEventArgs> KeyClickAfter = ( s, e ) => { };

        internal KeyboardState keyboardState = new KeyboardState( );

        internal KeyboardState keyboardStateLast = new KeyboardState( );

        /// <summary>
        /// 当前键盘事件.
        /// </summary>
        public KeyboardEventArgs KeyboardEvent;

        public override void Update( GameTime gameTime )
        {
            keyboardStateLast = keyboardState;
            keyboardState = Keyboard.GetState( );
            KeyboardEvent = new KeyboardEventArgs( keyboardState );
            Keys[ ] pressedKeysLast = keyboardStateLast.GetPressedKeys( );
            foreach( Keys key in keyboardState.GetPressedKeys( ) )
            {
                if( !pressedKeysLast.Contains( key ) )
                {
                    KeyboardEvent.Name = "Event.Keyboard.KeyClickBefore";
                    KeyboardEvent.Keys = key;
                    KeyClickBefore.Invoke( this, KeyboardEvent );
                }
            }
            foreach( Keys keyLast in keyboardStateLast.GetPressedKeys( ) )
            {
                if( keyboardState.IsKeyUp( keyLast ) && keyboardStateLast.IsKeyDown( keyLast ) )
                {
                    KeyboardEvent.Name = "Event.Keyboard.KeyClickAfter";
                    KeyboardEvent.Keys = keyLast;
                    KeyClickAfter.Invoke( this, KeyboardEvent );
                }
            }
            base.Update( gameTime );
        }

        public bool IsKeyDown( Keys keys ) => keyboardState.IsKeyDown( keys );

        public bool IsKeyUp( Keys keys ) => keyboardState.IsKeyUp( keys );

        public bool IsKeyClickBefore( Keys keys ) => keyboardStateLast.IsKeyUp( keys ) && keyboardState.IsKeyDown( keys );

        public bool IsKeyClickAfter( Keys keys ) => keyboardStateLast.IsKeyDown( keys ) && keyboardState.IsKeyUp( keys );

    }
}