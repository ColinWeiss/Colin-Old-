using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace Colin.Modulars.UserInterfaces
{
    /// <summary>
    /// 为划分元素指定剪裁样式.
    /// </summary>
    public struct ScissorStyle
    {
        /// <summary>
        /// 启用剪裁.
        /// </summary>
        public bool Enable;

       //private Rectangle _scissor;
        /// <summary>
        /// 指示要进行剪裁的范围.
        /// <br>若不指定其值, 默认使用 <see cref="LayoutStyle.DefaultTotalRect"/>.</br>
        /// </summary>
        public Rectangle Scissor;
    }
}