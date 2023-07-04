using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colin.Modulars.UserInterfaces
{
    public abstract class LayoutStyleController
    {
        internal Division _division;
        public Division Division => _division;
        public abstract void OnUpdate( ref LayoutStyle layout );
    }
}
