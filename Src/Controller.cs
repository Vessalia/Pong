using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pong
{
    public abstract class Controller
    {
        protected Paddle paddle;

        public Controller(Paddle paddle)
        {
            this.paddle = paddle;
        }

        public abstract void HandleInput();
    }
}
