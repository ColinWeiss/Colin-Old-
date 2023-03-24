namespace Colin.Common.Tiles
{
    /// <summary>
    /// 物块帧格
    /// </summary>
    [Serializable]
    public struct TileFrame
    {
        public int X;

        public int Y;

        public int Width;

        public int Height;

        public Rectangle Frame => new Rectangle( X * TileSetting.TileWidth, Y * TileSetting.TileHeight, Width * TileSetting.TileWidth, Height * TileSetting.TileHeight );

    }
}