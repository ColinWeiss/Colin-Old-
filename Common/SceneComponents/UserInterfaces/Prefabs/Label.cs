using Colin.Common.UserInterfaces.Renderers;
using Colin.Extensions;
using System.Runtime.Serialization;

namespace Colin.Common.UserInterfaces.Prefabs
{
    [Serializable]
    [DataContract( IsReference = true, Name = "Label" )]
    public class Label : Container
    {
        public string Text
        {
            get => Renderer is TextRenderer textRender ? textRender.Text : "null";
            set
            {
                if( Renderer is TextRenderer textRender )
                    textRender.Text = value;
            }
        }

        public Label( )
        {
            Renderer = new TextRenderer( );
        }
        public override void LayoutInfoUpdate( ref LayoutInfo info )
        {
            if( Renderer != null && Renderer is TextRenderer textRender )
                info.SetSize( textRender.Size );
            base.LayoutInfoUpdate( ref info );
        }
    }
}