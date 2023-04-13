using Colin.Common.SceneComponents.Physics.Dynamics;

namespace Colin.Common.SceneComponents.Physics.Collision.Handlers
{
    public delegate bool CollisionFilterHandler( Fixture fixtureA, Fixture fixtureB );
}