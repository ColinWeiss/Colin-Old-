using Colin.Graphics;
using Colin.Resources.PreLoadResources;

namespace Colin.Resources
{
    public class PreLoadResource : IPreloadGameResource
    {
        public static Sprite Pixel { get; private set; }

        public static Sprite ControllerCursor { get; private set; }

        public void PreLoad( )
        {
            Texture2D _pixelSource = new Texture2D( EngineInfo.Graphics.GraphicsDevice, 1, 1 );
            _pixelSource.Name = "PreloadResource_Pixel";
            _pixelSource.SetData( new Color[ ] { Color.White } );
            Pixel = new Sprite( _pixelSource );
            using( MemoryStream ms = new MemoryStream( ProgramResources.ControllerCursor , true ) )
            {
                using( StreamReader sr = new StreamReader( ms ) )
                {
                    Texture2D _controllerCursor = Texture2D.FromStream( EngineInfo.Engine.GraphicsDevice, sr.BaseStream );
                    _controllerCursor.Name = "PreloadResource_ControllerCursor";
                    ControllerCursor = new Sprite( _controllerCursor );
                }
            }
        }
    }
}