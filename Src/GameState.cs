using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pong
{
    public abstract class GameState
    {
        protected GameStateSwitcher switcher;

        public GameState(GameStateSwitcher switcher)
        {
            this.switcher = switcher;
        }

        public abstract void HandleInput();
        public abstract void Update(float timeStep);
        public abstract void DrawToScreen(SpriteBatch sb, SpriteFont font);
    }
}
