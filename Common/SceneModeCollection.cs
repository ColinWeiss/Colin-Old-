using Colin.Extensions;
using System.Collections.ObjectModel;

namespace Colin.Common
{
    /// <summary>
    /// 场景模块集合.
    /// </summary>
    public class SceneModeCollection : Collection<ISceneMode>
    {
        private Scene Scene { get; }

        /// <summary>
        /// 在向 <see cref="SceneModeCollection"/> 添加组件时引发。
        /// </summary>
        public event EventHandler<SceneModeCollectionEventArgs> ModeAdded;

        /// <summary>
        /// 在从 <see cref="SceneModeCollection"/> 移除组件时引发。
        /// </summary>
        public event EventHandler<SceneModeCollectionEventArgs> ModeRemoved;

        protected override void ClearItems( )
        {
            for( int i = 0; i < Count; i++ )
                OnComponentRemoved( new SceneModeCollectionEventArgs( base[i] ) );
            base.ClearItems( );
        }

        protected override void InsertItem( int index, ISceneMode item )
        {
            if( IndexOf( item ) != -1 )
            {
                throw new ArgumentException( "无法多次添加同一模块." );
            }
            base.InsertItem( index, item );
            if( item != null )
            {
                item.Scene = Scene;
                item.SetDefault( );
                OnComponentAdded( new SceneModeCollectionEventArgs( item ) );
            }
        }

        private void OnComponentAdded( SceneModeCollectionEventArgs eventArgs ) => this.EventRaise( ModeAdded, eventArgs );

        private void OnComponentRemoved( SceneModeCollectionEventArgs eventArgs ) => this.EventRaise( ModeRemoved, eventArgs );

        protected override void RemoveItem( int index )
        {
            ISceneMode sceneMode = base[index];
            base.RemoveItem( index );
            if( sceneMode != null )
                OnComponentRemoved( new SceneModeCollectionEventArgs( sceneMode ) );
        }

        protected override void SetItem( int index, ISceneMode item )
        {
            throw new NotSupportedException( );
        }

        /// <summary>
        /// 初始化场景模块集合的实例.
        /// </summary>
        public SceneModeCollection( Scene scene ) => Scene = scene;

    }
}