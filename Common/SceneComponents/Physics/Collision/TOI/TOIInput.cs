using Colin.Common.SceneComponents.Physics.Collision.Distance;

namespace Colin.Common.SceneComponents.Physics.Collision.TOI
{
    /// <summary>Input parameters for CalculateTimeOfImpact</summary>
    public struct TOIInput
    {
        public DistanceProxy ProxyA;
        public DistanceProxy ProxyB;
        public Sweep SweepA;
        public Sweep SweepB;
        public float TMax; // defines sweep interval [0, tMax]
    }
}