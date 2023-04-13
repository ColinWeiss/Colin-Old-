using Colin.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colin
{
    /// <summary>
    /// 表示一个可获取设备输入的对象.
    /// </summary>
    public interface IGetDeviceInputable
    {
        public InputEvent GetDeviceInput( );
    }
}
