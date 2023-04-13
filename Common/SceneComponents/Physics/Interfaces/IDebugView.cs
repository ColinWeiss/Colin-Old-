using Colin.Common.SceneComponents.Physics.Collision.Shapes;
using Colin.Common.SceneComponents.Physics.Dynamics.Joints;
using Colin.Common.SceneComponents.Physics.Shared;

namespace Colin.Common.SceneComponents.Physics.Interfaces
{
    public interface IDebugView
    {
        void DrawJoint( Joint joint );
        void DrawShape( Shape shape, ref Transform transform, Color color );
    }
}