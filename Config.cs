using Colin.Common.IO;
using Colin.Resources;
using System.Text.Json;

namespace Colin
{
    /// <summary>
    /// 程序设置.
    /// </summary>
    public sealed class Config
    {
        /// <summary>
        /// 指示游戏配置文件的位置及其本身.
        /// <br>包含文件扩展名.</br>
        /// </summary>
        public static string ConfigPath => string.Concat(DirPhonebook.ProgramDir, "\\Configs.json");

        /// <summary>
        /// 指示是否全屏.
        /// </summary>
        public bool IsFullScreen
        {
            get
            {
                return EngineInfo.Graphics.IsFullScreen;
            }
            set
            {
                EngineInfo.Graphics.IsFullScreen = value;
                EngineInfo.Graphics.ApplyChanges();
            }
        }

        /// <summary>
        /// 指示是否启用音效.
        /// </summary>
        public bool SoundEffect { get; set; } = true;

        /// <summary>
        /// 指示音效音量百分比.
        /// </summary>
        public float SoundEffectVolume { get; set; } = 1f;

        /// <summary>
        /// 指示图形质量.
        /// </summary>
        public PictureQuality PictureQuality { get; set; }

        /// <summary>
        /// 指示鼠标是否可见.
        /// </summary>
        public bool IsMouseVisiable
        {
            get
            {
                return EngineInfo.Engine.IsMouseVisible;
            }
            set
            {
                EngineInfo.Engine.IsMouseVisible = value;
            }
        }

        public void Load()
        {
            if (!File.Exists(ConfigPath))
                Save();
            Config result = JsonSerializer.Deserialize<Config>(File.ReadAllText(ConfigPath));
            IsFullScreen = result.IsFullScreen;
            SoundEffect = result.SoundEffect;
            SoundEffectVolume = result.SoundEffectVolume;
            PictureQuality = result.PictureQuality;
            IsMouseVisiable = result.IsMouseVisiable;
        }
        public void Save()
        {
            string config = JsonSerializer.Serialize(this);
            File.WriteAllText(ConfigPath, config);
        }
    }
}