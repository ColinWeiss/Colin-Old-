using Colin.Developments;

namespace Colin.Resources
{
    public class EffectResource : IGameResource
    {
        public string Name => "着色器";

        public float Progress { get; set; }

        public static Dictionary<string, Effect> Effects { get; set; } = new Dictionary<string, Effect>( );

        public void LoadResource( )
        {
            if( !Directory.Exists( string.Concat( EngineInfo.Engine.Content.RootDirectory, "/Effects" ) ) )
                return;
            Effect _effect;
            string _fileName;
            string[ ] TextureFileNames = Directory.GetFiles( string.Concat( EngineInfo.Engine.Content.RootDirectory, "/Effects" ), "*.*", SearchOption.AllDirectories );
            for( int count = 0; count < TextureFileNames.Length; count++ )
            {
                Progress = count / TextureFileNames.Length + 1 / TextureFileNames.Length;
                _fileName = IGameResource.ArrangementPath( TextureFileNames[count] );
                _effect = EngineInfo.Engine.Content.Load<Effect>( _fileName );
                Effects.Add( _fileName, _effect );
            }
        }

        /// <summary>
        /// 根据路径获取着色器.
        /// <br>[!] 起始目录为 <![CDATA["Content/Effects"]]></br>
        /// </summary>
        /// <param name="path">路径.</param>
        /// <returns>着色器.</returns>
        public static Effect GetAsset( string path )
        {
            Effect _texture;
            if( Effects.TryGetValue( string.Concat( "Effects/", path ), out _texture ) )
                return _texture;
            else
            {
                Effects.Add( string.Concat( "Effects/", path ), _texture );
                _texture = EngineInfo.Engine.Content.Load<Effect>( string.Concat( "Effects/", path ) );
                return _texture;
            }
        }

    }
}