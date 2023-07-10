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
        /// <param name="cut"></param>
        /// <param name="depth"></param>
        public static void DrawNineCut( this SpriteBatch spriteBatch, Texture2D texture, Color color, float x, float y, int Width, int Height, int cut, float depth )
        {
            Point borderSize = new Point( cut );
            Rectangle leftTop = new Rectangle( (int)x, (int)y, cut, cut );
            Rectangle rightTop = new Rectangle( (int)x + Width - cut, (int)y, cut, cut );
            Rectangle leftBottom = new Rectangle( (int)x, (int)y + Height - cut, cut, cut );
            Rectangle rightBottom = new Rectangle( (int)x + Width - cut, (int)y + Height - cut, cut, cut );
            Rectangle top = new Rectangle( (int)x + cut, (int)y, Width - cut * 2, cut );
            Rectangle left = new Rectangle( (int)x, (int)y + cut, cut, Height - cut * 2 );
            Rectangle right = new Rectangle( (int)x + Width - cut, (int)y + cut, cut, Height - cut * 2 );
            Rectangle bottom = new Rectangle( (int)x + cut, (int)y + Height - cut, Width - cut * 2, cut );
            Rectangle center = new Rectangle( (int)x + cut, (int)y + cut, Width - cut * 2, Height - cut * 2 );
            spriteBatch.Draw( texture, leftTop, new Rectangle( Point.Zero, borderSize ), color, 0f, Vector2.Zero, SpriteEffects.None, depth );
            spriteBatch.Draw( texture, rightTop, new Rectangle( new Point( texture.Width - cut, 0 ), borderSize ), color, 0f, Vector2.Zero, SpriteEffects.None, depth );
            spriteBatch.Draw( texture, leftBottom, new Rectangle( new Point( 0, texture.Height - cut ), borderSize ), color, 0f, Vector2.Zero, SpriteEffects.None, depth );
            spriteBatch.Draw( texture, rightBottom, new Rectangle( new Point( texture.Width - cut, texture.Height - cut ), borderSize ), color, 0f, Vector2.Zero, SpriteEffects.None, depth );
            spriteBatch.Draw( texture, top, new Rectangle( cut, 0, texture.Width - cut * 2, cut ), color, 0f, Vector2.Zero, SpriteEffects.None, depth );
            spriteBatch.Draw( texture, left, new Rectangle( 0, cut, cut, texture.Height - cut * 2 ), color, 0f, Vector2.Zero, SpriteEffects.None, depth );
            spriteBatch.Draw( texture, right, new Rectangle( texture.Width - cut, cut, cut, texture.Height - cut * 2 ), color, 0f, Vector2.Zero, SpriteEffects.None, depth );
            spriteBatch.Draw( texture, bottom, new Rectangle( cut, texture.Height - cut, texture.Width - cut * 2, cut ), color, 0f, Vector2.Zero, SpriteEffects.None, depth );
            spriteBatch.Draw( texture, center, new Rectangle( cut, cut, texture.Width - cut * 2, texture.Height - cut * 2 ), color, 0f, Vector2.Zero, SpriteEffects.None, depth );
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
        /// <param name="cut"></param>
        /// <param name="depth"></param>
        public static void DrawNineCut( this SpriteBatch spriteBatch, Texture2D texture, Color color, Vector2 pos, int Width, int Height, int cut, float depth )
        {
            spriteBatch.DrawNineCut( texture, color, (int)pos.X, (int)pos.Y, Width, Height, cut, depth );
        }

        /// <summary>
        /// 九宫绘制.
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="texture"></param>
        /// <param name="color"></param>
        /// <param name="pos"></param>
        /// <param name="size"></param>
        /// <param name="cut"></param>
        /// <param name="depth"></param>
        public static void DrawNineCut( this SpriteBatch spriteBatch, Texture2D texture, Color color, Vector2 pos, Point size, int cut, float depth )
        {
            spriteBatch.DrawNineCut( texture, color, (int)pos.X, (int)pos.Y, size.X, size.Y, cut, depth );
        }

        /// <summary>
        /// 九宫绘制.
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="texture"></param>
        /// <param name="color"></param>
        /// <param name="pos"></param>
        /// <param name="size"></param>
        /// <param name="cut"></param>
        /// <param name="depth"></param>
        public static void DrawNineCut( this SpriteBatch spriteBatch, Texture2D texture, Color color, Point pos, Point size, int cut, float depth )
        {
            spriteBatch.DrawNineCut( texture, color, pos.X, pos.Y, size.X, size.Y, cut, depth );
        }

        /// <summary>
        /// 九宫绘制.
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="texture"></param>
        /// <param name="color"></param>
        /// <param name="rec"></param>
        /// <param name="cut"></param>
        /// <param name="depth"></param>
        public static void DrawNineCut( this SpriteBatch spriteBatch, Texture2D texture, Color color, Rectangle rec, int cut, float depth )
        {
            spriteBatch.DrawNineCut( texture, color, rec.X, rec.Y, rec.Width, rec.Height, cut, depth );
        }
    }
}