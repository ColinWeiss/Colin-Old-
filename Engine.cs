using Colin.Common;
using Colin.Inputs;
using Colin.IO;
using Colin.Resources;
using System.Windows.Forms;

namespace Colin
{
    public class Engine : Game
    {
        public EngineInfo Info;

        public bool Enable { get; set; } = true;

        public bool Visiable { get; set; } = true;

        public bool Pause = false;

        /// <summary>
        /// 指示当前活跃场景.
        /// </summary>
        public Scene CurrentScene { get; internal set; }

        public ResourceLoader ResourceLoader { get; private set; }

        private int _targetFrame = 60;
        /// <summary>
        /// 指示程序目标帧率.
        /// </summary>
        public int TargetFrame
        {
            get => _targetFrame;
            set => SetTargetFrame( value );
        }

        public Form Form;

        public Engine( )
        {
            ProgramCheck.DoCheck( );
            EngineInfo.Init( this );
            if( EngineInfo.Graphics == null )
            {
                EngineInfo.Graphics = new GraphicsDeviceManager( this )
                {
                    PreferHalfPixelOffset = false,
                    HardwareModeSwitch = false,
                    SynchronizeWithVerticalRetrace = true,
                    PreferMultiSampling = true,
                };
            }
            Content.RootDirectory = "Assets";
            Window.AllowUserResizing = true;
            IsMouseVisible = true;
            IsFixedTimeStep = true;
            Form = Control.FromHandle( Window.Handle ) as Form;
            Form.MinimizeBox = false;
            Form.MaximizeBox = false;
            Form.MinimumSize = new System.Drawing.Size( 1280 , 720 );
        }

        public void SetTargetFrame( int frame )
        {
            _targetFrame = frame;
            TargetElapsedTime = new TimeSpan( 0, 0, 0, 0, (int)Math.Round( 1000f / frame ) );
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
                CurrentScene.UnLoad( );
                Components.Remove( CurrentScene );
            }
            Components.Clear( );
            Components.Add( Singleton<ControllerResponder>.Instance );
            Components.Add( Singleton<MouseResponder>.Instance );
            Components.Add( Singleton<KeyboardResponder>.Instance );
            Components.Add( Singleton<Input>.Instance );
            Components.Add( scene );
            CurrentScene = scene;
            GC.Collect( );
        }

        protected override sealed void Initialize( )
        {
            EngineInfo.SpriteBatch = new SpriteBatch( EngineInfo.Graphics.GraphicsDevice );
            PreLoader.LoadResources( );
            EngineInfo.Config = new Config( );
            EngineInfo.Config.Load( );
            TargetElapsedTime = new TimeSpan( 0, 0, 0, 0, (int)Math.Round( 1000f / TargetFrame ) );
            Components.Add( FileDropProcessor.Instance );
            DoInitialize( );
            base.Initialize( );
        }

        public virtual void DoInitialize( ) { }

        protected override sealed void LoadContent( )
        {
            base.LoadContent( );
        }

        /// <summary>
        /// 在程序开始运行时执行.
        /// </summary>
        public virtual void Start( ) { }

        private bool Started = false;
        protected override sealed void Update( GameTime gameTime )
        {
            if( !Enable )
                return;
            Time.Update( (float)gameTime.ElapsedGameTime.TotalSeconds );
            if( !Started )
            {
                ResourceLoader = new ResourceLoader( );
                ResourceLoader.OnLoadComplete += ( s, e ) => Start( );
                SetScene( ResourceLoader );
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
            DoRender( );
            base.Draw( gameTime );
            if( ControllerResponder.state.IsConnected )
            {
                EngineInfo.SpriteBatch.Begin( );
                EngineInfo.SpriteBatch.Draw( PreLoadResource.ControllerCursor.Source, Input.InteractionPoint - Vector2.One * 8, Color.White );
                EngineInfo.SpriteBatch.End( );
            }
        }
        public virtual void DoRender( ) { }

        protected override void OnActivated( object sender, EventArgs args )
        {
            Pause = false;
            base.OnActivated( sender, args );
        }

        protected override void OnDeactivated( object sender, EventArgs args )
        {
            Pause = true;
            base.OnDeactivated( sender, args );
        }

        protected override void OnExiting( object sender, EventArgs args )
        {
            CurrentScene?.SaveDatas( );
            EngineInfo.Config.Save( );
            base.OnExiting( sender, args );
        }
    }
}