using Colin.Common.SceneComponents.Physics.Collision.ContactSystem;
using Colin.Common.SceneComponents.Physics.Dynamics.Solver;

namespace Colin.Common.SceneComponents.Physics.Dynamics.Handlers
{
    public delegate void PostSolveHandler( Contact contact, ContactVelocityConstraint impulse );
}