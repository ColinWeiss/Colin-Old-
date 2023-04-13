namespace Colin.Common
{
    public class Camera : ISceneComponent
    {
        private static GraphicsDevice _graphics;

        public Vector2 TranslateCenter;

        public Matrix View;

        public Matrix Projection;

        public Vector2 LeftTop => Position - EngineInfo.ViewCenter * Zoom;

        public Vector2 Position;

        public float Rotation;

        public float Zoom;

        public bool Trace = true;

        public Vector2 Velocity;

        public float RotationVelocity;

        public Vector2 TargetPosition;

        public float TargetRotation;

        public bool Enable { get; set; }

        public Scene Scene { get; set; }

        public Matrix Transform => View * Projection;

        public void DoInitialize( )
        {
            _graphics = EngineInfo.Engine.GraphicsDevice;
            Projection = Matrix.CreateOrthographicOffCenter( 0f, _graphics.Viewport.Width, _graphics.Viewport.Height, 0f, 0f, 1f );
            View = Matrix.Identity;
            TranslateCenter = new Vector2( _graphics.Viewport.Width / 2f, _graphics.Viewport.Height / 2f );
            ResetCamera( );
            EngineInfo.Engine.Window.ClientSizeChanged += ( s, e ) =>
            {
                Projection = Matrix.CreateOrthographicOffCenter( 0f, _graphics.Viewport.Width, _graphics.Viewport.Height, 0f, 0f, 1f );
                View = Matrix.Identity;
                TranslateCenter = new Vector2( _graphics.Viewport.Width / 2f, _graphics.Viewport.Height / 2f );
                ResetCamera( );
            };
        }

        public void MoveCamera( Vector2 amount )
        {
            Position += amount;
            TargetPosition = Position;
        }

        public void RotateCamera( float amount )
        {
            Rotation += amount;
        }

        public void ResetCamera( )
        {
            Rotation = 0f;
            Zoom = 1f;
            SetView( );
        }

        public void DoUpdate( GameTime time )
        {
            if( Trace )
            {
                Velocity = (TargetPosition - Position) * 0.1f;
                RotationVelocity = (TargetRotation - Rotation) * 0.1f;
                if( Vector2.Distance( TargetPosition, Position ) < 1 )
                    Position = TargetPosition;
                if( Math.Abs( Rotation - RotationVelocity ) < 0.017f )
                    Rotation = RotationVelocity;
            }
            Rotation += RotationVelocity;
            Position += Velocity;
            SetView( );
        }

        private void SetView( )
        {
            Matrix matRotation = Matrix.CreateRotationZ( Rotation );
            Matrix matZoom = Matrix.CreateScale( Zoom );
            Vector3 translateCenter = new Vector3( TranslateCenter, 0f );
            Vector3 translateBody = new Vector3( -Position, 0f );
            View =
                Matrix.CreateTranslation( translateBody ) *
                matZoom *
                matRotation *
                Matrix.CreateTranslation( translateCenter );

        }

        public Vector2 ConvertScreenToWorld( Vector2 location )
        {
            Vector3 t = new Vector3( location, 0 );
            t = _graphics.Viewport.Unproject( t, Projection, View, Matrix.Identity );
            return new Vector2( t.X, t.Y );
        }

        public Vector2 ConvertWorldToScreen( Vector2 location )
        {
            Vector3 t = new Vector3( location, 0 );
            t = _graphics.Viewport.Project( t, Projection, View, Matrix.Identity );
            return new Vector2( t.X, t.Y );
        }

    }
}