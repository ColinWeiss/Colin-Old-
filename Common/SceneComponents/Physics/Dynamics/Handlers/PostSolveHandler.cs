using Colin.Common.Physics.Collision.ContactSystem;
using Colin.Common.Physics.Dynamics.Solver;

namespace Colin.Common.Physics.Dynamics.Handlers
{
    public delegate void PostSolveHandler( Contact contact, ContactVelocityConstraint impulse );
}