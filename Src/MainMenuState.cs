using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pong
{
    public class MainMenuState : GameState
    {
        private readonly Menu menu;

        public MainMenuState(IGameStateSwitcher switcher) : base(switcher)
        {
            menu = new Menu();

            var playPos = new Vector2(Constants.Width / 2, Constants.Height / 2);

            Action playAction = () =>
            {
                switcher.SetNextState(new PlayState(switcher));
            };

            var exitPos = new Vector2(Constants.Width / 2, Constants.Height / 2 + 100);

            Action exitAction = () =>
            {
                switcher.SetNextState(null);
            };

            menu.AddButton(playPos, Color.White, "Play", playAction);
            menu.AddButton(exitPos, Color.White, "Exit", exitAction);
        }

        public override void HandleInput()
        {
            menu.HandleButtonInput();
        }

        public override void Update(float timeStep) { }

        public override void DrawToScreen(SpriteBatch sb, SpriteFont font)
        {
            menu.DrawButtons(sb, font);

            var text = "Pong";
            var textSize = font.MeasureString(text);

            sb.DrawString(font, text, new Vector2(Constants.Width / 2, 200) - textSize / 2, Color.OrangeRed);
        }
    }
}
