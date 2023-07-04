using Colin.Common;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colin.Modulars.UserInterfaces
{
    public class UserInterface : ISceneComponent, IRenderableSceneComponent
    {
        public static Division Focus;

        public Container Container { get; private set; } = new Container( "NomalContainer" );

        public Scene Scene { get; set; }

        public bool Enable { get; set; }

        public RenderTarget2D SceneRt { get; set; }

        public bool Visiable { get; set; }

        public void DoInitialize( ) { }

        public void DoUpdate( GameTime time ) => Container?.DoUpdate( time );

        public void DoRender( SpriteBatch spriteBatch ) => Container?.DoRender( spriteBatch );

        public void Register( Container container ) => Container?.Register( container );

        public void Remove( Container container, bool dispose ) => Container?.Remove( container );

    }
}