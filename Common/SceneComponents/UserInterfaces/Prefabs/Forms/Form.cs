using Colin.Common.SceneComponents.UserInterfaces.Renderers;
using Colin.Graphics;
using Colin.Inputs;
using Colin.Resources;
using Microsoft.Xna.Framework.Input;

namespace Colin.Common.SceneComponents.UserInterfaces.Prefabs.Forms
{
    /// <summary>
    /// 通用窗口.
    /// </summary>
    public class Form : Canvas
    {
        /// <summary>
        /// 事件: 发生于窗口开启状态时.
        /// </summary>
        public EventHandler<EventArgs> OnOpenState = ( s, e ) => { };

        /// <summary>
        /// 事件: 发生于窗口关闭状态时.
        /// </summary>
        public EventHandler<EventArgs> OnCloseStateEnable = ( s, e ) => { };

        /// <summary>
        /// 窗体基底.
        /// </summary>
        public Container Substrate;

        /// <summary>
        /// 窗体.
        /// </summary>
        public Canvas Block;

        /// <summary>
        /// 窗体边框.
        /// </summary>
        public Container BlockBorder;

        /// <summary>
        /// 标题栏.
        /// </summary>
        public Container TitleBlock;

        /// <summary>
        /// 窗体图标.
        /// </summary>
        public Container Icon;

        /// <summary>
        /// 窗体标题文本.
        /// </summary>
        public Label TitleLabel;

        /// <summary>
        /// 关闭按钮.
        /// </summary>
        public Container CloseButton;

        /// <summary>
        /// 关闭按钮图标.
        /// </summary>
        public Container CloseButtonIcon;

        private int _titleBlockHeight = 24;
        /// <summary>
        /// 标题栏高度.
        /// <br>[!] 最小值为 24 像素.</br>
        /// </summary>
        public int TitleBlockHeight
        {
            get
            {
                return _titleBlockHeight;
            }
            set
            {
                if( value < 24 )
                    _titleBlockHeight = 24;
                else
                    _titleBlockHeight = value;
            }
        }

        public string Title = "Form";

        public override sealed void ContainerInitialize( )
        {
            if( LayoutInfo.Width <= 0 )
                LayoutInfo.SetWidth( 520 );
            if( LayoutInfo.Height <= 0 )
                LayoutInfo.SetHeight( 320 );

            DesignInfo.SetScale( Vector2.One * 0.7f );
            DesignInfo.SetColor( Color.Transparent );
            InteractiveInfo.CanDrag = true;

            if( Behavior == null )
                Behavior = new FormBehavior( this );

            Renderer = new NineCutRenderer( TextureResource.Get( "UI/Forms/Default/Substrate" ), 6 );

            Substrate = new Container( );
            Substrate.Renderer = new NineCutRenderer( TextureResource.Get( "UI/Forms/Default/Substrate" ), 6 );
            Substrate.LayoutInfo.SetLocation( 0, 0 );
            Substrate.LayoutInfo.SetSize( LayoutInfo.Width + 12, LayoutInfo.Height + TitleBlockHeight + 16 );
            Substrate.InteractiveInfo.CanSeek = false;
            base.Register( Substrate );

            TitleBlock = new Container( );
            TitleBlock.InteractiveInfo.CanSeek = false;
            TitleBlock.LayoutInfo.SetLocation( 6, 6 );
            TitleBlock.LayoutInfo.SetSize( LayoutInfo.Width, TitleBlockHeight );
            base.Register( TitleBlock );

            Icon = new Container( );
            Icon.Renderer = new PictureRenderer( TextureResource.Get( "UI/Forms/Default/Icon" ) );
            Icon.LayoutInfo.SetLocation( 0, 0 );
            Icon.LayoutInfo.SetSize( 24, 24 );
            TitleBlock.Register( Icon );

            TitleLabel = new Label( );
            TitleBlock.Register( TitleLabel );

            CloseButton = new Container( );
            CloseButton.Renderer = new PictureRenderer( TextureResource.Get( "UI/Forms/Default/Close" ) );
            CloseButton.LayoutInfo.SetSize( 40, 24 );
            CloseButton.LayoutInfo.SetLocation( TitleBlock.LayoutInfo.Width - CloseButton.LayoutInfo.Width, 0 );
            CloseButton.EventResponder.MouseLeftDown += ( s, e ) =>
            {
                (CloseButton.Renderer as PictureRenderer).Picture = new Sprite( TextureResource.Get( "UI/Forms/Default/Close_Using" ) );
            };
            CloseButton.EventResponder.ActivationEnd += ( s, e ) =>
            {
                (CloseButton.Renderer as PictureRenderer).Picture = new Sprite( TextureResource.Get( "UI/Forms/Default/Close" ) );
            };
            CloseButton.EventResponder.MouseLeftClickAfter += ( s, e ) =>
            {
                (CloseButton.Renderer as PictureRenderer).Picture = new Sprite( TextureResource.Get( "UI/Forms/Default/Close" ) );
                Close( );
            };
            TitleBlock.Register( CloseButton );

            Block = new Canvas( );
            Block.LayoutInfo.SetLocation( 6, TitleBlockHeight + 10 );
            Block.LayoutInfo.SetSize( LayoutInfo.Width, LayoutInfo.Height );
            Block.InteractiveInfo.CanSeek = false;

            Container blockSubstrate = new Container( );
            blockSubstrate.Renderer = new NineCutRenderer( TextureResource.Get( "UI/Forms/Default/Block" ), 6 );
            blockSubstrate.LayoutInfo.SetSize( LayoutInfo.Width, LayoutInfo.Height );
            blockSubstrate.DesignInfo.SetColor( 38, 37, 33 );
            Block.Register( blockSubstrate );
            base.Register( Block );

            BlockBorder = new Container( );
            BlockBorder.Renderer = new NineCutRenderer( TextureResource.Get( "UI/Forms/Default/BlockBorder" ), 6 );
            BlockBorder.LayoutInfo.SetLocation( 6, TitleBlockHeight + 10 );
            BlockBorder.LayoutInfo.SetSize( LayoutInfo.Width, LayoutInfo.Height );
            BlockBorder.InteractiveInfo.CanSeek = false;
            base.Register( BlockBorder );

            LayoutInfo.SetSize( Substrate.LayoutInfo.Width, Substrate.LayoutInfo.Height );

            FormInitialize( );

            base.ContainerInitialize( );

        }

