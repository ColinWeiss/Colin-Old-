using Colin.Common.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colin.Common.ECS
{
    public class RenderableComponent : Component
    {
        public bool Visiable;

        public Material Material = null;

        public virtual void DoRender( ) { }

    }
}