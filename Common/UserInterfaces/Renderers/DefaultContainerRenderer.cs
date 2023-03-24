using System.Runtime.Serialization;

namespace Colin.Common.UserInterfaces.Renderers
{
    [Serializable]
    [DataContract( IsReference = true )]
    public class DefaultContainerRenderer : ContainerRenderer
    {
        public override void RendererInit( ) { }
        public override void RenderSelf( Container container ) { }
    }
}
