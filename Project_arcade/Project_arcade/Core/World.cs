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
    class World
    {
        private Rock rock;
        private List<GameObject> objectList;
        private List<Enemy> enemyList;
      //  private List<Bullet_Enemy_Simple> enemyBulletList;
        private List<Item> itemList;
        private List<Vector2> background, middleground, foreground;
        private Enemy_Simple enemy;
        private Random rnd;
        private int bgSpacing, mgSpacing, fgSpacing, objCount, cloudIndex, difficulty;
        private float bgSpeed, mgSpeed, fgSpeed;
        private double deltaTime, diffTimer;
        private Vector2 avragePos, worldPos;
        private Vector2[] hpArray, hpArray2;
        private Texture2D[] texArray, rockArray, cloudArray, starArray;
        private Weapon_TS weapon_TS;
        


        public Vector2 WorldPos { get { return worldPos; } set { WorldPos = value; } }
        public List<Enemy> EnemyList { get { return enemyList; } set { enemyList = value; } }
        public List<Vector2> BackGround { get { return background; } set { background = value; } }
        public Vector2[] Player1Hp { get { return hpArray; } set { hpArray = value; } }
        public Vector2[] Player2Hp { get { return hpArray2; } set { hpArray2 = value; } }

        public World()
        {
            itemList = new List<Item>();
            rnd = new Random();
            objectList = new List<GameObject>();
            enemyList = new List<Enemy>();
            cloudArray = new Texture2D[2];
            cloudArray[0] = TextureManager.GetTexture("cloud_1");
            cloudArray[1] = TextureManager.GetTexture("cloud_2");
            starArray = new Texture2D[1];
            starArray[0] = TextureManager.GetTexture("stars_1");
            rockArray = new Texture2D[2];
            rockArray[0] = TextureManager.GetTexture("debris_1");
            rockArray[1] = TextureManager.GetTexture("debris_2");
            texArray = new Texture2D[1];
            cloudIndex = 0;
            texArray[0] = TextureManager.GetTexture("bg_1");
            background = new List<Vector2>();
            middleground = new List<Vector2>();
            foreground = new List<Vector2>();
            worldPos = new Vector2(0, 0);
            ParallaxBg();
            objCount = 0;
            difficulty = 1;
            hpArray = new Vector2[10];
            hpArray2 = new Vector2[10];

            SetHpArray();
   

        }

        public List<GameObject> GetObjList { get { return objectList; } }

        private void ParallaxBg()
        {
            bgSpacing = texArray[0].Height;
            bgSpeed = 0.2f;

            for (int i = 0; i < 2; i++)
            {          
                background.Add(new Vector2(0, i * -bgSpacing));
            }

            mgSpacing = 1920 / 2;
            mgSpeed = 0.5f;

            for (int i = 0; i < 3; i++)
            {
                int mgSpacing2 = rnd.Next(0, 700);

                middleground.Add(new Vector2(i * mgSpacing2, i * -mgSpacing));
            }



            fgSpacing = 400;
            fgSpeed = 1.0f;

            for (int i = 0; i < 6; i++)
            {
                int fgSpacing2 = rnd.Next(-200,200);
                foreground.Add(new Vector2(i* fgSpacing2, i * - fgSpacing));
            }
        }


        /*Calculates the avrage position of the two players and a random offset and uses the resulting vector to create an object*/
        private void GenerateObjects(GameTime gameTime, Vector2 playerPos1, Vector2 playerPos2)
        {
            deltaTime += gameTime.ElapsedGameTime.Milliseconds;
            avragePos = (playerPos1 += playerPos2) / 2;
            int chooseRock = rnd.Next(0, 2);
            double scale = rnd.NextDouble();
            scale *= 2;
            Texture2D rockTex = rockArray[chooseRock];
            Vector2 offset = new Vector2(rnd.Next(-900,900), (rnd.Next(-1500,-800)));
            Vector2 newPosition = avragePos + offset;
            
            if (deltaTime >= 100 / difficulty)
            {
                objCount += 1;
                rock = new Rock(rockTex, newPosition, (float)scale);
                objectList.Add(rock);
                deltaTime = 0;
            }
            if (objCount >= 100)
            {
                Vector2 enemyVector = new Vector2(avragePos.X, newPosition.Y);
                enemy = new Enemy_Simple(TextureManager.GetTexture("rock_2"), enemyVector);
                enemyList.Add(enemy);
                objCount = 0;
            }
            KillObjects(avragePos);
        }


        private void UpdateGameObjects(GameTime gameTime)
        {
            foreach (GameObject obj in objectList)
            {
                obj.Update(gameTime);
            }
        }


        private void HandleParallax()
        {
            for (int i = 0; i < background.Count; i++)
            {
                background[i] = new Vector2(background[i].X, background[i].Y + bgSpeed);        // 0, -1920
                    
                if (background[i].Y >= bgSpacing)  // om Y är större än 1900
                {
                    int j = i - 1;

                    if (j < 0)
                    {
                        j = background.Count - 1;
                    }

                    background[i] = new Vector2(background[i].X, background[j].Y - (bgSpacing - 1));
                }
            }

            for (int i = 0; i < middleground.Count; i++)
            {
                middleground[i] = new Vector2(middleground[i].X, middleground[i].Y + mgSpeed);        // 0, -1920

                if (middleground[i].Y >= (mgSpacing+500))  // om Y är mindre än -1920
                {
                    //cloudIndex++;
                    int j = i - 1;

                    if (j < 0)
                    {
                        j = middleground.Count - 1;
                    }

                    middleground[i] = new Vector2(middleground[i].X, middleground[j].Y - mgSpacing);
                  //  cloudIndex = rnd.Next(1, 3);
                }
            }

            for (int i = 0; i < foreground.Count; i++)
            {
                foreground[i] = new Vector2(foreground[i].X, foreground[i].Y + fgSpeed);        // 0, -1920

                if (foreground[i].Y >= (fgSpacing+500))  // om Y är mindre än -1920
                {
                    int j = i - 1;

                    if (j < 0)
                    {
                        j = foreground.Count - 1;
                    }

                    foreground[i] = new Vector2(foreground[i].X, foreground[j].Y - fgSpacing);
                    //  cloudIndex = rnd.Next(1, 3);
                }
            }



        }

        private void KillObjects(Vector2 pos)
        {
            foreach (Enemy enemy in enemyList)
            {
                if (enemy.Position.Y >= avragePos.Y + 1000)
                {
                    enemyList.Remove(enemy);
                    break;
                }
            }
        }

        public void UpdateSinglePlayer(GameTime gameTime, Vector2 playerPos1)
        {
            GenerateObjects(gameTime, playerPos1, playerPos1);
            UpdateGameObjects(gameTime);
            HandleParallax();
            foreach (Enemy e in enemyList)
            {
                e.Update(gameTime);
                e.FireAtPlayer(playerPos1, playerPos1);
            }
        }

        private void SetHpArray()
        {
            for (int i = 0; i < 10; i++)
            {
                hpArray[i] = new Vector2(200 + (20 * i), 100);
                hpArray2[i] = new Vector2(1720 - (20 * i), 100);
               // spriteBatch.Draw(TextureManager.GetTexture("HpBar1"), new Vector2(200 + (20 * i), 100), Color.White);
            }
        }

        private void HandleDifficulty(GameTime gameTime)
        {
            diffTimer += gameTime.ElapsedGameTime.Milliseconds;

            if (diffTimer >= 1000 * 50)
            {
                difficulty += 1;
                diffTimer = 0;
            }
        }

        public void Update(GameTime gameTime, Vector2 playerPos1, Vector2 playerPos2, Vector2 worldPos)
        {
            GenerateObjects(gameTime, playerPos1, playerPos2);
            UpdateGameObjects(gameTime);
            HandleDifficulty(gameTime);

            foreach (Enemy e in enemyList)
            {            
                e.Update(gameTime);              
                e.FireAtPlayer(playerPos1, playerPos2);
                foreach (Bullet_Enemy b in e.BulletList)
                {
                    b.Update(gameTime);
                }
            }

            HandleParallax();         
        }

        public void DrawBackground(SpriteBatch spriteBatch)
        {
            foreach (Vector2 v in background)
                spriteBatch.Draw(texArray[0], v, new Rectangle(0, 0, 1920, 1080), Color.White);

            for (int i = 0; i < middleground.Count - 1; i++)
                spriteBatch.Draw(cloudArray[0], middleground[i], new Rectangle(0, 0, 1920, 1080), Color.White * 0.5f);

            for (int i = 0; i < foreground.Count - 1; i++)
                spriteBatch.Draw(starArray[0], foreground[i], new Rectangle(0, 0, 1920, 1080), Color.White);

            spriteBatch.DrawString(FontManager.GetFont("menu_font"), "Difficulty: " + difficulty.ToString(), new Vector2(920, 950), Color.White);
        }

        public void DrawObjects(SpriteBatch spriteBatch)
        {
            foreach (GameObject obj in objectList)            
                obj.Draw(spriteBatch);
            
            foreach (Enemy e in enemyList)
            {
                e.Draw(spriteBatch);
                if (e is Enemy_Simple)
                {
                    foreach (Bullet_Enemy b in e.BulletList)
                    {
                        b.Draw(spriteBatch);
                    }
                }        
            }     
        }
    }
}
