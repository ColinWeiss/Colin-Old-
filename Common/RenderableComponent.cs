﻿using Colin.Graphics;

namespace Colin.Common
{
    public class RenderableComponent : Component
    {
        public bool Visiable;

        public virtual void DoRender( ) { }

    }
}