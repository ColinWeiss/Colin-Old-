using Colin.Common.Graphics;
using Colin.Developments;
using Colin.Resources;
using System.Text;

namespace Colin.Common.Tiles.Prefabs
{
    /// <summary>
    /// 适用单个方块的通用 <see cref="Tile"/>.
    /// </summary>
    public class MonoBlock : Tile
    {
        /// <summary>
        /// 该块使用的包含纹理与边框的 Sprite 路径.
        /// </summary>
        public virtual string TexturePath => "Tiles/Default";

        /// <summary>
        /// 该块适用的包含纹理与边框的 Sprite.
        /// </summary>
        public Sprite Texture { get; protected set; }

        public override bool Renderable => true;

        protected override void OnPlace( ref TileInfo tile )
        {
            TileCollider = new MonoBlockCollider( );
            Texture = new Sprite( TextureResource.Get( TexturePath ) );
            tile.TextureFrame.X = tile.CoordinateX % 9;
            tile.TextureFrame.Y = tile.CoordinateY % 9;
            tile.TextureFrame.Width = 1;
            tile.TextureFrame.Height = 1;
            byte[ ] name = Encoding.UTF8.GetBytes( "MonoBlock" );
            tile.Datas = name;
            base.OnPlace( ref tile );
        }

        public override void Render( ref TileInfo tile )
        {
            if( Texture != null )
            {
                EngineInfo.SpriteBatch.Draw( Texture.Source, new Vector2( tile.CoordinateX * TileSetting.TileWidth, tile.CoordinateY * TileSetting.TileHeight ),
                tile.TextureFrame.Frame, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, Texture.Depth );
            }
        }

    }
}