using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colin.Common.SceneComponents.UserInterfaces.Prefabs
{
    public class InputBox : ScRecContainer
    {
        public Label Label;
        public override void ContainerInitialize( )
        {
            Label = new Label( );
            Label.Text = "啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊";
            Register( Label );
            base.ContainerInitialize( );
        }
    }
}