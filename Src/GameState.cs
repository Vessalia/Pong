using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pong
{
    public abstract class GameState
    {
        protected IGameStateSwitcher switcher;

        public GameState(IGameStateSwitcher switcher)
        {
            this.switcher = switcher;
        }

        public abstract void HandleInput();
        public abstract void Update(float timeStep);
        public abstract void DrawToScreen(SpriteBatch sb, SpriteFont font);
    }
}
