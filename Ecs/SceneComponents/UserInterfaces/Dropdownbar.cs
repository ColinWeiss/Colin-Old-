﻿using Colin.Common.UserInterfaces.Prefabs;

namespace Colin.Common.UserInterfaces
{
    /// <summary>
    /// 下拉栏.
    /// </summary>
    public class Dropdownbar : Container
    {
        public Container Button = new Container( );

        public Container Panel = new Container( );

        public LinerMenu LinerMenu = new LinerMenu( );

        public bool Dropdown = false;

        public override void ContainerInitialize( )
        {
            Panel.LayoutInfo.SetWidth( LayoutInfo.Width );
            Panel.DesignInfo.SetColor( Color.Gray );
            Register( Panel );

            LinerMenu.LayoutInfo.SetTop( LayoutInfo.Height );
            LinerMenu.Enable = false;
            LinerMenu.Visiable = false;
            Register( LinerMenu );

            Button.LayoutInfo.SetSize( LayoutInfo.Size );
            Button.EventResponder.MouseLeftClickAfter += ( s, e ) =>
            {
                Dropdown = !Dropdown;
            };
            Register( Button );

            base.ContainerInitialize( );
        }
        public override void LayoutInfoUpdate( ref LayoutInfo info )
        {
            if( Dropdown )
            {
                LinerMenu.Enable = true;
                LinerMenu.Visiable = true;
                Panel.Enable = true;
                Panel.Visiable = true;
                LinerMenu.RefreshSize( );
                Panel.LayoutInfo.SetHeight( LinerMenu.LayoutInfo.Height + LayoutInfo.Height + (LinerMenu.Border ? LinerMenu.Interval : 0) );
            }
            else
            {
                LinerMenu.Enable = false;
                LinerMenu.Visiable = false;
                Panel.Enable = false;
                Panel.Visiable = false;
                LinerMenu.LayoutInfo.SetSize( 0 );
                Panel.LayoutInfo.SetHeight( 0 );
            }

            base.LayoutInfoUpdate( ref info );
        }
        public override void SelfUpdate( )
        {
            base.SelfUpdate( );
        }
    }
}