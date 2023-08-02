﻿using Colin.Graphics;
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
            _pixel = PreLoadResource.Pixel;
        }
        public override void DoRender( SpriteBatch batch )
        {
            batch.Draw(
              _pixel.Source,
              Division.Layout.TotalLocationF + Division.Design.Anchor,
              Division.Layout.TotalHitBox,
              Division.Design.Color,
              Division.Design.Rotation,
              Division.Design.Anchor,
              Division.Design.Scale,
              SpriteEffects.None,
              _pixel.Depth );
        }
        public PixelRenderer SetDesignColor( Color color )
        {
            Division.Design.Color = color;
            return this;
        }
        public PixelRenderer SetDesignColor( Color color, int a = 255 )
        {
            Division.Design.Color = new Color( color, a );
            return this;
        }
        public PixelRenderer SetDesignColor( int r, int g, int b, int a = 255 )
        {
            SetDesignColor( new Color( r, g, b, a ) );
            return this;
        }
    }
}