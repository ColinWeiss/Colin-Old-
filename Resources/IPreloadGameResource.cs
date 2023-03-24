namespace Colin.Resources
{
    /// <summary>
    /// 标识一个在程序开始循环之前就需要被加载的对象.
    /// </summary>
    public interface IPreloadGameResource
    {
        void PreLoadResource( );
    }
}