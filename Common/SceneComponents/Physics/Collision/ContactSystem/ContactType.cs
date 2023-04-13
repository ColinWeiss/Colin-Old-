namespace Colin.Common.SceneComponents.Physics.Collision.ContactSystem
{
    public enum ContactType : byte
    {
        NotSupported,
        Polygon,
        PolygonAndCircle,
        Circle,
        EdgeAndPolygon,
        EdgeAndCircle,
        ChainAndPolygon,
        ChainAndCircle
    }
}