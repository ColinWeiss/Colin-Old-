using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colin.Common.SceneComponents.Tiles
{
    public class TileBehaviorCollection
    {
        public int Width { get; }
        public int Height { get; }
        private TileBehavior[ ] _behaviors;
        public TileBehavior this[int index] => _behaviors[index];
        public TileBehavior this[int x, int y] => _behaviors[x + y * Width];
        public TileBehaviorCollection( int width, int height )
        {
            Width = width;
            Height = height;
            _behaviors = new TileBehavior[width * height];
            for( int count = 0; count < _behaviors.Length; count++ )
            {
                _behaviors[count] = new TileBehavior( );
            }
        }

        public void SetBehavior<T>( int x, int y ) where T : TileBehavior, new()
        {
            _behaviors[x + y * Width] = new T( );
            T _behavior = _behaviors[x + y * Width] as T;
            _behavior.SetDefaults( );
        }

        public void ClearBehavior( int x , int y )
        {
            _behaviors[x + y * Width] = new TileBehavior( );
        }

    }
}