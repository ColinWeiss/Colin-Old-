using System.Runtime.Serialization;

namespace Colin.Common.SceneComponents.UserInterfaces
{
    /// <summary>
    /// 容器的交互信息.
    /// </summary>
    [Serializable]
    [DataContract( Name = "InteractiveInfo" )]
    public struct InteractiveInfo
    {
        /// <summary>
        /// 指示容器当前是否可进行交互.
        /// </summary>
        [DataMember]
        public bool Activation;

        /// <summary>
        /// 指示容器当前是否具有焦点.
        /// </summary>
        [DataMember]
        public bool Focus;

        /// <summary>
        /// 指示容器上一帧的交互状态的值.
        /// </summary>
        [DataMember]
        public bool ActivationLast;

        /// <summary>
        /// 指示该容器是否可被父元素寻找到.
        /// </summary>
        [DataMember]
        public bool CanSeek;

        /// <summary>
        /// 指示该容器是否允许拖拽.
        /// </summary>
        [DataMember]
        public bool CanDrag;

    }
}
