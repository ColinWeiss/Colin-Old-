using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colin.Modulars.UserInterfaces.Prefabs
{
    public class Canvas : Division
    {
        public override sealed bool IsCanvas => true;
        public Canvas( string name ) : base( name ) { }
    }
}
