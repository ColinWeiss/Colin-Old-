using Colin.Graphics;
using Colin.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colin.Modulars.UserInterfaces.Renderers
{
    public class PixelRenderer : DivisionRenderer
    {
        private Sprite _pixel;
        public override void RendererInit( )
        {
            _pixel = PreloadResource.Pixel;
        }
        public override void DoRender( SpriteBatch batch )
        {
            batch.Draw(
              _pixel.Source,
              Division.Layout.TotalLocationF,
              Division.Layout.TotalHitBox,
              Division.Design.Color,
              Division.Design.Rotation,
              Division.Design.Anchor,
              Division.Design.Scale,
              SpriteEffects.None,
              _pixel.Depth );
        }
    }
}