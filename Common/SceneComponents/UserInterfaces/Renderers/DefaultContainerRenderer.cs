using System.Runtime.Serialization;

namespace Colin.Common.SceneComponents.UserInterfaces.Renderers
{
    [Serializable]
    [DataContract( IsReference = true )]
    public class DefaultContainerRenderer : ContainerRenderer
    {
        public override void RendererInit( ) { }
        public override void Render( Container container ) { }
    }
}
