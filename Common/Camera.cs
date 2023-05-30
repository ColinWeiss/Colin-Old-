namespace Colin.Common
{
    public class Camera : ISceneComponent
    {
        private static GraphicsDevice _graphics;

        public Vector2 translateCenter;

        public Matrix view;

        public Matrix projection;

        public Vector2 LeftTop => position - EngineInfo.ViewCenter * zoom;

        public Vector2 position;

        public float rotation;

        public float zoom;

        public bool trace = true;

        public Vector2 velocity;

        public float rotationVelocity;

        public Vector2 targetPosition;

        public float targetRotation;

        public bool Enable { get; set; }

        public Scene Scene { get; set; }

        public Matrix Transform => view * projection;

        public void DoInitialize( )
        {
            _graphics = EngineInfo.Engine.GraphicsDevice;
            projection = Matrix.CreateOrthographicOffCenter( 0f, _graphics.Viewport.Width, _graphics.Viewport.Height, 0f, 0f, 1f );
            view = Matrix.Identity;
            translateCenter = new Vector2( _graphics.Viewport.Width / 2f, _graphics.Viewport.Height / 2f );
            ResetCamera( );
            EngineInfo.Engine.Window.ClientSizeChanged += ( s, e ) =>
            {
                projection = Matrix.CreateOrthographicOffCenter( 0f, _graphics.Viewport.Width, _graphics.Viewport.Height, 0f, 0f, 1f );
                view = Matrix.Identity;
                translateCenter = new Vector2( _graphics.Viewport.Width / 2f, _graphics.Viewport.Height / 2f );
                ResetCamera( );
            };
        }

        public void MoveCamera( Vector2 amount )
        {
            position += amount;
            targetPosition = position;
        }

        public void RotateCamera( float amount )
        {
            rotation += amount;
        }

        public void ResetCamera( )
        {
            rotation = 0f;
            zoom = 1f;
            SetView( );
        }

        public void DoUpdate( GameTime time )
        {
            if( trace )
            {
                velocity = (targetPosition - position) * 0.1f;
                rotationVelocity = (targetRotation - rotation) * 0.1f;
                if( Vector2.Distance( targetPosition, position ) < 1 )
                    position = targetPosition;
                if( Math.Abs( rotation - rotationVelocity ) < 0.017f )
                    rotation = rotationVelocity;
            }
            rotation += rotationVelocity;
            position += velocity;
            SetView( );
        }

        private void SetView( )
        {
            Matrix matRotation = Matrix.CreateRotationZ( rotation );
            Matrix matZoom = Matrix.CreateScale( zoom );
            Vector3 trCenter = new Vector3( translateCenter, 0f );
            Vector3 translateBody = new Vector3( -position, 0f );
            view =
                Matrix.CreateTranslation( translateBody ) *
                matZoom *
                matRotation *
                Matrix.CreateTranslation( trCenter );

        }

        public Vector2 ConvertScreenToWorld( Vector2 location )
        {
            Vector3 t = new Vector3( location, 0 );
            t = _graphics.Viewport.Unproject( t, projection, view, Matrix.Identity );
            return new Vector2( t.X, t.Y );
        }

        public Vector2 ConvertWorldToScreen( Vector2 location )
        {
            Vector3 t = new Vector3( location, 0 );
            t = _graphics.Viewport.Project( t, projection, view, Matrix.Identity );
            return new Vector2( t.X, t.Y );
        }

    }
}