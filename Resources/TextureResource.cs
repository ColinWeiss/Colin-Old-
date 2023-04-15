﻿
namespace Colin.Resources
{
    /// <summary>
    /// 包含游戏已加载的纹理资产类.
    /// <br>加载模组后即可从此类获取模组内纹理资产.</br>
    /// </summary>
    public class TextureResource : IGameResource
    {
        public string Name => "纹理资源";

        public float Progress { get; set; }

        public static Dictionary<string, Texture2D> Textures { get; set; } = new Dictionary<string, Texture2D>( );

        public void LoadResource( )
        {
            if( !Directory.Exists( string.Concat( EngineInfo.Engine.Content.RootDirectory, "/Textures" ) ) )
                return;
            Texture2D _texture;
            string _fileName;
            string[ ] TextureFileNames = Directory.GetFiles( string.Concat( EngineInfo.Engine.Content.RootDirectory, "/Textures" ), "*.*", SearchOption.AllDirectories );
            for( int count = 0; count < TextureFileNames.Length; count++ )
            {
                Progress = count / TextureFileNames.Length + 1 / TextureFileNames.Length;
                _fileName = IGameResource.ArrangementPath( TextureFileNames[count] );
                _texture = EngineInfo.Engine.Content.Load<Texture2D>( _fileName );
                Textures.Add( _fileName, _texture );
            }
        }

        /// <summary>
        /// 根据路径获取纹理贴图.
        /// <br>[!] 原版纹理资产的获取起始目录为 <![CDATA["Content/Textures"]]>.</br>
        /// <br>若进行模组纹理加载, 则需要从模组主目录开始索引.</br>
        /// </summary>
        /// <param name="path">路径.</param>
        /// <returns>纹理贴图.</returns>
        public static Texture2D Get( string path )
        {
            Texture2D _texture;
            if( Textures.TryGetValue( string.Concat( "Textures\\", path ), out _texture ) )
                return _texture;
            else
            {
                _texture = EngineInfo.Engine.Content.Load<Texture2D>( string.Concat( "Textures\\", path ) );
                Textures.Add( string.Concat( "Textures\\", path ), _texture );
                return _texture;
            }
        }

    }
}