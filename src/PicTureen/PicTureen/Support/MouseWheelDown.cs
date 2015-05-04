using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PicTureen.Support
{

    public class MouseWheelDown : MouseGesture
    {
        public MouseWheelDown()
            : base(MouseAction.WheelClick)
        {
        }

        public MouseWheelDown(ModifierKeys modifiers)
            : base(MouseAction.WheelClick, modifiers)
        {
        }

        public override bool Matches(object targetElement, InputEventArgs inputEventArgs)
        {
            if (!base.Matches(targetElement, inputEventArgs)) return false;
            if (!(inputEventArgs is MouseWheelEventArgs)) return false;
            var args = (MouseWheelEventArgs) inputEventArgs;
            return args.Delta < 0;
        }
    }

}
