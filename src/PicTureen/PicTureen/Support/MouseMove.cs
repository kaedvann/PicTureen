using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PicTureen.Support
{
    class MouseMove : MouseGesture
    {
        public MouseMove() : base(MouseAction.LeftClick)
        {
            
        }

        public MouseMove(ModifierKeys modifier)
            : base(MouseAction.LeftClick)
        {

        }
    }
}
