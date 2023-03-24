using Colin.Resources;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colin.Developments
{
    public class CursorManager
    {
        public static void SetCursor( )
        {
            Mouse.SetCursor( CursorResource.Arrow );
        }
    }
}