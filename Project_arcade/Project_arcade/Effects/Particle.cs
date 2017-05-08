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
    class Particle
    {
        //Fixx så att lifeTime är float istället för int

        private Vector2 velocity, position;
        private Texture2D texture;
        private Rectangle srcRec;
        private int lifeTime, effect;
        private float rotation, rotationSpeed;
        private bool alive;
        private Random rnd;
        private SpriteEffects spriteEffect;

        public bool Alive { get { return alive; } }

        public Particle(Texture2D texture, Vector2 position, Vector2 velocity, int lifeTime)
        {
            this.texture = texture;
            this.position = position;
            this.velocity = velocity;
            this.lifeTime = lifeTime;          
            rnd = new Random();
            rotation = 0;
            effect = rnd.Next(0, 2);
            if (effect == 1)
                spriteEffect = SpriteEffects.FlipHorizontally;
            if (effect == 2)
                spriteEffect = SpriteEffects.FlipVertically;
    
    
            rotationSpeed = rnd.Next(1, 2);
            srcRec = new Rectangle(0, 0, 50, 50);
            alive = true;
        }


        public void Update(GameTime gameTime)
        {
            lifeTime -= gameTime.ElapsedGameTime.Milliseconds;
            if (lifeTime <= 0)
            {
                alive = false;
            }
            position += velocity;
            rotation += rotationSpeed / 10;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(texture, position, Color.White);
            spriteBatch.Draw(texture, position, srcRec, Color.White, rotation, new Vector2(25,25), 0.2f, spriteEffect, 1);

        }
    }
}
