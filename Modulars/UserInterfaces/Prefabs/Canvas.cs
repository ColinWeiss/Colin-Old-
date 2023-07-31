﻿using Colin.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colin.Modulars.UserInterfaces.Prefabs
{
    public class Canvas : Division
    {
        public override sealed bool IsCanvas => true;
        public Canvas( string name ) : base( name ) { }
        public override void OnInit( )
        {
            SetCanvas( Layout.Width , Layout.Height );
            base.OnInit( );
        }
        public void SetCanvas( int width , int height )
        {
            Layout.Width = width;
            Layout.Height = height;
            Design.Anchor = Layout.SizeF / 2;
            Canvas?.Dispose( );
            Canvas = RenderTargetExt.CreateDefault( width, height );
        }
        public override void OnUpdate( GameTime time )
        {
            Layout.IsCanvas = IsCanvas;
            base.OnUpdate( time );
        }
    }
}