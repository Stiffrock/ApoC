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
    class ParticleGenerator
    {
        private Texture2D texture;
        private Vector2 position;
        private int amount, maxLife, minLife, velMin, velMax, pgLife;
        private Particle particle;
        private Random random;
        private List<Particle> particleList;
        private bool alive;

        public int PgLife { get { return pgLife; } }
        public bool Alive { get { return alive; } }

        public List<Particle> GetParticleList { get { return particleList; } }

        public ParticleGenerator(Texture2D texture, Vector2 position, int pgLife, int amount, int velMin, int velMax, int minLife, int maxLife)
        {
            this.texture = texture;
            this.position = position;
            this.amount = amount;
            this.minLife = minLife;
            this.maxLife = maxLife;
            this.velMin = velMin;
            this.velMax = velMax;
            this.pgLife = pgLife;
            alive = true;
            random = new Random();
            particleList = new List<Particle>();
            GenerateParticles();
        }

        private void GenerateParticles()
        {
            for (int i = 0; i < amount; i++)
            {
                int lifeTime = random.Next(minLife, maxLife);
                Vector2 velocity = new Vector2(random.Next(velMin, velMax), random.Next(velMin, velMax));
            
                particle = new Particle(texture, position, velocity, lifeTime);
                particleList.Add(particle);
            }
        }

        private void RemoveParticles(GameTime gameTime)
        {
            foreach (Particle particle in particleList)
            {
                if (!particle.Alive)
                {
                    particleList.Remove(particle);
                    break;
                }
            }
        }



        public void Update(GameTime gameTime)
        {
           pgLife -= gameTime.ElapsedGameTime.Milliseconds;

            foreach (Particle p in particleList)
                p.Update(gameTime);

            if (pgLife <= 0)
                alive = false;

            RemoveParticles(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Particle p in particleList)
            {
                p.Draw(spriteBatch);
            }
        }
    }
}
