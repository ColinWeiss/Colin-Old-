using Colin.Extensions;
using System.Collections.ObjectModel;

namespace Colin.Common
{
    /// <summary>
    /// 场景模块集合.
    /// </summary>
    public class SceneComponentList
    {
        private readonly Scene Scene;

        public SceneComponentList( Scene scene ) => Scene = scene;

        /// <summary>
        /// 在向 <see cref="SceneComponentList"/> 添加组件时引发。
        /// </summary>
        public event EventHandler<SceneModeCollectionEventArgs> OnAdd;

        /// <summary>
        /// 在从 <see cref="SceneComponentList"/> 移除组件时引发。
        /// </summary>
        public event EventHandler<SceneModeCollectionEventArgs> OnRemove;

        private LiteList<ISceneComponent> _components = new LiteList<ISceneComponent>( );

        private LiteList<IRenderableSceneComponent> _renderableComponents = new LiteList<IRenderableSceneComponent>( );

        public void DoUpdate( GameTime gameTime )
        {
            ISceneComponent _com;
            for( int count = 0; count < _components.Length; count++ )
            {
                _com = _components.Buffer[count];
                if( _com.Enable )
                    _com.DoUpdate( gameTime );
            }
        }

        public void DoRender( )
        {
            IRenderableSceneComponent _com;
            for( int count = 0; count < _renderableComponents.Length; count++ )
            {
                _com = _renderableComponents.Buffer[count];
                if( _com.Visiable )
                    _com.DoRender( );
            }
        }

        public void Add( ISceneComponent sceneMode )
        {
            sceneMode.Scene = Scene;
            sceneMode.Enable = true;
            sceneMode.DoInitialize( );
            if( sceneMode is IRenderableSceneComponent dwMode )
            {
                dwMode.Visiable = true;
                _renderableComponents.Add( dwMode );
                dwMode.InitRenderTarget( );
                Scene.Game.Window.ClientSizeChanged += dwMode.OnClientSizeChanged;
            }
            OnComponentAdded( new SceneModeCollectionEventArgs( sceneMode ) );
            _components.Add( sceneMode );
        }

        public void Remove( ISceneComponent sceneMode )
        {
            if( sceneMode is IRenderableSceneComponent dwMode )
            {
                _renderableComponents.Remove( dwMode );
                Scene.Game.Window.ClientSizeChanged -= dwMode.OnClientSizeChanged;
            }
            OnComponentRemoved( new SceneModeCollectionEventArgs( sceneMode ) );
            _components.Remove( sceneMode );
        }

        private void OnComponentAdded( SceneModeCollectionEventArgs eventArgs ) => this.EventRaise( OnAdd, eventArgs );

        private void OnComponentRemoved( SceneModeCollectionEventArgs eventArgs ) => this.EventRaise( OnRemove, eventArgs );

    }
}