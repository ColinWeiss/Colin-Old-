using Colin.Extensions;
using Colin.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colin.Modulars.UserInterfaces.Renderers
{
    public class NinecutRenderer : DivisionRenderer
    {
        private Sprite _sprite;
        public Sprite Sprite => _sprite;
        public int Cut;
        public override void RendererInit( ) { }
        public override void DoRender( SpriteBatch batch )
        {
            batch.DrawNineCut(
                _sprite.Source,
                Division.Design.Color,
                Division.Layout.TotalLocation,
                Division.Layout.Size,
                Cut,
                _sprite.Depth );
        }
        public void Bind( Sprite sprite ) => _sprite = sprite;
        public void Bind( Texture2D texture ) => _sprite = new Sprite( texture );
    }
}