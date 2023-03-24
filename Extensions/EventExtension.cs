namespace Colin.Extensions
{
    public static class EventExtension
    {
        public static void EventRaise<TEventArgs>( this object sender, EventHandler<TEventArgs> handler, TEventArgs e )
        {
            handler?.Invoke( sender, e );
        }

        public static void EventRaise( this object sender, EventHandler handler, EventArgs e )
        {
            handler?.Invoke( sender, e );
        }
    }
}