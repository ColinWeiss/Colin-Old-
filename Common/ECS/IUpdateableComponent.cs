using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colin.Common.ECS
{
    /// <summary>
    /// 表示一个可执行逻辑计算的组件.
    /// </summary>
    public interface IUpdateableComponent
    {
        bool Enable { get; }
        int UpdateOrder { get; }
        public virtual void DoUpdate( GameTime gameTime ) { }
    }
}