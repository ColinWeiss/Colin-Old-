namespace Colin.Common.Tiles
{
    /// <summary>
    /// 物块.
    /// </summary>
    [Serializable]
    public class Tile
    {
        /// <summary>
        /// 指向的物块的索引.
        /// </summary>
        public int TileIndex { get; set; }

        /// <summary>
        /// 指示物块在脏绘制计算中是否需要清除.
        /// </summary>
        public bool NeedClear { get; set; } = false;

        /// <summary>
        /// 指示物块的区别名称.
        /// <br>默认为类型名.</br>
        /// </summary>
        public virtual string Name => GetType( ).Name;

        /// <summary>
        /// 指示物块是否启用 <see cref="Update"/>.
        /// <br>默认为 <see langword="false"/>.</br>
        /// </summary>
        public virtual bool Updateable => false;

        /// <summary>
        /// 指示物块是否启用 <see cref="Render"/>.
        /// <br>默认为 <see langword="false"/>.</br>
        /// </summary>
        public virtual bool Renderable => false;

        /// <summary>
        /// 指示物块搭载的碰撞器.
        /// <br>若欲为物块指定碰撞器, 请重写 <see cref="SetDefaultTileCollider"/>.</br>
        /// </summary>
        public TileCollider TileCollider { get; internal set; }

        /// <summary>
        /// 操作该方法以设置物块碰撞器.
        /// </summary>
        /// <returns>要使用的碰撞器.</returns>
        public TileCollider SetDefaultTileCollider( ) => null;

        /// <summary>
        /// 定制物块逻辑计算.
        /// </summary>
        public virtual void Update( ref TileInfo tile ) { }

        /// <summary>
        /// 定制物块渲染方式.
        /// </summary>
        /// <param name="tile">物块.</param>
        public virtual void Render( ref TileInfo tile ) { }

        /// <summary>
        /// 放置物块.
        /// </summary>
        /// <param name="tileInfo">物块信息.</param>
        public void Place( ref TileInfo tileInfo )
        {
            NeedClear = false;
            OnPlace( ref tileInfo );
        }

        /// <summary>
        /// 自定义放置物块时的操作.
        /// </summary>
        protected virtual void OnPlace( ref TileInfo tileInfo ) { }

        /// <summary>
        /// 破坏物块.
        /// </summary>
        /// <param name="tileInfo">物块信息.</param>
        public void Destruction( ref TileInfo tileInfo )
        {
            NeedClear = true;
            OnDestruction( ref tileInfo );
        }

        /// <summary>
        /// 发生于物块被破坏时.
        /// </summary>
        protected virtual void OnDestruction( ref TileInfo tileInfo ) { }

    }
}