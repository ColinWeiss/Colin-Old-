using Colin.Common.SceneComponents.Physics.Collision.Shapes;
using Colin.Common.SceneComponents.Physics.Dynamics;

namespace Colin.Common.SceneComponents.Tiles
{
    public class TileCollider
    {
        public PolygonShape Shape { get; internal set; }

        public Fixture Fixture { get; internal set; }

        public virtual PolygonShape OnCreate( TileInfo tile ) => null;

        public virtual void OnRemove( TileInfo tile ) { }

    }
}