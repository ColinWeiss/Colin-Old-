using Colin.Common.Backgrounds;
using Colin.Developments;
using Colin.Resources;

namespace Colin.Common.Backgrounds.Styles
{
    /// <summary>
    /// 背景样式: 森林.
    /// </summary>
    public class BackgroundStyle_Forest : BackgroundStyle
    {

        public override void SetDefault( )
        {
            FixLayerOverallOffset = -Vector2.UnitY * EngineInfo.ViewHeight / 3f;
            FixLayerScale = new Vector2( EngineInfo.ViewWidth / 1600f );
            LoopLayerOffset = new Vector2( 0, -0.5f );
            for( int count = 0; count < 16; count++ )
            {
                Layers.Add( new BackgroundLayer( new Graphics.Sprite( TextureResource.Get( "Regions/Positiveworld/Forests/Layer_" + count ) ) ) );
            }
            Layers[0].IsFix = true;
            Layers[1].IsFix = true;
            Layers[2].IsFix = true;
            Layers[3].IsFix = true;
            Layers[4].IsFix = true;
            for( int count = 5; count < 16; count++ )
            {
                Layers[count].Parallax = new Vector2( 0.25f + 0.045f * (count - 6), 0.4f + 0.02f * (count - 6) );
                Layers[count].IsLoop = true;
                Layers[count].LoopStyle = BackgroundLoopStyle.LeftRightConnect;
            }
            base.SetDefault( );
        }

        public override void UpdateStyle( )
        {
            FixLayerOverallOffset = -Vector2.UnitY * EngineInfo.ViewHeight / 2.85f;
            FixLayerScale = new Vector2( EngineInfo.ViewWidth / 1600f );

            base.UpdateStyle( );
        }

    }
}