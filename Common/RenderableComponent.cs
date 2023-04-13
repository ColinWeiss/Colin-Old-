using Colin.Graphics;

namespace Colin.Common
{
    public class RenderableComponent : Component
    {
        public bool Visiable;

        public Material Material = null;

        public virtual void DoRender( ) { }

    }
}