using Colin.Collections;
using System.Runtime.CompilerServices;

namespace Colin.Common
{
    public class ComponentList
    {
        public readonly Entity Entity;

        public ComponentList( Entity entity )
        {
            Entity = entity;
        }

        internal LiteList<Component> _components = new LiteList<Component>( );

        private LiteList<IUpdateableComponent> _updatableComponents = new LiteList<IUpdateableComponent>( );

        private LiteList<RenderableComponent> _renderableComponents = new LiteList<RenderableComponent>( );

        internal List<Component> _componentsToAdd = new List<Component>( );

        List<Component> _tempBufferList = new List<Component>( );

        private List<Component> _componentsToRemove = new List<Component>( );

        public void UpdateLists( )
        {
            if( _componentsToRemove.Count > 0 )
            {
                for( int i = 0; i < _componentsToRemove.Count; i++ )
                    _components.Remove( _componentsToRemove[i] );
                _componentsToRemove.Clear( );
            }
            if( _componentsToAdd.Count > 0 )
            {
                for( int i = 0, count = _componentsToAdd.Count; i < count; i++ )
                {
                    var component = _componentsToAdd[i];
                    component.Entity = Entity;
                    if( component is RenderableComponent renderableComponent )
                        _renderableComponents.Add( renderableComponent );
                    if( component is IUpdateableComponent updateableComponent )
                        _updatableComponents.Add( updateableComponent );
                    _components.Add( component );
                    _tempBufferList.Add( component );
                }
                _componentsToAdd.Clear( );
                for( var i = 0; i < _tempBufferList.Count; i++ )
                {
                    var component = _tempBufferList[i];
                    if( component.Enable )
                        component.OnEnabled( );
                }
                _tempBufferList.Clear( );
            }
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]

        internal void AddComponent( Component component ) => _componentsToAdd.Add( component );

        public void DoUpdate( GameTime gameTime )
        {
            UpdateLists( );
            for( var i = 0; i < _updatableComponents.Length; i++ )
            {
                if( _updatableComponents.Buffer[i].Enable && (_updatableComponents.Buffer[i] as Component).Enable )
                    _updatableComponents.Buffer[i].DoUpdate( gameTime );
            }
        }

        public void DoRender( )
        {
            for( var i = 0; i < _renderableComponents.Length; i++ )
            {
                if( _renderableComponents.Buffer[i].Visiable )
                    _renderableComponents.Buffer[i].DoRender( );
            }
        }

    }
}