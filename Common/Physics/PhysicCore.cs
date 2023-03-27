using Colin.Common.Physics.Dynamics;

namespace Colin.Common.Physics
{
    public class PhysicCore : ISceneMode, IUpdateableSceneMode
    {
        public bool Enable { get; set; }
        public Scene Scene { get; set; }

        public World World;

        public void SetDefault( )
        {
            World = new World( Vector2.Zero );
        }

        public void DoUpdate( )
        {

        }
    }
}