using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavidMonoIntro
{
    public static class Extensions
    {
        public static Vector2 ToVector2(this Vector3 me)
        {
            return new Vector2(me.X, me.Y);
        }
    }
}
