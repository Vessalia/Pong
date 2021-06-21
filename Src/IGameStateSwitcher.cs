using System;
using System.Collections.Generic;
using System.Text;

namespace Pong
{
    public interface IGameStateSwitcher
    {
        public void SetNextState(GameState gameState);
    }
}
