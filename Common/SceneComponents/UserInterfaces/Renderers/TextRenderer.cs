using Colin.Resources;
using FontStashSharp;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Colin.Common.SceneComponents.UserInterfaces.Renderers
{
    /// <summary>
    /// 可渲染文本的渲染器.
    /// </summary>
    [DataContract( IsReference = true )]
    public class TextRenderer : ContainerRenderer
    {
        private string _text;
        [DataMember]
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                SetText( value );
            }
        }

        private bool _needRefresh = false;

        private Point _half;
        public Point Half => _half;

        private Vector2 _size;
        public Vector2 Size { get => _size; set { _size = value; } }

        [IgnoreDataMember]
        public SpriteFontBase Font;

        public void RefreshSize( Container container )
        {
            _size = Font.MeasureString( _text );
            container.LayoutInfo.SetSize( _size );
            _half = (_size / 2).ToPoint( );
            _needRefresh = false;
        }

        public override void RendererInit( ) { }
        public override void Render( Container container )
        {
            if( _needRefresh )
                RefreshSize( container );
            EngineInfo.SpriteBatch.DrawString( Font, Text, container.LayoutInfo.RenderLocation.ToVector2( ), container.DesignInfo.CurrentColor );
        }

        public void SetText( string text )
        {
            _needRefresh = true;
            _text = text;
        }

        public TextRenderer( )
        {
            Font = FontResource.Unifont.GetFont( 16 ); //默认字体.
        }

    }
}