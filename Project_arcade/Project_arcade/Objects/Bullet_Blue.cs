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
    class Bullet_Blue : GameObject
    {
        private float speed;
        private Vector2 origin;
        private int recCount, dir;
        private float rotation;
        private double spriteTimer, spriteInterval;
        private SpriteEffects spriteEffect;


        public Bullet_Blue(Vector2 position, int dir, PlayerIndex playerIndex)
        {
            this.texture = TextureManager.GetTexture("projectile_1");
            this.velocity = new Vector2(8, 8);
            this.position = position;
            this.damage = 20;
            this.health = 10;
            this.dir = dir;
            this.playerIndex = playerIndex;
            srcRec = new Rectangle(0, 0, 50, 50);
            origin = new Vector2(0, 0);
            this.hitBox = new Rectangle((int)position.X, (int)position.Y, srcRec.Height, srcRec.Width);
            spriteTimer = 0;
            spriteInterval = 100;
            speed = 2;
        }

        private void HandleAnimation(GameTime gameTime)
        {
            spriteTimer -= gameTime.ElapsedGameTime.Milliseconds;

            if (dir == 2)
                spriteEffect = SpriteEffects.FlipHorizontally;

            if (dir == 3)
            {
                //Fix origin!?
                origin = new Vector2(texture.Width / 3 - 40, texture.Height / 3 - 15);
                origin = new Vector2(25, 0);
                rotation = -(float)Math.PI / 2;
            }
        }

        private void UpdateBulletPos()
        {
            position += velocity * speed;
            hitBox.X += (int)velocity.X * (int)speed;
            hitBox.Y += (int)velocity.Y * (int)speed;
        }

        public override void Update(GameTime gameTime)
        {
            UpdateBulletPos();
            HandleAnimation(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, srcRec, Color.Blue, rotation, origin, 1.0f, spriteEffect, 1.0f);
            //  spriteBatch.Draw(texture, new Vector2(hitBox.X, hitBox.Y), Color.Red);
            base.Draw(spriteBatch);
        }

    }
}
