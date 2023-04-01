using Colin.Common.Graphics;

namespace Colin.Common.Skys
{
    internal class Sky : ISceneComponent, IRenderableSceneComponent
    {
        public Scene Scene { get; set; }

        public bool Enable { get; set; }

        public bool Visiable { get; set; }
        public RenderTarget2D RenderTarget { get; set; }
        public RenderTarget2D RenderTargetSwap { get; set; }
        public SpriteSortMode SpriteSortMode { get; }
        public Material Material => Material.DefaultMaterial;
        public Matrix? TransformMatrix { get; }

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