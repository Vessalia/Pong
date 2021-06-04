using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pong
{
    public abstract class Entity
    {
        public Vector2 pos;
        public Vector2 vel;

        protected Color colour;

        public Entity(float posX, float posY, float velX, float velY, Color colour)
        {
            this.pos = new Vector2 (posX, posY);
            this.vel = new Vector2 (velX, velY);
            this.colour = colour;
        }


        public abstract void Update(float timeStep);


        public abstract void DefineSize(int width, int height);
    }
}
