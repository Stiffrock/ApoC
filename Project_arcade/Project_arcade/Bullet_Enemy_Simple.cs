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
    class Bullet_Enemy_Simple : Bullet_Enemy
    {
        private float speed;
        private Vector2 target;

        public Bullet_Enemy_Simple(Vector2 position, Vector2 target)
        {
            this.texture = TextureManager.GetTexture("rock_3");
            this.velocity = new Vector2(5, 5);
            this.position = position;
            this.damage = 10;
            this.alive = true;
            this.health = 10;
            this.playerIndex = playerIndex;
            this.target = target;
            velocity = velocity * target;
            srcRec = new Rectangle(0, 0, 50, 50);
            this.hitBox = new Rectangle((int)position.X, (int)position.Y, 50, 50);
            speed = 2;
        }


        private void UpdateBulletPos()
        {
            position += velocity * speed;
            hitBox.X += (int)velocity.X * (int)speed;
            hitBox.Y += (int)velocity.Y * (int)speed;
        }

        public override void Update(GameTime gameTime)
        {
            if (alive)
            UpdateBulletPos();
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(texture, position, srcRec, Color.Orange, rotation, origin, 1.0f, spriteEffect, 1.0f);
            if (alive)    
            spriteBatch.Draw(texture, hitBox, srcRec, Color.Red); //Blir av någon anledning bättre att ta hitbox istället för position här

            //   spriteBatch.Draw(texture, position, Color.Red);
            base.Draw(spriteBatch);
        }

    }
}
