﻿using Microsoft.Xna.Framework.Audio;

namespace Colin.Common.Audios
{
    /// <summary>
    /// 声音模块.
    /// </summary>
    public class Sound : ISceneComponent
    {
        public bool Enable { get; set; } = true;
        public Scene Scene { get; set; }

        public void DoInitialize( )
        {

        }

        public void DoUpdate( GameTime time )
        {

        }

        /// <summary>
        /// 播放指定音效.
        /// </summary>
        /// <param name="soundEffect">音效.</param>
        public static void Play( SoundEffect soundEffect )
        {
            if( EngineInfo.Config.SoundEffect )
            {
                SoundEffectInstance _instance = soundEffect?.CreateInstance( );
                if( _instance != null )
                {
                    _instance.Volume = EngineInfo.Config.SoundEffectVolume;
                    _instance.Play( );
                }
            }
        }
    }
}