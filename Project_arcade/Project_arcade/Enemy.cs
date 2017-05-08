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
    class Enemy
    {
        protected Texture2D texture;
        protected Vector2 position;
        protected Rectangle hitBox;
        protected List<Bullet_Enemy> bulletList = new List<Bullet_Enemy>();
        protected int health;
        protected bool alive;

        public List<Bullet_Enemy> BulletList { get { return bulletList; } set { bulletList = value; } }
        public bool Alive { get { return alive; } set { alive = value; } }
        public Rectangle HitBox { get { return hitBox; } set { hitBox = value; } }
        public Vector2 Position { get { return position; } set { position = value; } }
        public int Health { get { return health; } set { health = value; } }
         
        public virtual void FireAtPlayer(Vector2 playerPos, Vector2 playerPos2)
        { }
        public virtual void Update(GameTime gameTime)
        { }

        public virtual void Draw(SpriteBatch spriteBatch)
        { }

    }
}
