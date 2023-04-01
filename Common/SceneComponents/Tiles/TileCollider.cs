using Colin.Common.Physics.Collision.Shapes;
using Colin.Common.Physics.Dynamics;

namespace Colin.Common.Tiles
{
    public class TileCollider
    {
        public PolygonShape Shape { get; internal set; }

        public Fixture Fixture { get; internal set; }

        public virtual PolygonShape OnCreate( TileInfo tile ) => null;

        public virtual void OnRemove( TileInfo tile ) { }

    }
}