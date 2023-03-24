namespace Colin.Extensions
{
    public static class RandomExtension
    {
        public static bool NextBool( this Random rand )
        {
            return rand.Next( 2 ) == 1;
        }
        public static Vector2 NextVectorUnit( this Random rand )
        {
            float x = (float)(rand.NextBool( ) ? rand.NextDouble( ) : -rand.NextDouble( ));
            float y = (float)(rand.NextBool( ) ? rand.NextDouble( ) : -rand.NextDouble( ));
            return new Vector2( x, y );
        }
        public static Vector2 NextVectorRec( this Random rand, Point size )
        {
            float x = (float)rand.NextDouble( ) * size.X;
            float y = (float)rand.NextDouble( ) * size.Y;
            return new Vector2( x, y );
        }
    }
}
