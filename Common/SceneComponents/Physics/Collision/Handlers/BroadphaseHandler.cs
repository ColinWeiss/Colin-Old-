using Colin.Common.SceneComponents.Physics.Dynamics;

namespace Colin.Common.SceneComponents.Physics.Collision.Handlers
{
    public delegate void BroadphaseHandler( ref FixtureProxy proxyA, ref FixtureProxy proxyB );
}