﻿namespace Colin.IO
{
    /// <summary>
    /// 游戏数据; 通常用于标识一个文件.
    /// </summary>
    public class GameData
    {
        /// <summary>
        /// 相对文件路径.
        /// </summary>
        public string Path;

        /// <summary>
        /// 文件大小.
        /// </summary>
        public int Length;

        /// <summary>
        /// 文件数据.
        /// </summary>
        public byte[ ] Bytes;

        /// <summary>
        /// 保存文件至指定路径.
        /// </summary>
        /// <param name="async">指示该操作是否使用异步操作.</param>
        public async void Save( bool async )
        {
            if( async )
                await File.WriteAllBytesAsync( Path, Bytes );
            else
                File.WriteAllBytes( Path, Bytes );
        }

        public GameData( string path, byte[ ] bytes )
        {
            Path = path;
            Bytes = bytes;
        }

        /// <summary>
        /// 实例化一个 <seealso cref="GameData"/> 文件.
        /// </summary>
        /// <param name="name">文件名称.</param>
        /// <param name="length">文件大小.</param>
        /// <param name="bytes">文件字节.</param>
        public GameData( string path, int length )
        {
            Path = path;
            Length = length;
            Bytes = File.ReadAllBytes( path );
        }

        /// <summary>
        /// 实例化一个 <seealso cref="GameData"/> 文件.
        /// </summary>
        /// <param name="name">文件名称.</param>
        /// <param name="length">文件大小.</param>
        /// <param name="bytes">文件字节.</param>
        public GameData( string path, int length, byte[ ] bytes )
        {
            Path = path;
            Length = length;
            Bytes = bytes;
        }

    }
}