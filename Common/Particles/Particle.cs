using Colin.Common.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colin.Common.Particles
{
    /// <summary>
    /// 粒子系统.
    /// 在不同画质下有不同的表现.
    /// </summary>
    public class Particle : ISceneComponent, IRenderableSceneComponent
    {
        public RenderTarget2D RenderTarget { get; set; }
        public bool Visiable { get; set; }
        public SpriteSortMode SpriteSortMode { get; }
        public Material Material => Material.DefaultMaterial;
        public Matrix? TransformMatrix { get; }
        public Scene Scene { get; set; }
        public bool Enable { get; set; }

        public void DoInitialize( )
        {

        }

        public void DoUpdate( GameTime time )
        {

        }

        public void DoRender( )
        {

        }
    }
}