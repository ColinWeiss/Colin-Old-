using Colin.Graphics;
using System.Runtime.Serialization;

namespace Colin.Common.SceneComponents.UserInterfaces.Renderers
{
    /// <summary>
    /// 允许控制 <see cref="FillPattern"/> 的绘制纹理的渲染.
    /// </summary>
    [Serializable]
    [DataContract( IsReference = true )]
    public sealed class PictureRenderer : ContainerRenderer
    {
        public Sprite Picture;

        [DataMember]
        public FillPattern FillPattern = FillPattern.Center;

        [DataMember]
        public SpriteFrame PictureFrame;

        public sealed override void RendererInit( )
        {
            PictureFrame = new SpriteFrame( );
            PictureFrame.IsPlay = false;
            PictureFrame.Width = Picture.Width;
            PictureFrame.Height = Picture.Height;
            PictureFrame.X = 0;
            PictureFrame.Y = 0;
        }

        public sealed override void Render( Container container )
        {
            PictureFrame.UpdateFrame( );
            if( Picture == null )
                return;
            switch( FillPattern )
            {
                case FillPattern.Stretching:
                    {
                        EngineInfo.SpriteBatch.Draw(
                            Picture.Source,
                            new Rectangle(
                                container.IsCanvas ?
                                Point.Zero : container.LayoutInfo.RenderLocation,
                                container.LayoutInfo.Size ),
                            PictureFrame.Frame,
                            container.DesignInfo.CurrentColor,
                            0f,
                            Vector2.Zero,
                            SpriteEffects.None,
                            Picture.Depth
                            );
                    }
                    break;
                case FillPattern.Center:
                    {
                        EngineInfo.SpriteBatch.Draw(
                            Picture.Source,
                                container.IsCanvas ?
                                Vector2.Zero : container.LayoutInfo.RenderLocationF + container.DesignInfo.OriginF ,
                            PictureFrame.Frame,
                            container.DesignInfo.CurrentColor,
                            0f,
                            container.DesignInfo.OriginF,
                            container.DesignInfo.CurrentScale,
                            SpriteEffects.None,
                            Picture.Depth
                            );
                    }
                    break;
            }
        }

        public PictureRenderer( Sprite picture )
        {
            Picture = picture;
        }

        public PictureRenderer( Texture2D picture )
        {
            Picture = new Sprite( picture );
        }

    }
}