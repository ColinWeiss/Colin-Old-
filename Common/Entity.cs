namespace Colin.Common
{
    public class Entity
    {
        private static uint _idGenerator;

        public bool Enable = false;

        /// <summary>
        /// 实体唯一标识符.
        /// </summary>
        public readonly uint ID;

        /// <summary>
        /// 实体来源.
        /// </summary>
        public readonly ITraceable Source;

        public Transform2D Transform;

        public ComponentList Components;

        /// <summary>
        /// 构造一个实体.
        /// </summary>
        /// <param name="source">实体来源.</param>
        public Entity( ITraceable source )
        {
            Transform = new Transform2D( );
            ID = _idGenerator++;
            Source = source;
            Components = new ComponentList( this );
        }

        public void AddComponent( Component component ) => Components.AddComponent( component );

        public void DoUpdate( GameTime gameTime ) => Components.DoUpdate( gameTime );

        public void DoRender( ) => Components.DoRender( );

    }
}