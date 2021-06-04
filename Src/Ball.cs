using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pong
{
    public enum WallSide
    {
        left, right
    }

    public delegate void OnUpdate(Ball ball, float timeStep);

    public class Ball : Entity
    {
        public OnUpdate onBallUpdate;

        private float radius;

        private WallSide? touchedWall;

        private Vector2 initialPos;
        private Vector2 initialVel;

        public Ball(float posX, float posY, float radius, float velX, float velY, Color colour) : base(posX, posY, velX, velY, colour)
        {
            this.radius = radius;
            initialPos = pos;
            initialVel = vel;
            touchedWall = null;
            this.colour = colour;
        }

        public Ball(Ball ball) : base(ball.pos.X, ball.pos.Y, ball.vel.X, ball.vel.Y, ball.colour)
        {
            this.radius = ball.radius;
            initialPos = pos;
            initialVel = vel;
            touchedWall = null;
            this.colour = ball.colour;
        }

        public override void DefineSize(int width, int height = 0)
        {
            radius = width / 2;
        }

        public void DrawShape(SpriteBatch sb)
        {
            for (int i = 0; i <= radius; i++)
            {
                sb.DrawCircle((int)pos.X, (int)pos.Y, (int)i, 64, colour);
            }
        }


        public void CollisionUpdate(Paddle paddle, float maxAngle, int addVel)
        {
            var hitCircle = new CircleF(pos, radius);
            var hitRect = new BoundingRectangle(paddle.pos, new Vector2 (paddle.Width, paddle.Height) / 2);
            if (CircleF.Intersects(hitCircle, hitRect))
            {
                float thetaRange = 2 * (paddle.pos.Y - pos.Y) / paddle.Height;

                float theta = thetaRange * maxAngle;

                float magVel = vel.Length();

                vel.Y = -(float)Math.Sin(theta) * magVel;
                vel.X = (float)Math.Cos(theta) * magVel;
                if (pos.X < paddle.pos.X)
                {
                    vel.X = -vel.X - addVel;
                }
            }
        }


        public override void Update(float timeStep)
        {
            pos += vel * timeStep;
            if (pos.Y + radius > Constants.Height)
            {
                vel.Y = -vel.Y;
                pos.Y = Constants.Height - radius - (pos.Y + radius - Constants.Height);
            }
            else if (pos.Y - radius < 0)
            {
                vel.Y = -vel.Y;
                pos.Y = radius - (pos.Y - radius);
            }
            if (pos.X + radius > Constants.Width)
            {
                touchedWall = WallSide.right;
            }
            else if (pos.X - radius < 0)
            {
                touchedWall = WallSide.left;
            }

            onBallUpdate?.Invoke(this, timeStep);
        }


        public void ResetBall()
        {
            touchedWall = null;
            pos = initialPos;
            vel = initialVel;
        }


        public WallSide? HasTouchedWall()
        {
            return touchedWall;
        }
    }
}
