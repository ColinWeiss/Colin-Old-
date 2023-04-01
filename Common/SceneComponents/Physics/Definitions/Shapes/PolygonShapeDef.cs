using Colin.Common.Physics.Collision.Shapes;
using Colin.Common.Physics.Shared;

namespace Colin.Common.Physics.Definitions.Shapes
{
    public sealed class PolygonShapeDef : ShapeDef
    {
        public PolygonShapeDef( ) : base( ShapeType.Polygon )
        {
            SetDefaults( );
        }

        public Vertices Vertices { get; set; }

        public override void SetDefaults( )
        {
            Vertices = null;
            base.SetDefaults( );
        }
    }
}