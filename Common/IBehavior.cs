using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colin.Common
{
    public interface IBehavior
    {
        public void DoInitialize();
        public void DoUpdate(GameTime time);
        public void DoRender();
    }
}