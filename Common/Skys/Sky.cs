namespace Colin.Common.Skys
{
    internal class Sky : ISceneMode, IUpdateableSceneMode, IRenderableSceneMode
    {
        public Scene Scene { get; set; }

        public bool Enable { get; set; }

        public bool Visiable { get; set; }
        public RenderTarget2D RenderTarget { get; set; }
        public RenderTarget2D RenderTargetSwap { get; set; }
        public SpriteSortMode SpriteSortMode { get; }
        public BlendState BlendState { get; }
        public SamplerState SamplerState { get; }
        public DepthStencilState DepthStencilState { get; }
        public RasterizerState RasterizerState { get; }
        public Effect Effect { get; }
        public Matrix? TransformMatrix { get; }

        public void SetDefault( )
        {

        }
        public void DoUpdate( )
        {

        }
        public void DoRender( )
        {

        }
    }
}