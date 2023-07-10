﻿using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colin.Inputs
{
    public sealed class Input : GameComponent, ISingleton
    {
        /// <summary>
        /// 交互 激活开始时 操作.
        /// </summary>
        public static bool Interactive1_Before => MouseResponder.MouseLeftClickBeforeFlag || ControllerResponder.Button_A_ClickBefore;
        /// <summary>
        /// 交互 激活结束时 操作.
        /// </summary>
        public static bool Interactive1_After => MouseResponder.MouseLeftClickAfterFlag || ControllerResponder.Button_A_ClickAfter;
        /// <summary>
        /// 交互 持续激活 操作.
        /// </summary>
        public static bool Interactive1_Down => MouseResponder.MouseLeftDownFlag || ControllerResponder.Button_A_Pressed;
        /// <summary>
        /// 交互 无激活 操作.
        /// </summary>
        public static bool Interactive1_Up => MouseResponder.MouseLeftUpFlag && ControllerResponder.Button_A_Released;

        /// <summary>
        /// 其他交互 操作.
        /// </summary>
        public static bool Interactive2 = false;

        /// <summary>
        /// 返回 操作.
        /// </summary>
        public static bool Back = false;

        /// <summary>
        /// 退出 操作.
        /// </summary>
        public static bool Exit = false;

        public static float Wipe
        {
            get
            {
                if( MouseResponder.State.ScrollWheelValue < MouseResponder.stateLast.ScrollWheelValue )
                    return 1;
                else if( MouseResponder.State.ScrollWheelValue > MouseResponder.stateLast.ScrollWheelValue )
                    return -1;
                else
                {
                    return ( ControllerResponder.state.Triggers.Right - ControllerResponder.state.Triggers.Left ) * 2;
                }
            }
        }

        private static Vector2 _interactionPointLast;
        public static Vector2 InteractionPointLast => _interactionPointLast;

        /// <summary>
        /// 交互点.
        /// </summary>
        private static Vector2 _interactionPoint;
        public static Vector2 InteractionPoint => _interactionPoint;

        public override void Update( GameTime gameTime )
        {
            _interactionPointLast = _interactionPoint;
            if( MouseResponder.State.Position != MouseResponder.stateLast.Position )
            {
                _interactionPoint = MouseResponder.State.Position.ToVector2( );
                ControllerResponder.cursorPosition = MouseResponder.State.Position.ToVector2( );
            }
            else if( ControllerResponder.state.ThumbSticks.Right != Vector2.Zero )
            {
                _interactionPoint = ControllerResponder.cursorPosition;
                Mouse.SetPosition( (int)_interactionPoint.X, (int)_interactionPoint.Y );
            }
            base.Update( gameTime );
        }

        public static void SetInteractionPoint( Point point )
        {
            ControllerResponder.cursorPosition = point.ToVector2( );
            Mouse.SetPosition( point.X , point.Y );
            _interactionPoint = point.ToVector2( );
        }
        public Input( ) : base( EngineInfo.Engine ) { }

    }
}