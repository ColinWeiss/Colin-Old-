using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Colin.Developments
{
    /// <summary>
    /// 表示一个可溯源的对象.
    /// </summary>
    public interface ITraceable
    {
        /// <summary>
        /// 溯源.
        /// </summary>
        /// <returns>源.</returns>
        public IName GetSource( );
    }
}