namespace Colin.Common
{
    /// <summary>
    /// 标识一个可用于场景的模块.
    /// <para>
    /// 不需要在类内对 <see cref="Scene"/> 赋值,
    /// <br>这一操作在 <see cref="SceneModeCollection"/> 加入该模块时自动实现.</br>
    /// </para>
    /// </summary>
    public interface ISceneMode
    {
        /// <summary>
        /// 指示该模块所属的场景.
        /// </summary>
        public Scene Scene { get; set; }

        /// <summary>
        /// 在加入 <see cref="SceneModeCollection"/> 时执行初始化内容.
        /// </summary>
        public void SetDefault( );

    }
}