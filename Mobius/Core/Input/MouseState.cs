using Engine.Core.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Input
{
    public struct MouseState
    {
        public Point Position { get; set; }
        public bool LeftButton { get; set; }
        public bool RightButton { get; set; }
        public bool MiddleButton { get; set; }
        public bool Mouse4Button { get; set; }
        public bool Mouse5Button { get; set; }

        public bool GetButtonByEnum(MouseButtons button)
        {
            return button switch
            {
                MouseButtons.Left => LeftButton,
                MouseButtons.Right => RightButton,
                MouseButtons.Middle => MiddleButton,
                MouseButtons.Button_4 => Mouse4Button,
                MouseButtons.Button_5 => Mouse5Button,
                _ => false,
            };
        }
    }
}
