using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace Project_arcade
{
    class GamePlay
    {
        private Camera camera;
        private Matrix transform;
        private World world;       
        private Vector2 StartPos1, StartPos2, worldPos;
        private Player player1, player2;
        private ParticleGenerator particleGenerator;
        private List<ParticleGenerator> pgList;
        private List<GameObject> bulletList1, bulletList2;
        private List<Bullet_Enemy> enemyBulletList;
        private List<Item> itemList;
        private bool pvp, singlePlayer, play, retry, exit, failure;
        private Random rnd;
        private SoundEffect sound;
        private string name;

        public bool Retry { get { return retry; } set { retry = value; } }

        public bool Exit { get { return exit; } set { exit = value; } }

        public GamePlay(bool pvp, bool singlePlayer, string name)
        {
            this.pvp = pvp;
            this.singlePlayer = singlePlayer;
            this.name = name;
            InitPlayers();
            play = true;
            worldPos = new Vector2(0, 0);
            bulletList1 = new List<GameObject>();
            bulletList2 = new List<GameObject>();
            enemyBulletList = new List<Bullet_Enemy>();
            itemList = new List<Item>();
            pgList = new List<ParticleGenerator>();
            rnd = new Random();
            camera = new Camera(new Vector2(500,500));
            world = new World();
        }

        private void InitPlayers()
        {
            StartPos1 = new Vector2(400, 200);
            player1 = new Player(TextureManager.GetTexture("sd_spritesheet"), StartPos1, true);

            if (!singlePlayer)
            {
                StartPos2 = new Vector2(1520, 200);
                player2 = new Player(TextureManager.GetTexture("sd_spritesheet"), StartPos2, false);
            }
                
        }

        private void HandleFailure()
        {
            if (failure)
            {
                if (InputHandler.IsButtonDown(PlayerIndex.One, PlayerInput.Start))
                {
                    retry = true;
                }
                if (InputHandler.IsButtonDown(PlayerIndex.One, PlayerInput.Yellow))
                {
                    exit = true;
            
            }
}
        }

        private void DrawFailure(SpriteBatch spriteBatch)
        {
            if (!singlePlayer)
                spriteBatch.DrawString(FontManager.GetFont("menu_font"), "SCORE:    " + (player1.Score + player2.Score), new Vector2(900, 550), Color.Red);
            else
                spriteBatch.DrawString(FontManager.GetFont("menu_font"), "SCORE:   " + (player1.Score), new Vector2(900, 550), Color.Red);

            spriteBatch.DrawString(FontManager.GetFont("default_font"), "FAILURE", new Vector2(850, 400), Color.Red); 
            spriteBatch.DrawString(FontManager.GetFont("menu_font"), "START TO RETRY", new Vector2(900, 600), Color.Red);
            spriteBatch.DrawString(FontManager.GetFont("menu_font"), "YELLOW TO EXIT", new Vector2(900, 650), Color.Red);
        }

        private bool CollisionDetection(Rectangle rec1, Rectangle rec2)
        {
            if (rec1.Intersects(rec2))
                return true;
            else
                return false;
        }

        private void HandleItemSpawn(Vector2 position)
        {
            int type = rnd.Next(1, 8);
            Item item;           
            switch (type)
            {
                case 1:
                    item = new Weapon_TS(TextureManager.GetTexture("pickup"));
                    item.Position = position;
                    item.Rectangle = new Rectangle((int)position.X, (int)position.Y, 50, 50);
                    itemList.Add(item);

                    break;
                case 2:
                    item = new Weapon_TS(TextureManager.GetTexture("pickup"));
                    item.Position = position;
                    item.Rectangle = new Rectangle((int)position.X, (int)position.Y, 50, 50);
                    itemList.Add(item);
                    break;
                case 3:
                    item = new Pickup_Blue(TextureManager.GetTexture("pickup"));
                    item.Position = position;
                    item.Rectangle = new Rectangle((int)position.X, (int)position.Y, 50, 50);
                    itemList.Add(item);
                    break;
                case 4:
                    item = new Pickup_Red(TextureManager.GetTexture("pickup"));
                    item.Position = position;
                    item.Rectangle = new Rectangle((int)position.X, (int)position.Y, 50, 50);
                    itemList.Add(item);
                    break;
                case 5:
                    item = new Pickup_Health(TextureManager.GetTexture("health_pickup"));
                    item.Position = position;
                    item.Rectangle = new Rectangle((int)position.X, (int)position.Y, 50, 50);
                    itemList.Add(item);
                    break;
                case 6:
                    item = new Pickup_Points(TextureManager.GetTexture("health_pickup"));
                    item.Position = position;
                    item.Rectangle = new Rectangle((int)position.X, (int)position.Y, 50, 50);
                    itemList.Add(item);
                    break;
                case 7:
                    item = new Pickup_SC(TextureManager.GetTexture("pickup"));
                    item.Position = position;
                    item.Rectangle = new Rectangle((int)position.X, (int)position.Y, 50, 50);
                    itemList.Add(item);
                    break;
                default:
                    break;
            }                     
        }

        private void DrawItems(SpriteBatch spriteBatch)
        {
            foreach (Item item in itemList)
            {
                item.Draw(spriteBatch);
            }
        }

        private void ItemCollision(Player player)
        {
            foreach (Item item in itemList)
            {
                if (CollisionDetection(player.GetHitbox, item.Rectangle))
                {
                    if (SoundManager.SoundOn)        
                        SoundManager.GetSound("powerup").Play(0.01f, 0f, 0f);                 
             
                    if (item is Weapon_TS)
                    {
                        player.Weapon_TS = true;
                        player.Score += 20;

                    }
                    if (item is Pickup_Blue)
                    {
                        if (!player.Pickup_Blue)
                            player.Weapon_TS = false;

                        player.Pickup_Blue = true;
                        player.Score += 20;
                    }
                    if (item is Pickup_Red)
                    {
                        if (!player.Pickup_Red)
                            player.Weapon_TS = false;
                        player.Pickup_Red = true;
                        player.Score += 20;
                    }
                    if (item is Pickup_Health)
                    {
                        if (player.Health <= 75)
                            player.Health += 25;
                        if (player.Health < 100 && player.Health > 75)
                        {
                            int h = 100 - player.Health;
                            player.Health += h;
                        }
                    }
                    if (item is Pickup_Points)
                        player.Score += 100;

                    if (item is Pickup_SC)
                    {
                        player.ScReady = true;
                        player.Score += 20;
                    }

                    itemList.Remove(item);
                    break;
                }
            } 
        }

        private void HandleDeath(List<GameObject> list)
        {
            foreach (GameObject obj in list)
            {
                if (obj.Health <= 0)
                {
                    list.Remove(obj);

                    if (obj is Rock)
                     {
                         if (SoundManager.SoundOn)
                            SoundManager.GetSound("explosion").Play(0.03f, 0f, 0f);

                         int chance = rnd.Next(1, 6);
                         if (chance == 5)
                             HandleItemSpawn(obj.Position);

                         float scale = obj.Scale;
                         float pgBuffer = scale * 5;
                         particleGenerator = new ParticleGenerator(TextureManager.GetTexture("rock_2"), obj.Position, 10 + (int)pgBuffer, 20 + (int)pgBuffer, -5 - (int)pgBuffer, 5 + (int)pgBuffer, 5 + (int)pgBuffer, 20 + (int)pgBuffer);
                        pgList.Add(particleGenerator);
                    }                   
                    break;
                }
            }
   

            if (!singlePlayer)
            {
                if (!player1.Alive && player2.Alive)
                    player1.Position = player2.Position;
                if (!player2.Alive && player1.Alive)
                    player2.Position = player1.Position;
            } 
        }

        private void HandleScore()
        {
            if (singlePlayer && !player1.Alive)
            {
                play = false;
                failure = true;
                Highscore.SaveHighScore(name, player1.Score);
            }
            if (!singlePlayer)
            {
                if (!player1.Alive && !player2.Alive)
                {
                    play = false;
                    failure = true;
                    Highscore.SaveHighScore(name, player1.Score + player2.Score);
                }
            } 
        }

        private void UpdateParticleGenerator(GameTime gameTime)
        {        
            foreach (ParticleGenerator pg in pgList)
            {
                pg.Update(gameTime);
                if (pg.GetParticleList.Count == 0)
                {
                    pgList.Remove(pg);
                    break;
                }
            }
        }

        private void DrawParticleGenerator(SpriteBatch spriteBatch)
        {
            foreach (ParticleGenerator pg in pgList)
            {
                pg.Draw(spriteBatch);
            }
        }

        private void HandleObjectCollision(List<GameObject> list, Player player)
        {
            foreach (GameObject obj in list)
            {
                if (CollisionDetection(obj.Hitbox, player.GetHitbox))
                {
                    if (SoundManager.SoundOn)
                        SoundManager.GetSound("playerhit").Play(0.02f, 0f, 0f);
                    player.Health -= obj.Damage;
                    obj.Health = 0;
                    break;
                }
                foreach (GameObject bullet in player.GetBulletList)
                {
                    if (CollisionDetection(obj.Hitbox, bullet.Hitbox))
                    {
                        if (SoundManager.SoundOn)
                            SoundManager.GetSound("objecthit").Play(0.01f, 0f, 0f);

                        obj.Health -= bullet.Damage;
                        obj.PlayerIndex = bullet.PlayerIndex;
                        if (obj.Health <= 0 && obj is Rock)
                        {       
                            player.Score += obj.Points;
                        }
                        player.GetBulletList.Remove(bullet);
                        break;
                    }
                }            
            }
            if (player.Health <= 0)
                player.Alive = false;
        }

        private void ScreenClear(Player player, List<GameObject> list, List<Enemy> enemyList)
        {
            if (player.ScreenClear)
            {
                foreach (GameObject obj in list)
                {
                    if (obj is Rock)
                        obj.Health = 0;                
                }
                foreach (Enemy enemy in enemyList)
                {
                    enemy.Health = 0;
                }
                player.ScreenClear = false;
                player.Score += 200;
            }
        }

        private void HandleEnemyCollision(Player player, List<Enemy> enemyList)
        {
            foreach  (GameObject bullet in player.GetBulletList)
            {
                foreach (Enemy enemy in enemyList)
	            {
		            if (CollisionDetection(bullet.Hitbox, enemy.HitBox))
                    {
                        if (SoundManager.SoundOn)
                            SoundManager.GetSound("playerhit").Play(0.01f, 0f, 0f);
                        enemy.Health -= bullet.Damage;
                        bullet.Health = 0;
                        if (enemy.Health <= 0)
                        {
                            if (SoundManager.SoundOn)
                                SoundManager.GetSound("explosion").Play(0.01f, 0f, 0f);
                            enemy.Alive = false;
                            int chance = rnd.Next(1, 6);
                            if (chance == 5)
                                HandleItemSpawn(enemy.Position);
                            particleGenerator = new ParticleGenerator(TextureManager.GetTexture("rock_2"), enemy.Position, 10, 20, -5, 5, 5, 20);
                            pgList.Add(particleGenerator);
                          
                            enemyList.Remove(enemy);
                            break;

                        }
                    }
	            }
         
            }
        }

        private void HandlePvP(Player player1, Player player2)
        {
            foreach (GameObject obj in player1.GetBulletList)
            {
                if (CollisionDetection(player2.GetHitbox, obj.Hitbox))
                {
                    if (SoundManager.SoundOn)
                        SoundManager.GetSound("playerhit").Play(0.02f, 0f, 0f);
                    player2.Health -= obj.Damage;
                    obj.Health = 0;
                }

                if (player2.Health <= 0)
                    player2.Alive = false;
            }
            if (!singlePlayer)
            {
                foreach (GameObject obj in player2.GetBulletList)
                {
                    if (CollisionDetection(player1.GetHitbox, obj.Hitbox))
                    {
                        if (SoundManager.SoundOn)
                            SoundManager.GetSound("playerhit").Play(0.02f, 0f, 0f);
                        player1.Health -= obj.Damage;
                        obj.Health = 0;
                    }
                    if (player1.Health <= 0)
                        player1.Alive = false;
                }
            }

        }

        private void UpdateBullets(List<GameObject> list1, List<GameObject> list2, GameTime gameTime)
        {
            foreach (GameObject b in list1)
            {
                b.Update(gameTime);
            }
            foreach (GameObject b in list2)
            {
                b.Update(gameTime);
            }
        }

        private void DrawBullets(List<GameObject> list1, List<GameObject> list2, SpriteBatch spriteBatch)
        {
            foreach (GameObject b in list1)
            {
                b.Draw(spriteBatch);
            }
            foreach (GameObject b in list2)
            {
                b.Draw(spriteBatch);
            }
        }

        private void HandleEnemyBulletCollision(Player player, List<Enemy> enemyList)
        {
            foreach (Enemy e in enemyList)
            {
                foreach (Bullet_Enemy b in e.BulletList)
                {
                    if (CollisionDetection(player.GetHitbox, b.Hitbox))
                    {
                        SoundManager.GetSound("playerhit").Play(0.02f, 0f, 0f);
                        player.Health -= b.Damage;
                        b.Alive = false;
                        b.Velocity = new Vector2(0, 0);
                        e.BulletList.Remove(b);
                        break;
                    }
                }
            }
        }

        public Vector2 WorldToScreen(Vector2 world_position)
        {
            return Vector2.Transform(world_position, transform);
        }

        public Vector2 ScreenToWorld(Vector2 screen_position)
        {
            return Vector2.Transform(screen_position, Matrix.Invert(transform));
        }

        private void WithinBounds(Player player)
        {
            Vector2 playerPos = WorldToScreen(player.Position);
            if (playerPos.Y >= 1080)
                player.Alive = false;
            if (playerPos.Y <= 0)
                player.Alive = false;
            if (playerPos.X <= 0)
                player.Alive = false;
            if (playerPos.X >= 1920)
                player.Alive = false;
            if (!player.Alive)
                player.Health = 0;
        }

        private void HanldePlayerHealth(Player player, SpriteBatch spriteBatch)
        {

            if (player.PlayerIndex == PlayerIndex.One)
            {               

                for (int i = 0; i < player.Health / 10; i++)
                {
                    spriteBatch.Draw(TextureManager.GetTexture("HpBar"), world.Player1Hp[i], Color.White);
                }
            }


            if (player.PlayerIndex == PlayerIndex.Two)
            {

                for (int i = 0; i < player.Health / 10; i++)
                {
                    spriteBatch.Draw(TextureManager.GetTexture("HpBar"), world.Player2Hp[i], Color.White);
                }
            }
     
        }

        private void CheckObjectsWithinBounds(List<GameObject> list)
        {
            foreach (GameObject obj in list)
            {
                Vector2 pos = WorldToScreen(obj.Position);
                if (pos.Y >= 1080)
                {
                    list.Remove(obj);
                    break;
                }
                if (obj is Bullet_Standard || obj is Bullet_Blue || obj is Bullet_Red)
                {
                    if (pos.Y <= 0)
                    {
                        list.Remove(obj);
                        break;
                    }
       
                }
                    
                if (pos.X <= 0)
                {
                    list.Remove(obj);
                    break;
                }
                   
                if (pos.X >= 1920)
                {
                    list.Remove(obj);
                    break;
                }              
            }
        }

        private void CheckItemsWithinBounds(List<Item> list)
        {
            foreach (Item obj in list)
            {
                Vector2 pos = WorldToScreen(obj.Position);
                if (pos.Y >= 1080)
                {
                    list.Remove(obj);
                    break;
                }

                if (pos.X <= 0)
                {
                    list.Remove(obj);
                    break;
                }

                if (pos.X >= 1920)
                {
                    list.Remove(obj);
                    break;
                }
            }
        }

        private void EnemyWithinBounds(List<Enemy> list)
        {
            foreach (Enemy enemy in list)
            {
                Vector2 enemyPos = WorldToScreen(enemy.Position);
                if (enemyPos.Y >= 1080)
                {
                    enemy.Alive = false;

                    break;
                }
               
                if (enemyPos.Y <= 0)
                {
                    enemy.Alive = false;
                    break;
                }
                   
                if (enemyPos.X <= 100)
                {
                    enemy.Alive = false;
                    break;
                }
                if (enemyPos.X >= 1800)
                {
                    enemy.Alive = false;
                    break;
                }
            }
        }     

        public void Update(GameTime gameTime)
        {
            HandleFailure();

            if (play)
            {
                camera.Move(new Vector2(0, -2), false);
                transform = camera.get_transformation();
                player1.Update(gameTime);
                ItemCollision(player1);
                HandleEnemyCollision(player1, world.EnemyList);
                HandleEnemyBulletCollision(player1, world.EnemyList);
                EnemyWithinBounds(world.EnemyList);
                bulletList1 = player1.GetBulletList;
                ScreenClear(player1, world.GetObjList, world.EnemyList);

                HandleScore();
                CheckObjectsWithinBounds(bulletList1);

                if (!singlePlayer)
                {
                    ScreenClear(player2, world.GetObjList, world.EnemyList);
                    player2.Update(gameTime);
                    HandleEnemyCollision(player2, world.EnemyList);
                    HandleEnemyBulletCollision(player2, world.EnemyList);
                    ItemCollision(player2);
                    bulletList2 = player2.GetBulletList;
                    CheckObjectsWithinBounds(bulletList2);

                }
                
                UpdateBullets(bulletList1, bulletList2, gameTime);

                if (pvp && !singlePlayer)
                    HandlePvP(player1, player2);

                CheckObjectsWithinBounds(world.GetObjList);
                CheckItemsWithinBounds(itemList);
                WithinBounds(player1);
                

                if (!singlePlayer)
                WithinBounds(player2);

                if (player1.Alive)
                    HandleObjectCollision(world.GetObjList, player1);

                if (!singlePlayer)
                    if (player2.Alive)
                        HandleObjectCollision(world.GetObjList, player2);
               
                UpdateParticleGenerator(gameTime);


                if (!singlePlayer)
                {
                    if (!player1.Alive)
                        world.Update(gameTime, player2.Position, player2.Position, worldPos);

                    if (!player2.Alive)
                        world.Update(gameTime, player1.Position, player1.Position, worldPos);

                    else if (player1.Alive && player2.Alive)
                        world.Update(gameTime, player1.Position, player2.Position, worldPos);                 
                }
                else
                    world.UpdateSinglePlayer(gameTime, player1.Position);

                HandleDeath(world.GetObjList);
                HandleDeath(bulletList1);

                if (!singlePlayer)
                HandleDeath(bulletList2);
            }      
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.End();            
            spriteBatch.Begin();
            world.DrawBackground(spriteBatch);
            HanldePlayerHealth(player1, spriteBatch);
            HanldePlayerHealth(player2, spriteBatch);
            spriteBatch.End();

            if (play)
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, transform);
                world.DrawObjects(spriteBatch);

                DrawItems(spriteBatch);

                player1.Draw(spriteBatch);
                if (!singlePlayer)
                player2.Draw(spriteBatch);

                DrawParticleGenerator(spriteBatch);
                DrawBullets(bulletList1, bulletList2, spriteBatch);
                spriteBatch.End();
            }

            spriteBatch.Begin();
            spriteBatch.DrawString(FontManager.GetFont("menu_font"), player1.Score.ToString(), new Vector2(50, 100), Color.White);
            if (player1.ScReady)
                spriteBatch.DrawString(FontManager.GetFont("menu_font"), "ApoCannon READY", new Vector2(50, 150), Color.Green);
            else
                spriteBatch.DrawString(FontManager.GetFont("menu_font"), "ApoCannon EMPTY", new Vector2(50, 150), Color.White);

		 
	

            if (!singlePlayer)
            {
                spriteBatch.DrawString(FontManager.GetFont("menu_font"), player2.Score.ToString(), new Vector2(1850, 100), Color.White);
                if (player2.ScReady)
                    spriteBatch.DrawString(FontManager.GetFont("menu_font"), "ApoCannon READY", new Vector2(1688, 150), Color.Green);
                else
                    spriteBatch.DrawString(FontManager.GetFont("menu_font"), "ApoCannon EMPTY", new Vector2(1688, 150), Color.White);

            }
            if (failure)
            {
                DrawFailure(spriteBatch);
            }
            spriteBatch.End();
        }       
    }
}
