using Colin.Common.Physics.Collision.Shapes;
using Colin.Common.Physics.Dynamics.Joints;
using Colin.Common.Physics.Shared;

namespace Colin.Common.Physics.Interfaces
{
    public interface IDebugView
    {
        void DrawJoint( Joint joint );
        void DrawShape( Shape shape, ref Transform transform, Color color );
    }
}