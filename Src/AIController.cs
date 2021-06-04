using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pong
{
    class AIController : Controller
    {
        private readonly Ball ghostBall;

        private readonly float tol;
        private readonly  int radius;

        private float prevVelX;
        private float prevGhostVelX;

        private bool hasCollided;

        public AIController(Paddle paddle, Ball ball) : base(paddle)
        {
            this.ghostBall = new Ball(ball);
            prevVelX = ball.vel.X;
            tol = (float)paddle.Height/4;
            hasCollided = false;
            radius = 20;
        }

        public void OnBallUpdate(Ball ball, float timeStep)
        {
            if (prevVelX < 0 && ball.vel.X > 0)
            {
                hasCollided = true;
                ghostBall.pos = ball.pos;
                ghostBall.vel = ball.vel * 0.95f;
            }
            else if (prevVelX > 0 && ball.vel.X < 0)
            {
                hasCollided = false;
                ghostBall.ResetBall();
            }

            if (hasCollided)
            {
                ghostBall.Update(timeStep);
            }

            prevVelX = ball.vel.X;
        }

        public override void HandleInput()
        {
            if ((prevGhostVelX > 0 && ghostBall.vel.X < 0) || ghostBall.HasTouchedWall() != null)
            {
                ghostBall.vel = Vector2.Zero;
            }

            prevGhostVelX = ghostBall.vel.X;

            if (ghostBall.pos.Y - paddle.pos.Y > tol)
            {
                paddle.SetDirection(1);
            }
            else if (paddle.pos.Y - ghostBall.pos.Y > tol)
            {
                paddle.SetDirection(-1);
            }
            else
            {
                paddle.SetDirection(0);
            }
        }


        public void ResetGhostBall()
        {
            ghostBall.ResetBall();
        }


        public void DrawShape(SpriteBatch sb)
        {
            for (int i = 0; i <= radius; i++)
            {
                sb.DrawCircle((int)ghostBall.pos.X, (int)ghostBall.pos.Y, (int)i, 64, Color.Gray);
            }
        }
    }
}
