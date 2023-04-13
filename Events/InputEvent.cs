using Colin.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colin.Events
{
    public class InputEvent : BasicEvent
    {
        public KeyboardResponder Keyboard { get; set; }

        public MouseResponder Mouse { get; set; }

    }
}