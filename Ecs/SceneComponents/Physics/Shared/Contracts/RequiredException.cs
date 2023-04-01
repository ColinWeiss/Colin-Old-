namespace Colin.Common.Physics.Shared.Contracts
{
    public class RequiredException : Exception
    {
        public RequiredException( string message ) : base( message ) { }
    }
}