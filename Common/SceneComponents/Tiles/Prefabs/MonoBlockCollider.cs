using Colin.Common.SceneComponents.Physics.Collision.Shapes;

namespace Colin.Common.SceneComponents.Tiles.Prefabs
{
    public class MonoBlockCollider : TileCollider
    {
        public override PolygonShape OnCreate( TileInfo tile )
        {
            Vector2 pos = new Vector2( tile.CoordinateX * TileSetting.TileWidth, tile.CoordinateY * TileSetting.TileHeight );
            PolygonShape shape = new PolygonShape( 0.0f );
            //      shape.( 8, 8, pos, 0.0f );
            return shape;
        }
    }
}
