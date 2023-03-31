namespace Colin.Common
{
    /// <summary>
    /// 为应在 <see cref="Scene.Update"/> 中更新的场景模块定义的接口.
    /// <br>标识一个可随场景更新进行逻辑计算的对象.</br>
    /// </summary>
    public interface IUpdateableSceneMode
    {
        /// <summary>
        /// 指示对象是否启用逻辑计算.
        /// </summary>
        bool Enable { get; set; }
        /// <summary>
        /// 进行逻辑计算.
        /// </summary>
        void DoUpdate( GameTime time );

    }
}