using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colin.Modulars.UserInterfaces
{
    public class UserInterface
    {
        public static Division Focus;

        public Container Container { get; private set; } = new Container("NomalContainer");

        public void DoUpdate(GameTime time)
        {
            Container?.DoUpdate(time);
        }

        public void DoRender(SpriteBatch spriteBatch) => Container?.DoRender(spriteBatch);

        public void Register(Container container) => Container?.Register(container);

        public void Remove(Container container, bool dispose) => Container?.Remove(container);

    }
}