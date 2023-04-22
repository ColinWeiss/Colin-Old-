using Colin.Audios;
using Colin.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colin.Common.SceneComponents.UserInterfaces
{
    public class ContainerBehavior
    {
        public Container Container { get; private set; }

        public ContainerBehavior( Container container ) => Container = container;

        /// <summary>
        /// 获取容器是否处于正在关闭状态的值.
        /// </summary>
        public bool CloseState;

        /// <summary>
        /// 关闭状态计时器.
        /// <br>该计时器在容器每次启用时都会重置为 <see cref="CloseTime"/> 的值.</br>
        /// </summary>
        public int CloseTimer = 48;

        /// <summary>
        /// 指示容器处于关闭状态多久时完全关闭 ( <see cref="Container.Enable"/> = <see langword="false"/> ).
        /// </summary>
        public int CloseTime = 48;

        /// <summary>
        /// 执行初始化内容.
        /// </summary>
        /// <param name="form">窗体.</param>
        public virtual void SetDefault( )
        {
            Container.DesignInfo.Origin = new Point( Container.LayoutInfo.Size.X / 2, Container.LayoutInfo.Size.Y / 2 );
            Container.DesignInfo.ColorConversionTime = CloseTime;
            Container.DesignInfo.ScaleConversionTime = CloseTime;
        }

        /// <summary>
        /// 允许你自由定制关于容器样式的逻辑计算.
        /// </summary>
        public virtual void UpdateStyle( ) { }

        /// <summary>
        /// 当容器处于正在关闭状态时执行.
        /// </summary>
        public virtual void UpdateCloseState( ) { }

        /// <summary>
        /// 当容器打开时执行.
        /// </summary>
        public virtual void OnActive( ) { }

        /// <summary>
        /// 当容器关闭时执行.
        /// </summary>
        public virtual void OnDisactive( ) { }

    }
}
