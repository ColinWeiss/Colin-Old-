namespace Colin.Common.SceneComponents.Tiles
{
    /// <summary>
    /// 以自定义操作进行物块渲染的渲染对象.
    /// </summary>
    public interface ITileRenderer
    {
        /// <summary>
        /// 瓦片处理核心引用.
        /// </summary>
        Tiled Tiled { get; }

        /// <summary>
        /// 进行物块渲染.
        /// </summary>
        void RenderTiles( );

    }
}