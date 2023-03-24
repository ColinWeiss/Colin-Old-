namespace Colin.Common.UserInterfaces
{
    /// <summary>
    /// 用户交互界面核心.
    /// </summary>
    public sealed class UserInterface : ISceneMode, IUpdateableSceneMode, IDrawableSceneMode
    {
        /// <summary>
        /// 当前焦点容器.
        /// </summary>
        public static Container CurrentFocu = null;

        public Scene Scene { get; set; }

        public bool Enable { get; set; }

        public bool Visiable { get; set; }
        public SpriteSortMode SpriteSortMode => SpriteSortMode.Deferred;
        public BlendState BlendState => BlendState.AlphaBlend;
        public SamplerState SamplerState => SamplerState.PointClamp;
        public DepthStencilState DepthStencilState { get; }
        public RasterizerState RasterizerState { get; }
        public Effect Effect { get; }
        public Matrix? TransformMatrix { get; }

        public RenderTarget2D RenderTarget { get; set; }

        public RenderTarget2D RenderTargetSwap { get; set; }

        /// <summary>
        /// 当前使用的容器页.
        /// </summary>
        public ContainerPage Page { get; private set; } = new ContainerPage( );

        public void SetDefault( ) { }

        public void DoUpdate( )
        {
            Page?.DoUpdate( );
        }
        public void DoRender( ) => Page?.DoRender( );

        public void SavePage( string path )
        {

        }

        /// <summary>
        /// 设置当前容器页.
        /// </summary>
        /// <param name="containerPage">容器页.</param>
        /// <param name="doInit">指示是否执行初始化.</param>
        public void SetPage( ContainerPage containerPage, bool doInit )
        {
            Page = containerPage;
            Page.UserInterface = this;
            if( doInit )
                Page.DoInitialize( );
        }

        public void Register( Container container ) => Page?.Register( container );

    }
}