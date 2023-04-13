namespace Colin.Common.SceneComponents.Tiles.Events
{
    public class TileEvent : EventArgs
    {
        public string Name;

        public TileInfo? TileInfo { get; private set; }

        public TileEvent( TileInfo tileInfo )
        {
            TileInfo = tileInfo;
        }
    }
}