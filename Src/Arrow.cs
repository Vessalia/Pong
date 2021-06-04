using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pong
{
    public static class Arrow
    {
        public static void DrawArrow(this SpriteBatch sb, Vector2 start, Vector2 end)
        {
            float angle = MathF.PI / 4;
            var tipEnd = 0.25f * (start - end);
            var tip1End = tipEnd.Rotate(angle);
            var tip2End = tipEnd.Rotate(-angle);

            sb.DrawLine(start, end, Color.Black, 5);
            sb.DrawLine(end, end + tip1End, Color.Black, 5);
            sb.DrawLine(end, end + tip2End, Color.Black, 5);
        }
    }
}
