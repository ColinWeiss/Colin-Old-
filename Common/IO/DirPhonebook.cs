using Colin.Developments;

namespace Colin.Common.IO
{
    public class GameDirPhonebook
    {
        /// <summary>
        /// 使用 <see cref="Colin"/> 进行开发的程序的目录.
        /// </summary>
        public static string ProgramDir => string.Concat( Environment.GetFolderPath( Environment.SpecialFolder.MyDocuments ), "\\My Games\\", EngineInfo.EngineName );

        /// <summary>
        /// 指示游戏存档文件夹路径.
        /// </summary>
        public static string ArchiveDir => string.Concat( ProgramDir, "\\Archive" );

        /// <summary>
        /// 指示游戏存档区块文件夹路径.
        /// </summary>
        public static string ArchiveChunkDir => string.Concat( ProgramDir, "\\Archive\\Chunks" );

        /// <summary>
        /// 指示模组文件夹路径.
        /// </summary>
        public static string ModDir => string.Concat( string.Concat( ProgramDir, "\\Mod" ) );

    }
}