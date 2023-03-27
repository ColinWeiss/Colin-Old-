using Colin.Developments;
using Colin.Extensions;

namespace Colin.Common
{
    /// <summary>
    /// 场景.
    /// </summary>
    public class Scene : DrawableGameComponent
    {
        private SceneModeCollection _modes;
        public SceneModeCollection Modes => _modes;

        private List<IUpdateableSceneMode> _updates = new List<IUpdateableSceneMode>( );

        private List<IRenderableSceneMode> _draws = new List<IRenderableSceneMode>( );

        /// <summary>
        /// 指示场景在切换时是否执行初始化的值.
        /// </summary>
        public bool InitializeOnSwitch = true;

        /// <summary>
        /// 场景本身的 RenderTarget.
        /// </summary>
        public RenderTarget2D SceneRenderTarget;

        public override sealed void Initialize( )
        {
            if( InitializeOnSwitch )
            {
                InitRenderTarget( this, new EventArgs( ) );
                Game.Window.ClientSizeChanged += InitRenderTarget;
                _modes = new SceneModeCollection( this );
                _modes.ModeAdded += Modes_ComponentAdded;
                _modes.ModeRemoved += Modes_ComponentRemoved;
                SceneInit( );
            }
            else
                Game.Window.ClientSizeChanged += InitRenderTarget;
            base.Initialize( );
        }

        internal void InitRenderTarget( object s, EventArgs e )
        {
            SceneRenderTarget = RenderTargetExtension.CreateDefault( );
        }

        private void Modes_ComponentAdded( object sender, SceneModeCollectionEventArgs e ) => CategorizeMode( e.Mode );

        private void Modes_ComponentRemoved( object sender, SceneModeCollectionEventArgs e ) => DecategorizeMode( e.Mode );

        /// <summary>
        /// 执行场景初始化.
        /// </summary>
        public virtual void SceneInit( ) { }

        private bool Started = false;
        public override sealed void Update( GameTime gameTime )
        {
            if( !Started )
            {
                Start( );
                Started = true;
            }
            for( int count = 0; count < _updates.Count; count++ )
            {
                if( _updates[count].Enable )
                    _updates[count].DoUpdate( );
            }
            SceneUpdate( );
            base.Update( gameTime );
        }
        public virtual void Start( ) { }

        public virtual void SceneUpdate( ) { }

        public override sealed void Draw( GameTime gameTime )
        {
            IRenderableSceneMode renderMode;
            RenderTarget2D frameRenderLayer;
            for( int count = 0; count < _draws.Count; count++ )
            {
                renderMode = _draws[count];
                if( renderMode.Visiable )
                {
                    frameRenderLayer = renderMode.RenderTarget;
                    EngineInfo.Graphics.GraphicsDevice.SetRenderTarget( frameRenderLayer );
                    if( count == 0 )
                        EngineInfo.Graphics.GraphicsDevice.Clear( Color.Black );
                    else
                        EngineInfo.Graphics.GraphicsDevice.Clear( Color.Transparent );

                    renderMode.DoRender_SceneModeCollectionUseIt( );
                    EngineInfo.Graphics.GraphicsDevice.SetRenderTarget( SceneRenderTarget );
                    EngineInfo.SpriteBatch.Begin( );
                    EngineInfo.SpriteBatch.Draw( frameRenderLayer, new Rectangle( 0, 0, EngineInfo.ViewWidth, EngineInfo.ViewHeight ), Color.White );
                    EngineInfo.SpriteBatch.End( );
                }
            }
            SceneRender( );
            EngineInfo.Graphics.GraphicsDevice.SetRenderTarget( null );
            EngineInfo.SpriteBatch.Begin( );
            EngineInfo.SpriteBatch.Draw( SceneRenderTarget, new Rectangle( 0, 0, EngineInfo.ViewWidth, EngineInfo.ViewHeight ), Color.White );
            EngineInfo.SpriteBatch.End( );
            base.Draw( gameTime );
        }

        public virtual void SceneRender( ) { }

        /// <summary>
        /// 我不在乎你加不加载, 但我希望玩家的电脑犯病的时候我们能把重要数据保存下来.
        /// <br>这个方法将伴随 <see cref="Game.Exit"/> 一起执行.</br>
        /// </summary>
        public virtual void SaveDatas( ) { }

        private void CategorizeMode( ISceneMode sceneMode )
        {
            sceneMode.Scene = this;
            if( sceneMode is IUpdateableSceneMode upMode )
            {
                upMode.Enable = true;
                _updates.Add( upMode );
            }
            if( sceneMode is IRenderableSceneMode dwMode )
            {
                dwMode.Visiable = true;
                _draws.Add( dwMode );
                dwMode.InitRenderTarget( );
                Game.Window.ClientSizeChanged += dwMode.OnClientSizeChanged; ;
            }
        }

        private void DecategorizeMode( ISceneMode sceneMode )
        {
            if( sceneMode is IUpdateableSceneMode upMode )
                _updates.Remove( upMode );
            if( sceneMode is IRenderableSceneMode dwMode )
            {
                _draws.Remove( dwMode );
                Game.Window.ClientSizeChanged -= dwMode.OnClientSizeChanged; ;
            }
        }

        public Scene( ) : base( EngineInfo.Engine ) { }

    }
}
