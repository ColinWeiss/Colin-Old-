﻿using Colin.Modulars.UserInterfaces;
using Colin.Modulars.UserInterfaces.Controllers;
using Colin.Modulars.UserInterfaces.Prefabs;
using Colin.Modulars.UserInterfaces.Renderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colin.Modulars.UserInterfaces.Forms
{
    public class Popup : Canvas
    {
        private int _titleHeight;

        public Popup( string name, int width, int height, int titleHeight ) : base( name )
        {
            Layout.Width = width;
            Layout.Height = height;
            _titleHeight = titleHeight;
        }

        public Division Substrate;

        public Division TitleColumn;

        public Division Icon;

        public Division CloseButton;

        public Division Block;

        public override void OnInit( )
        {
            Design.Scale = Vector2.One;
            Design.Color = Color.White;

            Controller = new DivGradientController( this );
            Interact.IsDraggable = true;
            Interact.IsSelectable = false;
            Interact.IsInteractive = true;

            Layout.PaddingLeft = 4;
            Layout.PaddingTop = 4;

            Substrate = new Division( "Substrate" );
            Substrate.Interact.IsInteractive = false;
            Substrate.Layout.Left = -4;
            Substrate.Layout.Top = -4;
            Substrate.Layout.Width = Layout.Width + 8;
            Substrate.Layout.Height = Layout.Height + _titleHeight + 8;
            NinecutRenderer _substrateRenderer = Substrate.BindRenderer<NinecutRenderer>( );
            _substrateRenderer.Bind( TextureResource.Get( "UserInterfaces/Forms/Substrate1" ) );
            _substrateRenderer.Cut = 6;
            base.Register( Substrate );

            Block = new Division( "Block" );
            Block.BindRenderer<PixelRenderer>( );
            Block.Design.Color = new Color( 17, 18, 20 );
            Block.Layout.Top = _titleHeight;
            Block.Layout.Width = Layout.Width;
            Block.Layout.Height = Layout.Height;
            base.Register( Block );

            TitleColumn = new Division( "TitleColumn" );
            NinecutRenderer _tileColumnRenderer = TitleColumn.BindRenderer<NinecutRenderer>( );
            _tileColumnRenderer.Bind( TextureResource.Get( "UserInterfaces/Forms/TitleColumn1" ) );
            _tileColumnRenderer.Cut = 6;
            TitleColumn.Interact.IsInteractive = false;
            TitleColumn.Layout.Width = Layout.Width;
            TitleColumn.Layout.Height = _titleHeight;
            {
                Icon = new Division( "Icon" );
                SpriteRenderer _iconRenderer = Icon.BindRenderer<SpriteRenderer>( );
                _iconRenderer.Bind( TextureResource.Get( "UserInterfaces/Forms/Icon1" ) );
                Icon.Interact.IsInteractive = false;
                Icon.Layout.Left = 4;
                Icon.Layout.Top = 4;
                Icon.Layout.Width = 8;
                Icon.Layout.Height = 8;
                TitleColumn.Register( Icon );

                CloseButton = new Division( "CloseButton" );
                SpriteRenderer _closeRenderer = CloseButton.BindRenderer<SpriteRenderer>( );
                _closeRenderer.Bind( TextureResource.Get( "UserInterfaces/Forms/Close1" ) );
                CloseButton.Interact.IsInteractive = true;
                CloseButton.Layout.Left = TitleColumn.Layout.Width - 16;
                CloseButton.Layout.Top = 2;
                CloseButton.Layout.Width = 24;
                CloseButton.Layout.Height = 24;
                CloseButton.Events.MouseLeftClickBefore += ( s, e ) =>
                {
                    _closeRenderer.Bind( TextureResource.Get( "UserInterfaces/Forms/Close1_Off" ) );
                };
                CloseButton.Events.MouseLeftClickAfter += ( s, e ) =>
                {
                    _closeRenderer.Bind( TextureResource.Get( "UserInterfaces/Forms/Close1" ) );
                    Close( );
                };
                CloseButton.Events.ActivationEnd += ( s, e ) =>
                {
                    _closeRenderer.Bind( TextureResource.Get( "UserInterfaces/Forms/Close1" ) );
                };
                TitleColumn.Register( CloseButton );
            }
            base.Register( TitleColumn );

            PopupInit( );

            Layout.Width += 8;
            Layout.Height += _titleHeight + 8;

            base.OnInit( );
        }
        public virtual void PopupInit( ) { }
        public override bool Register( Division division, bool doInit = false ) => Block.Register( division, doInit );
        public void Show( ) => (Controller as DivGradientController).Open( );
        public void Close( ) => (Controller as DivGradientController).Close( );
    }
}