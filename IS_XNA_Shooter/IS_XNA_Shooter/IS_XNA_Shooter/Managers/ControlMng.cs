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

        private KeyboardState prevKeyboardState, actKeyboardState;

        public static bool fPreshed, kPreshed, lPreshed, gPreshed, enterPreshed;
        public static bool f1Preshed, f2Preshed, f3Preshed, f4Preshed, f5Preshed;
        public static bool f6Preshed, f7Preshed, f8Preshed, f9Preshed, f10Preshed;
        public static bool f11Preshed, f12Preshed;

        private MouseState prevMouseState, actMouseState;

        public static bool leftClickPreshed, rightClickPreshed;
        public static bool leftClickReleased, rightClickReleased;
        public static int lastClickX, lastClickY;

        public ControlMng ()
        {
            controllerActive = GamePad.GetState(PlayerIndex.One).IsConnected;

            fPreshed = kPreshed = gPreshed = enterPreshed = false;
            f1Preshed = f2Preshed = f3Preshed = f4Preshed = f5Preshed = false;
            f6Preshed = f7Preshed = f8Preshed = f9Preshed = f10Preshed = false;
            f11Preshed = f12Preshed = false;

            leftClickPreshed = rightClickPreshed = false;
            leftClickReleased = rightClickReleased = false;
            lastClickX = lastClickY = 0;
        }

        public void Update(float deltaTime)
        {
            controllerActive = GamePad.GetState(PlayerIndex.One).IsConnected;

            // keyboard:
            actKeyboardState = Keyboard.GetState();

            kPreshed = (actKeyboardState.IsKeyDown(Keys.K) && prevKeyboardState.IsKeyUp(Keys.K));
            fPreshed = (actKeyboardState.IsKeyDown(Keys.F) && prevKeyboardState.IsKeyUp(Keys.F));
            lPreshed = (actKeyboardState.IsKeyDown(Keys.L) && prevKeyboardState.IsKeyUp(Keys.L));
            gPreshed = (actKeyboardState.IsKeyDown(Keys.G) && prevKeyboardState.IsKeyUp(Keys.G));
            enterPreshed = (actKeyboardState.IsKeyDown(Keys.Enter) && prevKeyboardState.IsKeyUp(Keys.Enter));

            f1Preshed = (actKeyboardState.IsKeyDown(Keys.F1) && prevKeyboardState.IsKeyUp(Keys.F1));
            f2Preshed = (actKeyboardState.IsKeyDown(Keys.F2) && prevKeyboardState.IsKeyUp(Keys.F2));
            f3Preshed = (actKeyboardState.IsKeyDown(Keys.F3) && prevKeyboardState.IsKeyUp(Keys.F3));
            f4Preshed = (actKeyboardState.IsKeyDown(Keys.F4) && prevKeyboardState.IsKeyUp(Keys.F4));
            f5Preshed = (actKeyboardState.IsKeyDown(Keys.F5) && prevKeyboardState.IsKeyUp(Keys.F5));
            f6Preshed = (actKeyboardState.IsKeyDown(Keys.F6) && prevKeyboardState.IsKeyUp(Keys.F6));
            f7Preshed = (actKeyboardState.IsKeyDown(Keys.F7) && prevKeyboardState.IsKeyUp(Keys.F7));
            f8Preshed = (actKeyboardState.IsKeyDown(Keys.F8) && prevKeyboardState.IsKeyUp(Keys.F8));
            f9Preshed = (actKeyboardState.IsKeyDown(Keys.F9) && prevKeyboardState.IsKeyUp(Keys.F9));
            f10Preshed = (actKeyboardState.IsKeyDown(Keys.F10) && prevKeyboardState.IsKeyUp(Keys.F10));
            f11Preshed = (actKeyboardState.IsKeyDown(Keys.F11) && prevKeyboardState.IsKeyUp(Keys.F11));
            f12Preshed = (actKeyboardState.IsKeyDown(Keys.F12) && prevKeyboardState.IsKeyUp(Keys.F12));

            prevKeyboardState = actKeyboardState;

            // mouse:
            actMouseState = Mouse.GetState();

            leftClickPreshed = ((actMouseState.LeftButton == ButtonState.Pressed) && (prevMouseState.LeftButton == ButtonState.Released));
            if (leftClickPreshed)
            {
                lastClickX = actMouseState.X;
                lastClickY = actMouseState.Y;
            }
            rightClickPreshed = ((actMouseState.RightButton == ButtonState.Pressed) && (prevMouseState.RightButton == ButtonState.Released));

            leftClickReleased = ((actMouseState.LeftButton == ButtonState.Released) && (prevMouseState.LeftButton == ButtonState.Pressed));
            rightClickReleased = ((actMouseState.RightButton == ButtonState.Released) && (prevMouseState.RightButton == ButtonState.Pressed));

            prevMouseState = actMouseState;
        }

        public static bool isControllerActive ()
        {
            return controllerActive;
        }

    } // static class ControlMng
}
