using Colin.Developments;
using Microsoft.Xna.Framework.Input;

namespace Colin.Resources
{
    public class CursorResource : IGameResource
    {
        public static MouseCursor Arrow { get; private set; }

        public static MouseCursor No { get; private set; }

        public string Name { get; }

        public float Progress { get; set; }

        private static Texture2D GetCursorTexture( string path )
        {
            return EngineInfo.Engine.Content.Load<Texture2D>( string.Concat( "Textures/Systems/Cursors/", path ) );
        }

        public void LoadResource( )
        {
            //  Arrow = MouseCursor.FromTexture2D( GetCursorTexture( "Arrow" ), 0, 0 );
            // No = MouseCursor.FromTexture2D( GetCursorTexture( "No" ), 0, 0 );
        }
    }
}