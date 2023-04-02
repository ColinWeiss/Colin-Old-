using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colin.Common
{
    public class Entity
    {
        private static uint _idGenerator;

        /// <summary>
        /// 实体唯一标识符.
        /// </summary>
        public readonly uint ID;

        /// <summary>
        /// 实体来源.
        /// </summary>
        public readonly ITraceable Source;

        /// <summary>
        /// 构造一个实体.
        /// </summary>
        /// <param name="source">实体来源.</param>
        public Entity( ITraceable source )
        {
            ID = _idGenerator++;
            Source = source;
        }

        public Transform2D Transform;

        public bool Enable = false;

        public ComponentList Components = new ComponentList( );


        public void DoUpdate( GameTime gameTime ) => Components.DoUpdate( gameTime );

        public void DoRender( ) => Components.DoRender( );

    }
}