using Colin.Common.SceneComponents.Physics.Collision.ContactSystem;
using Colin.Common.SceneComponents.Physics.Dynamics;

namespace Colin.Common.SceneComponents.Physics.Collision.Handlers
{
    public delegate void OnSeparationHandler( Fixture fixtureA, Fixture fixtureB, Contact contact );
}