using System.Runtime.Serialization;

namespace Colin.Common.UserInterfaces
{
    /// <summary>
    /// 容器设计信息.
    /// </summary>
    [Serializable]
    [DataContract( Name = "DesignInfo" )]
    public struct DesignInfo
    {
        [DataMember]
        public Color CurrentColor;

        [DataMember]
        public Color TargetColor;

        /// <summary>
        /// 容器的缩放起点.
        /// </summary>
        [DataMember]
        public Point Origin;

        /// <summary>
        /// 容器的缩放起点.
        /// </summary>
        public Vector2 OriginF => new Vector2( Origin.X, Origin.Y );

        /// <summary>
        /// 容器的缩放.
        /// </summary>
        [DataMember]
        public Vector2 CurrentScale;

        /// <summary>
        /// 容器的目标缩放值.
        /// </summary>
        [DataMember]
        public Vector2 TargetScale;

        /// <summary>
        /// 指示颜色转换当前时间.
        /// </summary>
        [DataMember]
        public float ColorConversionTimer;

        /// <summary>
        /// 指示颜色转换所需时间.
        /// </summary>
        [DataMember]
        public float ColorConversionTime;

        /// <summary>
        /// 指示缩放调整当前时间.
        /// </summary>
        [DataMember]
        public float ScaleConversionTimer;

        /// <summary>
        /// 指示缩放调整所需时间.
        /// </summary>
        [DataMember]
        public float ScaleConversionTime;

        public void SetColor( byte r, byte g, byte b, byte a = 255 )
        {
            SetColor( new Color( r, g, b, a ) );
        }

        public void SetColor( Color color )
        {
            CurrentColor = color;
            TargetColor = color;
        }

        public void SetScale( Vector2 scale )
        {
            CurrentScale = scale;
            TargetScale = scale;
        }

        public void SetTargetColor( Color color )
        {
            ColorConversionTimer = 0;
            TargetColor = color;
        }

        public void SetTargetColor( byte r, byte g, byte b, byte a = 255 )
        {
            ColorConversionTimer = 0;
            TargetColor = new Color( r, g, b, a );
        }

        public void SetTargetScale( Vector2 scale )
        {
            ScaleConversionTimer = 0;
            TargetScale = scale;
        }

    }
}