using System.Reflection;

namespace Colin.Resources
{
    public sealed class Preloader
    {
        public static void LoadResources( )
        {
            foreach( Type item in Assembly.GetExecutingAssembly( ).GetTypes( ) )
            {
                if( item.GetInterfaces( ).Contains( typeof( IPreloadGameResource ) ) && !item.IsAbstract )
                {
                    IPreloadGameResource asset = (IPreloadGameResource)Activator.CreateInstance( item );
                    asset.PreLoadResource( );
                }
            }
            foreach( Type item in Assembly.GetEntryAssembly( ).GetTypes( ) )
            {
                if( item.GetInterfaces( ).Contains( typeof( IPreloadGameResource ) ) && !item.IsAbstract )
                {
                    IPreloadGameResource asset = (IPreloadGameResource)Activator.CreateInstance( item );
                    asset.PreLoadResource( );
                }
            }
        }
    }
}