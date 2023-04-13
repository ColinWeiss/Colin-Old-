using System.Runtime.Serialization;

namespace Colin.Common.SceneComponents.UserInterfaces.Renderers
{
    [Serializable]
    [DataContract( IsReference = true )]
    public abstract class ContainerRenderer
    {
        public abstract void RendererInit( );
        public abstract void Render( Container container );
    }
}