using Colin.Common.Physics.Collision.ContactSystem;
using Colin.Common.Physics.Dynamics;

namespace Colin.Common.Physics.Collision.Handlers
{
    public delegate void OnSeparationHandler( Fixture fixtureA, Fixture fixtureB, Contact contact );
}