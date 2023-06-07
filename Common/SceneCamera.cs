namespace Colin.Common
{
    /// <summary>
    /// 场景摄像机.
    /// </summary>
    public class SceneCamera : Camera, ISceneComponent
    {
        public void DoInitialize( )
        {
            DoInitialize( EngineInfo.Graphics.GraphicsDevice.Viewport.Width, EngineInfo.Graphics.GraphicsDevice.Viewport.Height );
            EngineInfo.Engine.Window.ClientSizeChanged += ( s, e ) =>
            {
                projection = Matrix.CreateOrthographicOffCenter( 0f, EngineInfo.Graphics.GraphicsDevice.Viewport.Width, EngineInfo.Graphics.GraphicsDevice.Viewport.Height, 0f, 0f, 1f );
                view = Matrix.Identity;
                ResetCamera( );
            };
        }
    }
}