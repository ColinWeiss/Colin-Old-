using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colin.Common
{
    public class Camera
    {
        public Matrix view;

        public Matrix projection;

        public Vector2 position;
        public Vector2 positionLast;
        public Vector2 translate;

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

        private int _width;
        public int Width => _width;

        private int _height;
        public int Height => _height;

        public Point Size => new Point( Width , Height);
        public Vector2 SizeF => new Vector2( Width , Height );

        public void DoInitialize( int width, int height )
        {
            _width = width;
            _height = height;
            projection = Matrix.CreateOrthographicOffCenter( 0f, width, height, 0f, 0f, 1f );
            view = Matrix.Identity;
            translate = Vector2.Zero;
            ResetCamera( );
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
            positionLast = position;
            position += velocity;
            SetView( );
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

        private void SetView( )
        {
            Matrix matRotation = Matrix.CreateRotationZ( rotation );
            Matrix matZoom = Matrix.CreateScale( zoom );
            Vector3 trCenter = new Vector3( translate, 0f );
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
            t = EngineInfo.Graphics.GraphicsDevice.Viewport.Unproject( t, projection, view, Matrix.Identity );
            return new Vector2( t.X, t.Y );
        }

        public Vector2 ConvertWorldToScreen( Vector2 location )
        {
            Vector3 t = new Vector3( location, 0 );
            t = EngineInfo.Graphics.GraphicsDevice.Viewport.Project( t, projection, view, Matrix.Identity );
            return new Vector2( t.X, t.Y );
        }

    }
}