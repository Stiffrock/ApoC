using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Project_arcade
{
    class Pickup_Blue : Item
    {
        public Pickup_Blue(Texture2D texture)
        {
            this.texture = texture;
        }
    }
}
