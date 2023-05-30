namespace Colin.Graphics
{
    public class Material : IComparable<Material>, IDisposable
    {
        private static SamplerState _defaultSamplerState = new SamplerState
        {
            Filter = TextureFilter.Point,
            BorderColor = Color.Red
        };
        public static SamplerState DefaultSamplerState => _defaultSamplerState;

        private static Material _defaultMaterial = new Material( );
        public static Material DefaultMaterial => _defaultMaterial;

        private static Material _defaultOpaqueMaterial = new Material( BlendState.Opaque );
        public static Material DefaultOpaqueMaterial => _defaultOpaqueMaterial;

        public BlendState blendState = BlendState.AlphaBlend;

        public DepthStencilState depthStencilState = DepthStencilState.None;

        public SamplerState samplerState = DefaultSamplerState;

        public Effect effect;

        public Material( )
        {
        }

        public Material( Effect effect )
        {
            this.effect = effect;
        }

        public Material( BlendState blendState, Effect effect = null )
        {
            this.blendState = blendState;
            this.effect = effect;
        }

        public Material( DepthStencilState depthStencilState, Effect effect = null )
        {
            this.depthStencilState = depthStencilState;
            this.effect = effect;
        }

        ~Material( )
        {
            Dispose( );
        }

        public virtual void Dispose( )
        {
            if( blendState != null && blendState != BlendState.AlphaBlend )
            {
                blendState.Dispose( );
                blendState = null;
            }

            if( depthStencilState != null && depthStencilState != DepthStencilState.None )
            {
                depthStencilState.Dispose( );
                depthStencilState = null;
            }

            if( samplerState != null && samplerState != DefaultSamplerState )
            {
                samplerState.Dispose( );
                samplerState = null;
            }

            if( effect != null )
            {
                effect.Dispose( );
                effect = null;
            }
        }

        public int CompareTo( Material other )
        {
            if( ReferenceEquals( other, null ) )
                return 1;
            if( ReferenceEquals( this, other ) )
                return 0;
            return -1;
        }

    }
}
