using Colin.Common.SceneComponents.Physics.Collision.Shapes;

namespace Colin.Common.SceneComponents.Physics.Definitions.Shapes
{
    public sealed class CircleShapeDef : ShapeDef
    {
        public CircleShapeDef( ) : base( ShapeType.Circle )
        {
            SetDefaults( );
        }

        /// <summary>Get or set the position of the circle</summary>
        public Vector2 Position { get; set; }

        public override void SetDefaults( )
        {
            Position = Vector2.Zero;
            base.SetDefaults( );
        }
    }
}