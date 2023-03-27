using Colin.Developments;
using Colin.Extensions;

namespace Colin.Common
{
    /// <summary>
    /// 为应在 <see cref="Scene.Draw"/> 中渲染的场景模块定义的接口.
    /// <br>标识一个可随场景渲染进行渲染的对象.</br>
    /// <para>[!] 不需要在类内对 <see cref="RenderTarget"/> 实例化, 
    /// <br>这一操作在 <see cref="SceneModeCollection"/> 加入该模块时自动实现.</br></para>
    /// </summary>
    public interface IRenderableSceneMode
    {
        /// <summary>
        /// 场景渲染目标.
        /// <br>用完记得还.</br>
        /// <br>[!] 不用自己初始化.</br>
        /// </summary>
        RenderTarget2D RenderTarget { get; set; }

        /// <summary>
        /// 指示对象是否启用渲染.
        /// </summary>
        bool Visiable { get; set; }

        SpriteSortMode SpriteSortMode { get; }

        BlendState BlendState { get; }

        SamplerState SamplerState { get; }

        DepthStencilState DepthStencilState { get; }

        RasterizerState RasterizerState { get; }

        Effect Effect { get; }

        Matrix? TransformMatrix { get; }

        void DoRender( );

        /// <summary>
        /// 使用类内参数执行 <see cref="SpriteBatch.Begin"/>.
        /// </summary>
        public void BatchBegin( )
        {
            EngineInfo.SpriteBatch.Begin( SpriteSortMode, BlendState, SamplerState, DepthStencilState, RasterizerState, Effect, TransformMatrix );
        }

        public void DoRender_SceneModeCollectionUseIt( )
        {
            EngineInfo.SpriteBatch.Begin( SpriteSortMode, BlendState, SamplerState, DepthStencilState, RasterizerState, Effect, TransformMatrix );
            DoRender( );
            EngineInfo.SpriteBatch.End( );
        }

        public void InitRenderTarget( )
        {
            RenderTarget = RenderTargetExtension.CreateDefault( );
        }

        public void OnClientSizeChanged( object o, EventArgs e )
        {
            InitRenderTarget( );
        }
    }
}