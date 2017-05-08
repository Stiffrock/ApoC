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
    abstract class GameObject
    {
        protected Texture2D texture;
        protected Vector2 position, offset, velocity;
        protected Rectangle hitBox, srcRec;
        protected int health, damage, points;
        protected float rotationSpeed, scale;
        protected Random random;
        protected PlayerIndex playerIndex;


        public Vector2 Position { get { return position; } set { position = value; } }
        public Vector2 Offset { get { return offset; } set { offset = value; } }
        public Rectangle Hitbox { get { return hitBox; } set { hitBox = value; } }
        public int Health { get { return health; } set { health = value; } }
        public int Damage { get { return damage; } }
        public int Points { get { return points; } }
        public Vector2 Velocity { get { return velocity; } set { velocity = value; } }
        public float Scale { get { return scale; } }

        public Rectangle GetSrcRec { get { return srcRec; } }

        public PlayerIndex PlayerIndex { get { return playerIndex; } set { playerIndex = value; } }



        public virtual void Update(GameTime gameTime)
        { }

        public virtual void Draw(SpriteBatch spriteBatch)
        { }
    }
}
