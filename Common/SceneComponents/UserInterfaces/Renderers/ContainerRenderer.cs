using System.Runtime.Serialization;

namespace Colin.Common.UserInterfaces.Renderers
{
    [Serializable]
    [DataContract( IsReference = true )]
    public abstract class ContainerRenderer
    {
        public abstract void RendererInit( );
        public abstract void RenderSelf( Container container );
    }
}