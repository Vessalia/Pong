using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pong
{
    class PlayerController : Controller
    {
        public PlayerController(Paddle paddle) : base(paddle) { }

        public override void HandleInput()
        {
            int dir = 0;
            if (Keyboard.GetState().IsKeyDown(Keys.Up)) dir -= 1;
            if (Keyboard.GetState().IsKeyDown(Keys.Down)) dir += 1;
            paddle.SetDirection(dir);
        }
    }
}
