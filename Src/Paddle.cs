using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pong
{
    public class Paddle : Entity
    {
        public float Width { get; private set; }
        public float Height { get; private set; }

        public Vector2 initialPos;
        public Vector2 initialVel;

        private float dir;
        private int speed;

        public Paddle(float posX, float posY, float width, float height, float velY, Color colour) : base(posX, posY, 0, velY, colour)
        {
            this.Width = width;
            this.Height = height;
            this.colour = colour;
            speed = Constants.Height;

            initialPos = pos;
            initialVel = vel;
        }

        public override void DefineSize(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public void DrawShape(SpriteBatch sb)
        {
            sb.FillRectangle((int)pos.X-Width/2, (int)pos.Y-Height/2, (int)Width, (int)Height, colour);
        }

        public override void Update(float timeStep)
        {
            pos.Y += dir * speed * timeStep;
            if (pos.Y + Height/2 > Constants.Height)
            {
                pos.Y = Constants.Height - Height/2;
            }
            else if (pos.Y < Height/2)
            {
                pos.Y = Height/2;
            }
        }

        public void SetDirection(float dir)
        {
            this.dir = dir;
        }

        public void ResetPaddle()
        {
            pos = initialPos;
            vel = initialVel;
        }
    }
}
