using Colin.Common.Physics.Dynamics;

namespace Colin.Common.Physics.Collision.Handlers
{
    public delegate bool CollisionFilterHandler( Fixture fixtureA, Fixture fixtureB );
}