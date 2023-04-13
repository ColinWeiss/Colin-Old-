namespace Colin.Common.SceneComponents.Physics.Shared.Contracts
{
    public class RequiredException : Exception
    {
        public RequiredException( string message ) : base( message ) { }
    }
}