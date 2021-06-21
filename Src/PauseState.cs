using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pong
{
    class PauseState : GameState
    {
        private readonly Menu menu;
        private readonly PlayState playState;

        public PauseState(IGameStateSwitcher switcher, PlayState playState) : base(switcher)
        {
            menu = new Menu();

            this.playState = playState;

            var resumePos = new Vector2(Constants.Width / 2, Constants.Height / 2);

            Action resumeAction = () =>
            {
                switcher.SetNextState(playState);
            };

            var menuPos = new Vector2(Constants.Width / 2, Constants.Height / 2 + 100);

            Action menuAction = () =>
            {
                switcher.SetNextState(new MainMenuState(switcher));
            };

            var exitPos = new Vector2(Constants.Width / 2, Constants.Height / 2 + 200);

            Action exitAction = () =>
            {
                switcher.SetNextState(null);
            };

            menu.AddButton(resumePos, Color.White, "Resume", resumeAction);
            menu.AddButton(menuPos, Color.White, "Menu", menuAction);
            menu.AddButton(exitPos, Color.White, "Exit", exitAction);
        }

        public override void HandleInput()
        {
            menu.HandleButtonInput();
        }

        public override void Update(float timeStep) { }

        public override void DrawToScreen(SpriteBatch sb, SpriteFont font)
        {
            playState.DrawToScreen(sb, font);

            var arrowEnd = playState.GetBallVel();
            arrowEnd.Normalize();

            sb.DrawArrow(playState.GetBallPos(), playState.GetBallPos() + 50 * arrowEnd);

            sb.FillRectangle(0, 0, Constants.Width, Constants.Height, new Color(Color.CornflowerBlue, 0.84f));

            menu.DrawButtons(sb, font);

            var text = "Paused";
            var textSize = font.MeasureString(text);

            sb.DrawString(font, text, new Vector2(Constants.Width / 2, 200) - textSize / 2, Color.HotPink);
        }
    }
}
