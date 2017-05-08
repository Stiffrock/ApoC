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
    class Rock : GameObject
    {
        private float rotation;
        private Vector2 origin;


        public Rock(Texture2D texture, Vector2 position, float scale)
        {
            this.texture = texture;
            this.position = position;
            random = new Random();
            srcRec = new Rectangle(0, 0, texture.Width, texture.Height);
            hitBox = new Rectangle((int)position.X, (int)position.Y, texture.Height, texture.Width);
            origin.X = texture.Width / 2;
            origin.Y = texture.Height / 2;
            this.scale = scale;
            hitBox.Width *= (int)scale;
            hitBox.Height *= (int)scale;
            points = 10;
            health = (50 * (int)scale) + 1;
            damage = 25;
            rotation = 0;
            SetRotationAndVelocity();
            

        }

        private void SetRotationAndVelocity()
        {
            rotationSpeed = (float)random.NextDouble();
            velocity = new Vector2(random.Next(-5, 5), random.Next(-5, 5));

        }

        public override void Update(GameTime gameTime)
        {
            position += velocity;
            rotation += rotationSpeed/10;
            hitBox.X += (int)velocity.X;
            hitBox.Y += (int)velocity.Y;
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(texture, position, srcRec, Color.White);
            spriteBatch.Draw(texture, position, srcRec, Color.White, rotation, origin, scale, SpriteEffects.None, 1);
            base.Draw(spriteBatch);
        }
    }
}
