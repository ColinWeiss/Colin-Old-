using Colin.Common.Graphics;
using Colin.Developments;

namespace Colin.Resources
{
    public class PreloadResource : IPreloadGameResource
    {
        public static Sprite Pixel { get; private set; }
        public void PreLoadResource( )
        {
            Texture2D _pixelSource = new Texture2D( EngineInfo.Graphics.GraphicsDevice, 1, 1 );
            _pixelSource.SetData( new Color[ ] { Color.White } );
            Pixel = new Sprite( _pixelSource );
        }
    }
}