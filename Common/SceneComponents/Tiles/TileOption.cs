﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colin.Common.SceneComponents.Tiled
{
    /// <summary>
    /// 物块配置.
    /// </summary>
    public class TileOption
    {
        public static Point TileSize => new Point( 16, 16 );

        public static Point ChunkSize => new Point( 16 , 16 );

    }
}