using Colin.Common.UserInterfaces;
using Colin.Developments;
using Colin.Resources;
using Colin.Extensions;
using FontStashSharp;
using System.Runtime.Serialization;

namespace Colin.Common.UserInterfaces.Renderers
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

        public override void RendererInit( ) { }
        public override void RenderSelf( Container container )
        {
            if( _needRefresh )
            {
                _size = Font.MeasureString( _text );
                container.LayoutInfo.SetSize( _size );
                _half = (_size / 2).ToPoint( );
                _needRefresh = false;
            }
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