﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Pong
{
    public interface GameStateSwitcher
    {
        public void SetNextState(GameState gameState);
    }
}
