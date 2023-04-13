﻿using Colin.Common.SceneComponents.Physics.Dynamics;

namespace Colin.Common.SceneComponents.Physics
{
    public class PhysicCore : ISceneComponent
    {
        public bool Enable { get; set; }
        public Scene Scene { get; set; }

        public World World;

        public void DoInitialize( )
        {
            World = new World( Vector2.Zero );
        }

        public void DoUpdate( GameTime time )
        {

        }
    }
}