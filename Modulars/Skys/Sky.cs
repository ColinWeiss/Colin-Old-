﻿using Colin.Common;
using Colin.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colin.Modulars.Skys
{
    public class Sky : ISceneComponent, IRenderableSceneComponent
    {
        public Scene Scene { get; set; }

        public bool Enable { get; set; }

        public RenderTarget2D SceneRt { get; set; }

        public bool Visible { get; set; }

        public SpriteSortMode SpriteSortMode { get; }

        public Matrix? TransformMatrix { get; }

        public SkyStyle CurrentSkyStyle { get; private set; }

        public SkyStyle NextStyle { get; private set; }

        public void DoInitialize()
        {

        }
        public void DoUpdate(GameTime time)
        {
            CurrentSkyStyle?.DoUpdate(time);
        }

        public void DoRender(SpriteBatch batch )
        {
            CurrentSkyStyle?.DoRender();
        }

        public void ChangeSkyStyle(SkyStyle skyStyle)
        {
            NextStyle = skyStyle;
        }
    }
}
