namespace Colin.Common.Mods
{
    /// <summary>
    /// 模组主类.
    /// </summary>
    public class Mod
    {
        /// <summary>
        /// 模组内部名称.
        /// </summary>
        public string Name => GetType( ).Name;

        /// <summary>
        /// 模组显示名称.
        /// </summary>
        public virtual string DisplayName => " ";

        /// <summary>
        /// 模组核心.
        /// </summary>
        public ModCore Core { get; internal set; }

    }
}