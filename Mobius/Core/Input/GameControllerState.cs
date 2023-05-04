using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Input
{
    public struct GameControllerStick
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
    }

    public struct GameControllerState
    {
        public bool A { get; set; }
        public bool B { get; set; }
        public bool X { get; set; }
        public bool Y { get; set; }
        public bool Back { get; set; }
        public bool Guide { get; set; }
        public bool Start { get; set; }
        public bool DPadUp { get; set; }
        public bool DPadDown { get; set; }
        public bool DPadLeft { get; set; }
        public bool DPadRight { get; set; }
        public bool LeftShoulder { get; set; }
        public bool RightShoulder { get; set; }
        public bool LeftStickButton { get; set; }
        public bool RightStickButton { get; set; }
        public GameControllerStick LeftAxis { get; set; }
        public GameControllerStick RightAxis { get; set; }

        public bool GetButtonByEnum(GameControllerButtons button)
        {
            return button switch
            {
                GameControllerButtons.A => A,
                GameControllerButtons.B => B,
                GameControllerButtons.X => X,
                GameControllerButtons.Y => Y,
                GameControllerButtons.Back => Back,
                GameControllerButtons.Guide => Guide,
                GameControllerButtons.Start => Start,
                GameControllerButtons.LeftShoulder => LeftShoulder,
                GameControllerButtons.RightShoulder => RightShoulder,
                GameControllerButtons.DPadUp => DPadUp,
                GameControllerButtons.DPadDown => DPadDown,
                GameControllerButtons.DPadLeft => DPadLeft,
                GameControllerButtons.DPadRight => DPadRight,
                GameControllerButtons.LeftStick => LeftStickButton,
                GameControllerButtons.RightStick => RightStickButton,
                _ => false,
            };
        }

        public GameControllerStick GetAxisByEnum(GameControllerAxis axis)
        {
            return axis switch
            {
                GameControllerAxis.LeftStick => LeftAxis,
                GameControllerAxis.RightStick => RightAxis,
            };
        }
    }
}
