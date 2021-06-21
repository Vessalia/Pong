using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Pong
{
    public class PlayState : GameState
    {
        private readonly Ball ball;
        private readonly Paddle playerPaddle;
        private readonly Paddle aiPaddle;
        private readonly Controller playerController;
        private readonly Controller aiController;
                
        private readonly int paddleWidth;
        private readonly int maxScore;
        private int aiScore;
        private int playerScore;
        private float timer;

        public PlayState(IGameStateSwitcher switcher) : base(switcher)
        {
            paddleWidth = 20;
            int paddleHeight = 200;
            float paddleVelY = 180;
            float initialBallVel = -400;
            int ballSize = 10;

            maxScore = 5;

            timer = 4;

            float initialMaxAngle = MathF.PI / 20;
            float initialAngle = (float)(2 * new Random().NextDouble() - 1) * initialMaxAngle;

            playerPaddle = new Paddle(paddleWidth / 2, Constants.Height / 2, paddleWidth, paddleHeight, paddleVelY, Color.White);
            aiPaddle = new Paddle(Constants.Width - paddleWidth / 2, Constants.Height / 2, paddleWidth, paddleHeight, paddleVelY, Color.White);

            ball = new Ball(Constants.Width / 2, Constants.Height / 2, ballSize, MathF.Cos(initialAngle) * initialBallVel, MathF.Sin(initialAngle) * initialBallVel, Color.White);

            playerController = new PlayerController(playerPaddle);
            aiController = new AIController(aiPaddle, ball);

            ball.onBallUpdate += ((AIController)aiController).OnBallUpdate;
        }

        public override void HandleInput()
        {
            playerController.HandleInput();
            aiController.HandleInput();
        }

        public override void Update(float timeStep)
        {
            if (playerScore == maxScore || aiScore == maxScore)
            {
                if (timer >= 0)
                {
                    timer -= timeStep;
                }
                else
                {
                    switcher.SetNextState(new MainMenuState(switcher));
                }
                return;
            }

            if (timer >= 0)
            {
                timer -= timeStep;
                if (timer >= 1)
                {
                    return;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                switcher.SetNextState(new PauseState(switcher, this));
            }

            int addVel = 100;
            float maxAngle = MathF.PI / 4;

            playerPaddle.Update(timeStep);

            //determine AI movement
            aiPaddle.Update(timeStep);

            ball.Update(timeStep);
            ball.CollisionUpdate(playerPaddle, maxAngle, addVel);
            ball.CollisionUpdate(aiPaddle, maxAngle, addVel);

            var side = ball.HasTouchedWall();
            if (side != null)
            {
                ball.ResetBall();
                playerPaddle.ResetPaddle();
                ((AIController)aiController).ResetGhostBall();
                aiPaddle.ResetPaddle();
                if (side == WallSide.left)
                {
                    aiScore += 1;
                }
                else if (side == WallSide.right)
                {
                    playerScore += 1;
                }
                timer = 4;
            }
        }

        public override void DrawToScreen(SpriteBatch sb, SpriteFont font)
        {
            playerPaddle.DrawShape(sb);
            aiPaddle.DrawShape(sb);

            ball.DrawShape(sb);

            sb.DrawString(font, playerScore + "", new Vector2(paddleWidth, 0), Color.Blue);
            sb.DrawString(font, aiScore + "", new Vector2(Constants.Width - paddleWidth - font.MeasureString(aiScore + "").X, 0), Color.Red);

            if (aiScore == maxScore)
            {
                var text = "YOU FUCKING LOSE YOU FUCKING LOSER ASS BITCH";
                var textSize = font.MeasureString(text);

                sb.DrawString(font, text, new Vector2(Constants.Width, Constants.Height) / 2 - textSize / 2, Color.Purple);
                return;
            }
            else if (playerScore == maxScore)
            {
                var text = "YOU FUCKING WIN YOU FUCKING WINNER ASS BITCH";
                var textSize = font.MeasureString(text);

                sb.DrawString(font, text, new Vector2(Constants.Width, Constants.Height) / 2 - textSize / 2, Color.Purple);
                return;
            }

            if (timer >= 1)
            {
                var text = $"{(int)timer}";
                var textSize = font.MeasureString(text);

                sb.DrawString(font, text, new Vector2(Constants.Width / 2, Constants.Height / 2) - textSize / 2, Color.Purple);
            }
            else if (timer >= 0)
            {
                var text = "GO";
                var textSize = font.MeasureString(text);

                sb.DrawString(font, text, new Vector2(Constants.Width / 2, Constants.Height / 2) - textSize / 2, Color.Purple);
            }
        }

        public Vector2 GetBallPos()
        {
            return ball.pos;
        }

        public Vector2 GetBallVel()
        {
            return ball.vel;
        }
    }
}
