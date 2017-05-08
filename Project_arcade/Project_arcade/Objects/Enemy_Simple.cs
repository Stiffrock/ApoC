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
    class Enemy_Simple : Enemy
    {
        private Bullet_Enemy_Simple bullet;
        private int target;
        private Vector2 targetPos;
        private Random rnd;
        private bool shotReady;
        private float shotTimer, shotDelay;


        public Enemy_Simple(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
            this.hitBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            shotDelay = 1500;
            health = 60;
            targetPos = new Vector2(0, 0);
            rnd = new Random();
            target = rnd.Next(1, 2);
        }

        public override void FireAtPlayer(Vector2 playerPos, Vector2 playerPos2)
        {
            Vector2 targetDirection;
            if (target == 1)
                targetDirection = playerPos - position;
            else
                targetDirection = playerPos2 - position;
                          
            targetDirection.Normalize();

            if (shotReady)
            {
                bullet = new Bullet_Enemy_Simple(position, targetDirection);
                bulletList.Add(bullet);
                shotReady = false;
            }
          
        }

        private void Behaviour()
        {
            position.Y -= 1;
            hitBox.Y -= (int)1;
        }

        private void ShotDelay(GameTime gameTime)
        {
            shotTimer += gameTime.ElapsedGameTime.Milliseconds;
            if (shotTimer >= shotDelay)
            {
                shotReady = true;
                shotTimer = 0;
            }
        }


        public override void Update(GameTime gameTime)
        {
            Behaviour();
            ShotDelay(gameTime);
            
               foreach (Bullet_Enemy_Simple b in bulletList)                
                    b.Update(gameTime);
           
   
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Bullet_Enemy_Simple b in bulletList)
                bullet.Draw(spriteBatch);             
                spriteBatch.Draw(texture, position, Color.Yellow);
            base.Draw(spriteBatch);
        }


    }
}
