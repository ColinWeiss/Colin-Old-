using Colin.Audios;
using Colin.Resources;

namespace Colin.Common.SceneComponents.UserInterfaces.Prefabs.Forms
{
    /// <summary>
    /// 窗体行为.
    /// </summary>
    public class FormBehavior : ContainerBehavior
    {
        public Form GameForm { get; private set; }

        public FormBehavior( Form form ) : base( form ) { GameForm = form; }

        public override void UpdateCloseState( )
        {
            if( CloseTimer > 0 )
                CloseTimer--;
            else
            {
                GameForm.Disactive( true );
                CloseState = false;
                CloseTimer = 0;
            }
            base.UpdateCloseState( );
        }

        public override void OnActive( )
        {
            CloseState = false;
            CloseTimer = CloseTime;
            GameForm.DesignInfo.SetTargetColor( Color.White );
            GameForm.DesignInfo.SetTargetScale( Vector2.One );
            base.OnActive( );
        }

        public override void OnDisactive( )
        {
            Sound.Play( SoundResource.Get( "UI/FormOpen" ) );
            GameForm.DesignInfo.SetTargetColor( Color.Transparent );
            GameForm.DesignInfo.SetTargetScale( Vector2.One * 0.78f );
            CloseState = true;
            base.OnDisactive( );
        }

    }
}