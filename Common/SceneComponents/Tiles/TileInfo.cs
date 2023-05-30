using Colin.Common.SceneComponents.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colin.Common.SceneComponents.Tiled
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
        /// 指示物块帧格.
        /// </summary>
        public TileFrame TileFrame;

        public TileInfo( )
        {
            ID = 0;
            Empty = true;
            CoordinateX = 0;
            CoordinateY = 0;
            TileFrame = new TileFrame( -1 , -1 );
        }
    }
}