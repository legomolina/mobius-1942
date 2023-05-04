using Engine.Core.Input;
using Engine.Core.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1942.UI
{
    public class MouseEventArgs : EventArgs
    {
        public MouseButtons MouseButtons { get; private set; }
        public Point MousePosition { get; private set; }

        public MouseEventArgs(Point mousePosition, MouseButtons buttons) : base()
        {
            MouseButtons = buttons;
            MousePosition = mousePosition;
        }
    }
}
