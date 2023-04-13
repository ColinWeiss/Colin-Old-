using Colin.Common.SceneComponents.Physics.Collision.Shapes;
using Colin.Common.SceneComponents.Physics.Shared;

namespace Colin.Common.SceneComponents.Physics.Definitions.Shapes
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