namespace Colin.Common.Graphics
{
    /// <summary>
    /// 纹理缓存, 单例.
    /// </summary>
    public class SpritePool : Dictionary<int, Sprite>
    {
        private static SpritePool _instance = new SpritePool( );
        public static SpritePool Instance => _instance;

        public new void Add( int key, Sprite value )
        {
            if( !ContainsKey( key ) )
            {
                value.Depth = Count / 1000000;
                base.Add( key, value );
            }
        }

    }
}