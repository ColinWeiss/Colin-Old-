namespace Colin.Extensions
{
    /// <summary>
    /// 与 <see cref="SpriteBatch"/> 相关的扩展方法.
    /// </summary>
    public static class SpriteBatchExt
    {
        /// <summary>
        /// 九宫绘制.
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="texture"></param>
        /// <param name="color"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        /// <param name="borderCut"></param>
        /// <param name="depth"></param>
        public static void DrawNineCut( this SpriteBatch spriteBatch, Texture2D texture, Color color, float x, float y, int Width, int Height, int borderCut, float depth )
        {
            Point borderSize = new Point( borderCut );
            Rectangle leftTop = new Rectangle( (int)x, (int)y, borderCut, borderCut );
            Rectangle rightTop = new Rectangle( (int)x + Width - borderCut, (int)y, borderCut, borderCut );
            Rectangle leftBottom = new Rectangle( (int)x, (int)y + Height - borderCut, borderCut, borderCut );
            Rectangle rightBottom = new Rectangle( (int)x + Width - borderCut, (int)y + Height - borderCut, borderCut, borderCut );
            Rectangle top = new Rectangle( (int)x + borderCut, (int)y, Width - borderCut * 2, borderCut );
            Rectangle left = new Rectangle( (int)x, (int)y + borderCut, borderCut, Height - borderCut * 2 );
            Rectangle right = new Rectangle( (int)x + Width - borderCut, (int)y + borderCut, borderCut, Height - borderCut * 2 );
            Rectangle bottom = new Rectangle( (int)x + borderCut, (int)y + Height - borderCut, Width - borderCut * 2, borderCut );
            Rectangle center = new Rectangle( (int)x + borderCut, (int)y + borderCut, Width - borderCut * 2, Height - borderCut * 2 );
            spriteBatch.Draw( texture, leftTop, new Rectangle( Point.Zero, borderSize ), color, 0f, Vector2.Zero, SpriteEffects.None, depth );
            spriteBatch.Draw( texture, rightTop, new Rectangle( new Point( texture.Width - borderCut, 0 ), borderSize ), color, 0f, Vector2.Zero, SpriteEffects.None, depth );
            spriteBatch.Draw( texture, leftBottom, new Rectangle( new Point( 0, texture.Height - borderCut ), borderSize ), color, 0f, Vector2.Zero, SpriteEffects.None, depth );
            spriteBatch.Draw( texture, rightBottom, new Rectangle( new Point( texture.Width - borderCut, texture.Height - borderCut ), borderSize ), color, 0f, Vector2.Zero, SpriteEffects.None, depth );
            spriteBatch.Draw( texture, top, new Rectangle( borderCut, 0, texture.Width - borderCut * 2, borderCut ), color, 0f, Vector2.Zero, SpriteEffects.None, depth );
            spriteBatch.Draw( texture, left, new Rectangle( 0, borderCut, borderCut, texture.Height - borderCut * 2 ), color, 0f, Vector2.Zero, SpriteEffects.None, depth );
            spriteBatch.Draw( texture, right, new Rectangle( texture.Width - borderCut, borderCut, borderCut, texture.Height - borderCut * 2 ), color, 0f, Vector2.Zero, SpriteEffects.None, depth );
            spriteBatch.Draw( texture, bottom, new Rectangle( borderCut, texture.Height - borderCut, texture.Width - borderCut * 2, borderCut ), color, 0f, Vector2.Zero, SpriteEffects.None, depth );
            spriteBatch.Draw( texture, center, new Rectangle( borderCut, borderCut, texture.Width - borderCut * 2, texture.Height - borderCut * 2 ), color, 0f, Vector2.Zero, SpriteEffects.None, depth );
        }

        /// <summary>
        /// 九宫绘制.
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="texture"></param>
        /// <param name="color"></param>
        /// <param name="pos"></param>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        /// <param name="borderCut"></param>
        /// <param name="depth"></param>
        public static void DrawNineCut( this SpriteBatch spriteBatch, Texture2D texture, Color color, Vector2 pos, int Width, int Height, int borderCut, float depth )
        {
            spriteBatch.DrawNineCut( texture, color, (int)pos.X, (int)pos.Y, Width, Height, borderCut, depth );
        }

        /// <summary>
        /// 九宫绘制.
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="texture"></param>
        /// <param name="color"></param>
        /// <param name="pos"></param>
        /// <param name="size"></param>
        /// <param name="borderCut"></param>
        /// <param name="depth"></param>
        public static void DrawNineCut( this SpriteBatch spriteBatch, Texture2D texture, Color color, Vector2 pos, Point size, int borderCut, float depth )
        {
            spriteBatch.DrawNineCut( texture, color, (int)pos.X, (int)pos.Y, size.X, size.Y, borderCut, depth );
        }

        /// <summary>
        /// 九宫绘制.
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="texture"></param>
        /// <param name="color"></param>
        /// <param name="pos"></param>
        /// <param name="size"></param>
        /// <param name="borderCut"></param>
        /// <param name="depth"></param>
        public static void DrawNineCut( this SpriteBatch spriteBatch, Texture2D texture, Color color, Point pos, Point size, int borderCut, float depth )
        {
            spriteBatch.DrawNineCut( texture, color, pos.X, pos.Y, size.X, size.Y, borderCut, depth );
        }

        /// <summary>
        /// 九宫绘制.
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="texture"></param>
        /// <param name="color"></param>
        /// <param name="rec"></param>
        /// <param name="borderCut"></param>
        /// <param name="depth"></param>
        public static void DrawNineCut( this SpriteBatch spriteBatch, Texture2D texture, Color color, Rectangle rec, int borderCut, float depth )
        {
            spriteBatch.DrawNineCut( texture, color, rec.X, rec.Y, rec.Width, rec.Height, borderCut, depth );
        }
    }
}