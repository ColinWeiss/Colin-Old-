using Colin.Extensions;
using Colin.Graphics;

namespace Colin.Common
{
    /// <summary>
    /// 为应在 <see cref="Scene.Draw"/> 中渲染的场景模块定义的接口.
    /// <br>标识一个可随场景渲染进行渲染的对象.</br>
    /// <para>[!] 不需要在类内对 <see cref="RenderTarget"/> 实例化, 
    /// <br>这一操作在 <see cref="SceneComponentList"/> 加入该模块时自动实现.</br></para>
    /// </summary>
    public interface IRenderableSceneComponent
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

        Material Material { get; }

        Matrix? TransformMatrix { get; }

        void DoRender( );

        /// <summary>
        /// 使用类内参数执行 <see cref="SpriteBatch.Begin"/>.
        /// </summary>
        public void BatchBegin( )
        {
            EngineInfo.SpriteBatch.Begin( SpriteSortMode, Material.BlendState, Material.SamplerState, Material.DepthStencilState, null, Material.Effect, TransformMatrix );
        }

        public void DoRender_SceneModeCollectionUseIt( )
        {
            EngineInfo.SpriteBatch.Begin( SpriteSortMode, Material.BlendState, Material.SamplerState, Material.DepthStencilState, null, Material.Effect, TransformMatrix );
            DoRender( );
            EngineInfo.SpriteBatch.End( );
        }

        public void InitRenderTarget( )
        {
            RenderTarget = RenderTargetExt.CreateDefault( );
        }

        public void OnClientSizeChanged( object o, EventArgs e )
        {
            InitRenderTarget( );
        }

    }
}