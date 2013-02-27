using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace IS_XNA_Shooter
{
    public class ControlMng
    {
        private static bool controllerActive;

        public static Keys controlUp = Keys.W;
        public static Keys controlDown = Keys.S;
        public static Keys controlLeft = Keys.A;
        public static Keys controlRight = Keys.D;

        public ControlMng ()
        {
            controllerActive = GamePad.GetState(PlayerIndex.One).IsConnected;
        }

        public static bool isControllerActive ()
        {
            return controllerActive;
        }

    } // static class ControlMng
}
