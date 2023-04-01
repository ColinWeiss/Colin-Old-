using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colin.Common.Graphics
{
    public class Material : IComparable<Material>, IDisposable
    {
        public static SamplerState DefaultSamplerState = new SamplerState
        {
            Filter = TextureFilter.Point
        };

        public static Material DefaultMaterial = new Material( );

        public static Material DefaultOpaqueMaterial = new Material( BlendState.Opaque );

        public BlendState BlendState = BlendState.AlphaBlend;

        public DepthStencilState DepthStencilState = DepthStencilState.None;

        public SamplerState SamplerState = DefaultSamplerState;

        public Effect Effect;

        public Material( )
        {
        }

        public Material( Effect effect )
        {
            Effect = effect;
        }

        public Material( BlendState blendState, Effect effect = null )
        {
            BlendState = blendState;
            Effect = effect;
        }

        public Material( DepthStencilState depthStencilState, Effect effect = null )
        {
            DepthStencilState = depthStencilState;
            Effect = effect;
        }

        ~Material( )
        {
            Dispose( );
        }

        public virtual void Dispose( )
        {
            if( BlendState != null && BlendState != BlendState.AlphaBlend )
            {
                BlendState.Dispose( );
                BlendState = null;
            }

            if( DepthStencilState != null && DepthStencilState != DepthStencilState.None )
            {
                DepthStencilState.Dispose( );
                DepthStencilState = null;
            }

            if( SamplerState != null && SamplerState != DefaultSamplerState )
            {
                SamplerState.Dispose( );
                SamplerState = null;
            }

            if( Effect != null )
            {
                Effect.Dispose( );
                Effect = null;
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
