﻿using Microsoft.Xna.Framework.Input;
using MonoGame.IMEHelper;
using System.Reflection;

namespace Colin
{
    /// <summary>
    /// 从设备获取信息.
    /// </summary>
    public class EngineInfo
    {
        public static Random Random => new Random( );

        public static string[ ] StartupParameter;

        /// <summary>
        ///用于初始化和控制图形设备的显示.
        /// </summary>
        public static GraphicsDeviceManager Graphics { get; set; }

        /// <summary>
        /// 纹理批管道.
        /// </summary>
        public static SpriteBatch SpriteBatch { get; set; }

        /// <summary>
        /// 游戏刻缓存.
        /// </summary>
        public static GameTime GameTimeCache { get; set; }

        /// <summary>
        /// 指示当前 <see cref="Colin.Engine"/> 对象.
        /// </summary>
        public static Engine Engine { get; set; }

        /// <summary>
        /// 指示当前程序设置.
        /// </summary>
        public static Config Config { get; set; }

        private static string _engineName = string.Empty;
        /// <summary>
        /// 指示当前 <see cref="Colin.Engine"/> 程序集的名称.
        /// </summary>
        public static string EngineName
        {
            get
            {
                if( _engineName == string.Empty )
                {
                    _engineName = Assembly.GetEntryAssembly( ).FullName.Split( ',' ).First( );
                    return _engineName;
                }
                else
                    return _engineName;
            }
        }

        /// <summary>
        /// 视图分辨率宽度.
        /// </summary>
        public static int ViewWidth
        {
            get
            {
                return Graphics.GraphicsDevice.Viewport.Width;
            }
            set
            {
                Graphics.PreferredBackBufferWidth = value;
                Graphics.ApplyChanges( );
            }
        }

        /// <summary>
        /// 视图分辨率高度.
        /// </summary>
        public static int ViewHeight
        {
            get
            {
                return Graphics.GraphicsDevice.Viewport.Height;
            }
            set
            {
                Graphics.PreferredBackBufferHeight = value;
                Graphics.ApplyChanges( );
            }
        }

        /// <summary>
        /// 视图分辨率.
        /// </summary>
        public static Vector2 ViewSizeF
        {
            get
            {
                return new Vector2( ViewWidth, ViewHeight );
            }
        }

        /// <summary>
        /// 视图分辨率.
        /// </summary>
        public static Point ViewSize
        {
            get
            {
                return new Point( ViewWidth, ViewHeight );
            }
        }

        /// <summary>
        /// 视图分辨率中心坐标.
        /// </summary>
        public static Vector2 ViewCenter
        {
            get
            {
                return new Vector2( ViewWidth / 2, ViewHeight / 2 );
            }
        }

        /// <summary>
        /// 分辨率矩形.
        /// </summary>
        public static Rectangle ViewRectangle
        {
            get
            {
                return new Rectangle( 0, 0, ViewWidth, ViewHeight );
            }
        }

        /// <summary>
        /// 当前鼠标信息.
        /// </summary>
        public static MouseState MouseState { get; private set; } = new MouseState( );

        /// <summary>
        /// 上一帧鼠标信息.
        /// </summary>
        public static MouseState MouseStateLast { get; private set; } = new MouseState( );

        public static Vector2 MousePositionF => MouseState.Position.ToVector2( );

        public static WinFormsIMEHandler IMEHandler;

        internal static void Init( Engine engine )
        {
            Engine = engine;
            IMEHandler = new WinFormsIMEHandler( engine );
        }

        /// <summary>
        /// 从设备获取信息.
        /// </summary>
        /// <param name="gameTime">游戏刻.</param>
        internal static void GetInformationFromDevice( GameTime gameTime )
        {
            GameTimeCache = gameTime;
            MouseStateLast = MouseState;
            MouseState = Mouse.GetState( );
        }
    }
}
