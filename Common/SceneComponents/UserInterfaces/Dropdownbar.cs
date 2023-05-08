using Colin.Common.SceneComponents.UserInterfaces.Prefabs;

namespace Colin.Common.SceneComponents.UserInterfaces
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
            LinerMenu.Visible = false;
            Register( LinerMenu );

            Button.LayoutInfo.SetSize( LayoutInfo.Size );
            Button.EventResponder.MouseLeftClickAfter += EventResponder_MouseLeftClickAfter;
            Register( Button );

            base.ContainerInitialize( );
        }

        private void EventResponder_MouseLeftClickAfter( object sender, Events.ContainerEvent e )
        {
            Dropdown = !Dropdown;
        }

        public override void LayoutInfoUpdate( ref LayoutInfo info )
        {
            if( Dropdown )
            {
                LinerMenu.Enable = true;
                LinerMenu.Visible = true;
                Panel.Enable = true;
                Panel.Visible = true;
                LinerMenu.RefreshSize( );
                Panel.LayoutInfo.SetHeight( LinerMenu.LayoutInfo.Height + LayoutInfo.Height + (LinerMenu.Border ? LinerMenu.Interval : 0) );
            }
            else
            {
                LinerMenu.Enable = false;
                LinerMenu.Visible = false;
                Panel.Enable = false;
                Panel.Visible = false;
                LinerMenu.LayoutInfo.SetSize( 0 );
                Panel.LayoutInfo.SetHeight( 0 );
            }

            base.LayoutInfoUpdate( ref info );
        }

        protected override void OnDispose( )
        {
            Button.EventResponder.MouseLeftClickAfter -= EventResponder_MouseLeftClickAfter;
            base.OnDispose( );
        }
    }
}