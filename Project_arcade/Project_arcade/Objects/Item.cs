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
    abstract class Item
    {
        protected Texture2D texture;
        protected Vector2 position;
        protected Rectangle rectangle;
        protected string key;

        public Vector2 Position { get { return position; } set { position = value; } }
        public Rectangle Rectangle { get { return rectangle; } set { rectangle = value; } }

        public string Key { get { return key; } }

        public virtual void Update(GameTime gameTime)
        { }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

    }
}
