using Colin.Common.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colin.Common.ECS
{
    public class EntityCore : ISceneComponent , IRenderableSceneComponent
    {
        public Scene Scene { get; set; }

        public RenderTarget2D RenderTarget { get; set; }

        public SpriteSortMode SpriteSortMode { get; }

        public Material Material { get; }

        public Matrix? TransformMatrix { get; }

        public bool Enable { get; set; }

        public bool Visiable { get; set; }

        public List<Entity> Entities = new List<Entity>( );

        public void DoInitialize( )
        {

        }

        public void DoRender( )
        {

        }

        public void DoUpdate( GameTime time )
        {

        }
    }
}