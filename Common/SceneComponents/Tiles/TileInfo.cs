namespace Colin.Common.SceneComponents.Tiles
{
    /// <summary>
    /// 物块基本信息.
    /// </summary>
    [Serializable]
    public struct TileInfo
    {
        /// <summary>
        /// 指示物块在区块内的索引.
        /// </summary>
        public int ID;

        /// <summary>
        /// 指示物块非空状态.
        /// </summary>
        public bool Empty;

        /// <summary>
        /// 指示物块在区块内的横坐标.
        /// </summary>
        public int CoordinateX;

        /// <summary>
        /// 指示物块在区块内的纵坐标.
        /// </summary>
        public int CoordinateY;

        /// <summary>
        /// 默认无用途的 <see cref="byte"/>[], 可用于存储其他数据.
        /// </summary>
        public byte[ ] Datas;

        /// <summary>
        /// 物块存储的纹理帧数据.
        /// </summary>
        public TileFrame TextureFrame;

        /// <summary>
        /// 物块存储的边框帧数据.
        /// </summary>
        public TileFrame BorderFrame;

        public TileInfo( )
        {
            ID = 0;
            Empty = false;
            CoordinateX = 0;
            CoordinateY = 0;
            Datas = null;
            TextureFrame = new TileFrame( );
            BorderFrame = new TileFrame( );
        }

    }
}