using Colin.Common.Physics.Collision.ContactSystem;
using Colin.Common.Physics.Dynamics;
using Colin.Common.Physics.Dynamics.Solver;

namespace Colin.Common.Physics.Collision.Handlers
{
    public delegate void AfterCollisionHandler( Fixture fixtureA, Fixture fixtureB, Contact contact, ContactVelocityConstraint impulse );
}