namespace Colin.Common
{
    /// <summary>
    /// <see cref="SceneComponentList"/> 中的事件使用的参数.
    /// </summary>
    public sealed class SceneModeCollectionEventArgs : EventArgs
    {
        private ISceneComponent _sceneMode;

        /// <summary>
        /// 受事件影响的场景模块.
        /// </summary>
        public ISceneComponent Mode => _sceneMode;

        /// <summary>
        /// 新建 <see cref="SceneModeCollectionEventArgs"/> 实例.
        /// </summary>
        /// <param name="sceneMode">受事件影响的场景模块.</param>
        public SceneModeCollectionEventArgs( ISceneComponent sceneMode )
        {
            _sceneMode = sceneMode;
        }

    }
}