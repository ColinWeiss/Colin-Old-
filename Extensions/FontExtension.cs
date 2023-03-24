using FontStashSharp;

namespace Colin.Extensions
{
    public static class FontExtension
    {
        public static Vector2 Size( this Bounds bounds )
        {
            return new Vector2( bounds.X2 - bounds.X, bounds.Y2 - bounds.Y );
        }
    }
}