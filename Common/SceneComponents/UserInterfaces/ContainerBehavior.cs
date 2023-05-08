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
        /// 执行初始化内容.
        /// </summary>
        /// <param name="form">窗体.</param>
        public virtual void SetDefault( )
        {
            Container.DesignInfo.Origin = new Point( Container.LayoutInfo.Size.X / 2, Container.LayoutInfo.Size.Y / 2 );
        }

        /// <summary>
        /// 当容器处于正在关闭状态时执行.
        /// </summary>
        public virtual void UpdateCloseState( ) { }

        /// <summary>
        /// 当容器开始打开过程时执行.
        /// </summary>
        public virtual void Opening( ) { }

        /// <summary>
        /// 当容器开始关闭过程时执行.
        /// </summary>
        public virtual void Closing( ) { }

    }
}
