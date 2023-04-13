using Colin.Common.SceneComponents.Physics.Dynamics;

namespace Colin.Common.SceneComponents.Physics.Collision.Handlers
{
    public delegate bool BeforeCollisionHandler( Fixture fixtureA, Fixture fixtureB );
}