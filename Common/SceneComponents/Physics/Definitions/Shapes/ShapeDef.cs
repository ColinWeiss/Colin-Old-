﻿using Colin.Common.SceneComponents.Physics.Collision.Shapes;

namespace Colin.Common.SceneComponents.Physics.Definitions.Shapes
{
    public abstract class ShapeDef : IDef
    {
        protected ShapeDef( ShapeType type )
        {
            ShapeType = type;
        }

        /// <summary>Gets or sets the density.</summary>
        public float Density { get; set; }

        /// <summary>Radius of the Shape</summary>
        public float Radius { get; set; }

        /// <summary>Get the type of this shape.</summary>
        public ShapeType ShapeType { get; }

        public virtual void SetDefaults( )
        {
            Density = 0;
            Radius = 0;
        }
    }
}