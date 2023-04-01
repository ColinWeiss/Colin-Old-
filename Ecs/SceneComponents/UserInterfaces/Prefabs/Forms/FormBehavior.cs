using Colin.Common.Audios;
using Colin.Resources;

namespace Colin.Common.UserInterfaces.Prefabs.Forms
{
    /// <summary>
    /// 窗体行为.
    /// </summary>
    public class FormBehavior
    {
        public Form GameForm { get; private set; }

        public FormBehavior( Form form ) => GameForm = form;

        /// <summary>
        /// 获取窗体是否处于正在关闭状态的值.
        /// </summary>
        public bool CloseState;

        /// <summary>
        /// 关闭状态计时器.
        /// <br>该计时器在窗体每次打开时都会重置为 <see cref="CloseTime"/> 的值.</br>
        /// </summary>
        public int CloseTimer = 48;

        /// <summary>
        /// 指示窗体处于关闭状态多久时完全关闭 ( <see cref="Container.Enable"/> = <see langword="false"/> ).
        /// </summary>
        public int CloseTime = 48;

        /// <summary>
        /// 执行初始化内容.
        /// </summary>
        /// <param name="form">窗体.</param>
        public virtual void SetDefault( )
        {
            GameForm.DesignInfo.Origin = new Point( GameForm.LayoutInfo.Size.X / 2, GameForm.LayoutInfo.Size.Y / 2 );
            GameForm.DesignInfo.ColorConversionTime = CloseTime;
            GameForm.DesignInfo.ScaleConversionTime = CloseTime;
        }

        /// <summary>
        /// 允许你自由定制关于窗体样式的逻辑计算.
        /// </summary>
        public virtual void UpdateFormStyle( ) { }

        /// <summary>
        /// 当窗体处于正在关闭状态时执行.
        /// </summary>
        public virtual void UpdateCloseState( )
        {
            if( CloseTimer > 0 )
                CloseTimer--;
            else
            {
                GameForm.Enable = false;
                GameForm.Visiable = false;
                CloseState = false;
                CloseTimer = 0;
            }
        }

        /// <summary>
        /// 当窗体打开时执行.
        /// </summary>
        public virtual void OnOpen( )
        {
            GameForm.DesignInfo.SetTargetColor( Color.White );
            GameForm.DesignInfo.SetTargetScale( Vector2.One );
        }

        /// <summary>
        /// 当窗体关闭时执行.
        /// </summary>
        public virtual void OnClose( )
        {
            Sound.Play( SoundResource.Get( "UI/FormOpen" ) );

            //   Sound.Play( SoundResource.GetAsset( "UI/Click_0" ) );
            GameForm.DesignInfo.SetTargetColor( Color.Transparent );
            GameForm.DesignInfo.SetTargetScale( Vector2.One * 0.78f );
        }
    }
}