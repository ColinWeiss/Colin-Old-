using Colin.Common.Physics.Dynamics;

namespace Colin.Common.Physics.Collision.Handlers
{
    public delegate void BroadphaseHandler( ref FixtureProxy proxyA, ref FixtureProxy proxyB );
}