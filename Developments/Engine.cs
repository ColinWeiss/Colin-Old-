using Colin.Common;
using Colin.Common.Inputs;
using Colin.Common.IO;
using Colin.Resources;
using System.Text.Json;

namespace Colin.Developments
{
    public class Engine : Game
    {
        public EngineInfo Info;

        public bool Enable { get; set; } = true;

        public bool Visiable { get; set; } = true;

        /// <summary>
        /// 指示当前活跃场景.
        /// </summary>
        public Scene CurrentScene { get; internal set; }

        public virtual int TargetFrame => 120;

        public Engine( )
        {
            if( EngineInfo.Engine == null )
                EngineInfo.Engine = this;
            if( EngineInfo.Graphics == null )
            {
                EngineInfo.Graphics = new GraphicsDeviceManager( this )
                {
                    PreferHalfPixelOffset = true,
                    HardwareModeSwitch = false,
                    SynchronizeWithVerticalRetrace = true,
                    PreferMultiSampling = true,
                };
            }
            Content.RootDirectory = "Assets";
            Window.AllowUserResizing = true;
            IsMouseVisible = true;
            IsFixedTimeStep = true;
            TargetElapsedTime = new TimeSpan( 0, 0, 0, 0, (int)Math.Round( 1000f / TargetFrame ) );
        }

        /// <summary>
        /// 切换场景.
        /// </summary>
        /// <param name="scene">要切换到的场景对象.</param>
        public void SetScene( Scene scene )
        {
            if( CurrentScene != null )
            {
                if( CurrentScene.InitializeOnSwitch )
                    Window.ClientSizeChanged -= CurrentScene.InitRenderTarget;
                Components.Remove( CurrentScene );
            }
            Components.Add( scene );
            CurrentScene = scene;
        }

        protected override sealed void Initialize( )
        {
            EngineInfo.SpriteBatch = new SpriteBatch( EngineInfo.Graphics.GraphicsDevice );
            Preloader.LoadResources( );
            EngineInfo.Config = new Config( );
            EngineInfo.Config.Load( );
            Components.Add( FileDropProcessor.Instance );
            Components.Add( KeyboardResponder.Instance );
            Components.Add( MouseResponder.Instance );
            DoInitialize( );
            base.Initialize( );
        }

        public virtual void DoInitialize( ) { }

        protected override sealed void LoadContent( )
        {
            base.LoadContent( );
        }

        public virtual void Start( ) { }

        private bool Started = false;
        protected override sealed void Update( GameTime gameTime )
        {
            if( !Enable )
                return;
            if( !Started )
            {
                Start( );
                Started = true;
            }
            EngineInfo.GetInformationFromDevice( gameTime );
            DoUpdate( );
            base.Update( gameTime );
        }
        public virtual void DoUpdate( ) { }

        protected override sealed void Draw( GameTime gameTime )
        {
            if( !Visiable )
                return;
            GraphicsDevice.Clear( Color.Black );
            FrameCounter frameCounter = new FrameCounter( );
            frameCounter.Update( (float)gameTime.ElapsedGameTime.TotalSeconds );
            Window.Title = frameCounter.AverageFramesPerSecond.ToString( "F0" );
            DoRender( );
            base.Draw( gameTime );
        }
        public virtual void DoRender( ) { }

        protected override void OnExiting( object sender, EventArgs args )
        {
            EngineInfo.Config.Save( );
            base.OnExiting( sender, args );
        }
    }
}