using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Colin.Developments.Ecs
{
    [Serializable]
    [DataContract]
    public class Entity : ITraceable, IName , IBehavior
    {
        public bool Enable;

        public bool Visiable;

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public readonly IName Source;

        public Entity( IName source )
        {
            Source = source;
        }

        public IName GetSource( ) => Source;

        public Transform Transform = new Transform( );

        private Entity parent;
        public Entity Parent
        {
            get => parent;
            set
            {
                if( parent != value )
                {
                    if( parent != null )
                    {
                        parent.children.Remove( this );
                    }
                    parent = value;
                    if( parent != null )
                    {
                        parent.children.Add( this );
                        Transform.Parent = parent.Transform;
                    }
                }
            }
        }

        public IEnumerable<Entity> Children => children;

        private List<Entity> children = new List<Entity>( );

        public IBehavior Behavior;

        public void DoInitialize( )
        {
            Behavior?.DoInitialize( );
        }

        public void DoUpdate( GameTime time )
        {
            Behavior?.DoUpdate( time );
            Entity _child;
            for( int count = 0; count < children.Count; count++)
            {
                _child = children[count];
                if( _child.Enable )
                    _child.DoUpdate( time );
            }
        }

        public void DoRender( )
        {
            Behavior?.DoRender( );
            Entity _child;
            for( int count = 0; count < children.Count; count++ )
            {
                _child = children[count];
                if( _child.Enable )
                    _child.DoRender( );
            }
        }

        public void Register( Entity entity )
        {
            entity.Parent = this;
            entity.DoInitialize( );
        }

    }
}