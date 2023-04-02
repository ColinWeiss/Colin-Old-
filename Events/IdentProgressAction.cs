namespace Colin.Events
{
    /// <summary>
    /// 标识一个带有进度标识的执行任务.
    /// </summary>
    /// <param name="progress">当前进度.</param>
    public delegate void IdentProgressAction(ref float progress);
}
