﻿using Colin.Common;
using Colin.Graphics;
using Colin.Resources;

namespace Colin.Modulars.Backgrounds
{
    /// <summary>
    /// 场景背景.
    /// <br>仅支持将其加入 <see cref="Gameworld"/> 场景中.</br>
    /// <br>加入其他场景可能会发生预料不到的后果.</br>
    /// </summary>
    public sealed class Background : ISceneComponent, IRenderableSceneComponent
    {
        public Scene Scene { get; set; }

        public bool Enable { get; set; }

        public bool Visiable { get; set; }

        private SceneCamera _camera;
        public SceneCamera Camera => _camera;

        public RenderTarget2D SceneRt { get; set; }

        public BackgroundStyle CurrentStyle { get; private set; }

        /// <summary>
        /// 绘制左右循环图层所使用的着色器文件.
        /// </summary>
        private Effect LeftRightLoopEffect;

        private Texture2D _screenMap;

        /// <summary>
        /// 设置该场景所使用的摄像机.
        /// </summary>
        /// <param name="camera">摄像机.</param>
        public void SetCamera(SceneCamera camera)
        {
            _camera = camera;
        }

        /// <summary>
        /// 设置背景样式.
        /// </summary>
        /// <param name="style"></param>
        public void SetBackgroundStyle(BackgroundStyle style)
        {
            if (CurrentStyle != style)
                CurrentStyle = style;
        }

        public void DoInitialize()
        {
            _screenMap = PreloadResource.Pixel.Source;
            LeftRightLoopEffect = EffectResource.GetAsset("LeftRightLoopMapping");
        }

        public void DoUpdate(GameTime time)
        {
            CurrentStyle?.UpdateStyle();
        }

        public void DoRender(SpriteBatch batch )
        {
            if (CurrentStyle != null)
            {
                BackgroundLayer layer;
                for (int count = 0; count < CurrentStyle.Layers.Count; count++)
                {
                    layer = CurrentStyle.Layers[count];
                    if (layer.IsFix)
                        RenderFixBackground(layer);
                    if (layer.IsLoop && layer.LoopStyle == BackgroundLoopStyle.LeftRightConnect)
                        RenderLeftRightLoopBackground(layer);
                }
            }
        }

        public void RenderFixBackground(BackgroundLayer layer)
        {
            EngineInfo.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp);
            EngineInfo.SpriteBatch.Draw(
                layer.Sprite.Source,
                CurrentStyle.FixLayerOverallOffset,
                null,
                Color.White,
                0f,
                Vector2.Zero,
                CurrentStyle.FixLayerScale,
                SpriteEffects.None,
                layer.Sprite.Depth
                );
            EngineInfo.SpriteBatch.End();
        }

        public void RenderLeftRightLoopBackground(BackgroundLayer layer)
        {
            Vector3 translateBody = new Vector3(-(Camera.position - CurrentStyle.LoopLayerDrawPosition) * layer.Parallax, 0f);
            Vector3 translateCenter = new Vector3(Camera.translate, 0f);
            Vector2 drawCount = new Vector2((float)EngineInfo.ViewWidth / layer.Sprite.Width, (float)EngineInfo.ViewHeight / layer.Sprite.Height);
            Vector2 offset = Vector2.One / layer.Sprite.SizeF;
            layer.Transform = Matrix.CreateTranslation(translateBody) * Matrix.CreateTranslation(translateCenter);

            offset *= new Vector2(-layer.Translation.X, -layer.Translation.Y);
            offset.X += CurrentStyle.LoopLayerOffset.X / layer.Sprite.Height;
            offset.Y -= CurrentStyle.LoopLayerOffset.Y / layer.Sprite.Width;

            LeftRightLoopEffect.Parameters["MappingTexture"].SetValue(layer.Sprite.Source);
            LeftRightLoopEffect.Parameters["DrawCount"].SetValue(drawCount);
            LeftRightLoopEffect.Parameters["Offset"].SetValue(offset);
            EngineInfo.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, LeftRightLoopEffect, null);
            EngineInfo.SpriteBatch.Draw(_screenMap, new Rectangle(0, 0, EngineInfo.ViewWidth, EngineInfo.ViewHeight), Color.White);
            EngineInfo.SpriteBatch.End();
        }
    }
}