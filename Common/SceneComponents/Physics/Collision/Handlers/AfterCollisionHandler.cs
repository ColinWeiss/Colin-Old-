using Colin.Common.SceneComponents.Physics.Collision.ContactSystem;
using Colin.Common.SceneComponents.Physics.Dynamics;
using Colin.Common.SceneComponents.Physics.Dynamics.Solver;

namespace Colin.Common.SceneComponents.Physics.Collision.Handlers
{
    public delegate void AfterCollisionHandler( Fixture fixtureA, Fixture fixtureB, Contact contact, ContactVelocityConstraint impulse );
}