using Colin.Common;
using Colin.Common.Events;
using Colin.Extensions;
using Colin.Resources;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace EternalResolve.Contents.Scenes.Loaders.GameAsset
{
    /// <summary>
    /// 游戏资产加载器.
    /// </summary>
    public sealed class ResourceLoader : Scene
    {
        public EventHandler<BasicEvent> OnLoadComplete = ( s, e ) => { };

        public override void SceneInit( )
        {
            LoadGameAssets( );
            base.SceneInit( );
        }

        private async void LoadGameAssets( )
        {
            await Task.Run( ( ) =>
            {
                IGameResource asset;
                foreach( Type item in Assembly.GetExecutingAssembly( ).GetTypes( ) )
                {
                    if( item.GetInterfaces( ).Contains( typeof( IGameResource ) ) && !item.IsAbstract )
                    {
                        asset = (IGameResource)Activator.CreateInstance( item );
                        asset.LoadResource( );
                        Console.WriteLine( asset.Name + "is loading." );
                    }
                }
                Console.WriteLine( "Load complete." );
                BasicEvent onResourceLoadComplete = new BasicEvent( );
                onResourceLoadComplete.Name = "Event_GameResources_LoadComplete";
                this.EventRaise( OnLoadComplete , onResourceLoadComplete );
            } );
        }
    }
}