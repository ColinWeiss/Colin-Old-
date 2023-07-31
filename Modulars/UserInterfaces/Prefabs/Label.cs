﻿using Colin.Modulars.UserInterfaces.Renderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colin.Modulars.UserInterfaces.Prefabs
{
    public class Label : Division
    {
        public Label( string name ) : base( name ) { }
        public FontRenderer FontRenderer;
        public override void OnInit( )
        {
            FontRenderer = BindRenderer<FontRenderer>( );
            base.OnInit( );
        }
        public void SetText( string text ) => FontRenderer.Text = text;
    }
}