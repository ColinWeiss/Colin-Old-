using Colin.Common.SceneComponents.Physics.Collision.Handlers;
using Colin.Common.SceneComponents.Physics.Collision.RayCast;
using Colin.Common.SceneComponents.Physics.Dynamics;
using Colin.Common.SceneComponents.Physics.Shared;

namespace Colin.Common.SceneComponents.Physics.Collision.Broadphase
{
    public interface IBroadPhase
    {
        int ProxyCount { get; }

        void UpdatePairs( BroadphaseHandler callback );

        bool TestOverlap( int proxyIdA, int proxyIdB );

        int AddProxy( ref FixtureProxy proxy );

        void RemoveProxy( int proxyId );

        void MoveProxy( int proxyId, ref AABB aabb, Vector2 displacement );

        FixtureProxy GetProxy( int proxyId );

        void TouchProxy( int proxyId );

        void GetFatAABB( int proxyId, out AABB aabb );

        void Query( Func<int, bool> callback, ref AABB aabb );

        void RayCast( Func<RayCastInput, int, float> callback, ref RayCastInput input );

        void ShiftOrigin( ref Vector2 newOrigin );
    }
}