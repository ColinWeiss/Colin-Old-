﻿using Colin.Developments;
using Microsoft.Xna.Framework.Audio;

namespace Colin.Resources
{
    public class SoundResource : IGameResource
    {
        public string Name => "声音资源";

        public float Progress { get; set; }

        public static Dictionary<string, SoundEffect> Sounds { get; set; } = new Dictionary<string, SoundEffect>( );

        public void LoadResource( )
        {
            SoundEffect _sound;
            string _fileName;
            string[ ] TextureFileNames = Directory.GetFiles( string.Concat( EngineInfo.Engine.Content.RootDirectory, "/Sounds" ), "*.*", SearchOption.AllDirectories );
            for( int count = 0; count < TextureFileNames.Length; count++ )
            {
                Progress = count / TextureFileNames.Length + 1 / TextureFileNames.Length;
                _fileName = IGameResource.ArrangementPath( TextureFileNames[count] );
                _sound = EngineInfo.Engine.Content.Load<SoundEffect>( _fileName );
                Sounds.Add( _fileName, _sound );
            }
        }

        /// <summary>
        /// 根据路径获取声音贴图.
        /// <br>[!] 起始目录为 <![CDATA["Sounds"]]></br>
        /// </summary>
        /// <param name="path">路径.</param>
        /// <returns>声音贴图.</returns>
        public static SoundEffect GetAsset( string path )
        {
            SoundEffect _sound;
            if( Sounds.TryGetValue( string.Concat( "Sounds/", path ), out _sound ) )
                return _sound;
            else
            {
                Sounds.Add( string.Concat( "Sounds/", path ), _sound );
                _sound = EngineInfo.Engine.Content.Load<SoundEffect>( string.Concat( "Sounds/", path ) );
                return _sound;
            }
        }

    }
}
