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
    class Pickup_SC : Item
    {
        public Pickup_SC(Texture2D texture)
        {
            this.texture = texture;
        }
    }
}
