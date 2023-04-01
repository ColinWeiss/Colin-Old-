using Colin.Resources.PreLoadResources;
using FontStashSharp;

namespace Colin.Resources
{
    public class FontResource : IPreloadGameResource
    {
        public static FontSystem Unifont { get; private set; }

        public static FontSystem GlowSans { get; private set; }

        public void PreLoadResource( )
        {
            Unifont = new FontSystem( );
            Unifont.AddFont( ProgramResources.Unifont );

            GlowSans = new FontSystem( );
            GlowSans.AddFont( ProgramResources.GlowSansSC_Normal_Medium );

        }
    }
}