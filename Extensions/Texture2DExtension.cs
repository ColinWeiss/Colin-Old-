namespace Colin.Extensions
{
    public static class Texture2DExtension
    {
        public static Vector2 GetSize( this Texture2D texture )
        {
            return new Vector2( texture.Width, texture.Height );
        }
    }
}
