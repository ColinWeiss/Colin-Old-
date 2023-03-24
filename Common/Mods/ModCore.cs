using System.Reflection;

namespace Colin.Common.Mods
{
    /// <summary>
    /// 表示一个模组的核心.
    /// </summary>
    public class ModCore
    {
        /// <summary>
        /// 模组的程序集.
        /// </summary>
        public Assembly Assembly { get; internal set; }

        /// <summary>
        /// 模组的图像, 已自动转为 <see cref="Texture2D"/>.
        /// <br>键为其对应的文件路径.</br>
        /// </summary>
        public Dictionary<string, Texture2D> Textures { get; internal set; } = new Dictionary<string, Texture2D>( );

        /// <summary>
        /// 模组包含的其他格式的文件.
        /// </summary>
        public Dictionary<string, byte[ ]> Files { get; internal set; } = new Dictionary<string, byte[ ]>( );

        /// <summary>
        /// 加载核心所绑定的模组.
        /// </summary>
        internal void Load( )
        {

        }
    }
}