        public virtual void FormInitialize( ) { }

        public override sealed void SelfUpdate( )
        {
            TitleLabel.Text = Title;
            TitleLabel.DesignInfo.SetColor( 22, 22, 19 );
            TitleLabel.LayoutInfo.SetLocation( Icon.LayoutInfo.Width + 6, TitleBlockHeight / 2 - TitleLabel.LayoutInfo.Size.Y / 2 - 2 );
            Behavior.UpdateFormStyle( );
            if( Behavior.CloseState )
                Behavior.UpdateCloseState( );
            FormUpdate( );
            base.SelfUpdate( );
        }
        public virtual void FormUpdate( ) { }

        public override void InteractiveInfoUpdate( ref InteractiveInfo info )
        {
            if( UserInterface.State.SeekInteractive( ) == this &&MouseResponder.Instance.MouseLeftClickBeforeFlag )
                UserInterface.State.SetTop( this );
            base.InteractiveInfoUpdate( ref info );
        }

        public override void Register( Container container ) => Block.Register( container );

        /// <summary>
        /// 设置窗体位置.
        /// </summary>
        /// <param name="location">位置.</param>
        public void SetLocation( Point location ) => LayoutInfo.SetLocation( location );

        /// <summary>
        /// 设置窗体位置.
        /// </summary>
        /// <param name="x">位置横坐标.</param>
        /// <param name="y">位置纵坐标.</param>
        public void SetLocation( int x, int y ) => LayoutInfo.SetLocation( x, y );

        /// <summary>
        /// 设置窗体大小.
        /// </summary>
        /// <param name="size">窗体大小.</param>
        public void SetSize( Point size ) => LayoutInfo.SetSize( size );

        /// <summary>
        /// 设置窗体大小.
        /// </summary>
        /// <param name="width">窗体宽度.</param>
        /// <param name="height">窗体高度.</param>
        public void SetSize( int width, int height ) => LayoutInfo.SetSize( width, height );

        public void Open( )
        {
            Active( true );
        }

        public void Close( )
        {
            Behavior.CloseState = true;
            Behavior.OnDisactive( );
        }

    }
}