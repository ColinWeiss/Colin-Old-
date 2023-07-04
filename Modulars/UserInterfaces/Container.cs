using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Colin.Modulars.UserInterfaces
{
    /// <summary>
    /// 指代用户交互界面中的容器.
    /// </summary>
    public class Container : Division
    {
        public Container(string name) : base(name) { }

        public override void OnInit()
        {
            Interact.IsInteractive = false;
            Interact.IsSelectable = false;
            //     Layout.Width = EngineInfo.ViewWidth;
            //     Layout.Height = EngineInfo.ViewHeight;
            // EngineInfo.Engine.Window.ClientSizeChanged += Window_ClientSizeChanged;
            ContainerInitialize();
            base.OnInit();
        }

        /// <summary>
        /// 在此处进行容器初始化操作.
        /// </summary>
        public virtual void ContainerInitialize()
        {

        }
    }
}