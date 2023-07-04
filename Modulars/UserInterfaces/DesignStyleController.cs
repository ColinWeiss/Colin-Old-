using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colin.Modulars.UserInterfaces
{
    /// <summary>
    /// 划分元素设计样式控制器.
    /// </summary>
    public abstract class DesignStyleController
    {
        internal Division _division;
        public Division Division => _division;
        public abstract void OnUpdate( ref DesignStyle designStyle );
    }
}