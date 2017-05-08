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

    //Race spel där man ska försöka döda varandra, 
    //släppa bomber eller hinder medans man undviker hinder. Spelaren som först nuddar södra delen av skärmen dör
    class Player
    {
        #region Member variables
        private Texture2D texture;
        private Vector2 position,velocity;
        private Rectangle hitBox, srcRec;
        private bool fire, alive, ready;
        private bool weapon_standard, weapon_TS, pickup_blue, pickup_red, soundButton, screenClear, scReady;
        private int health, recCount, score;
        private GameObject bullet, bullet1, bullet2;
        private List<GameObject> bulletList;
        private SoundEffect pew;
        private List<Item> itemList;
        private PlayerIndex playerIndex;
        private Color color;
        private float spriteTimer, spriteInterval, fireTimer, fireCD;
        private enum Direction { Up, Down, Left, Right, UpLeft, UpRight, DownLeft, DownRight }
        private Direction currentDirection;
        private enum EquippedWeapons { Standard, Tripple }
        private EquippedWeapons currentWeapon;
        #endregion

        #region Properties
        public Vector2 Position { get { return position; } set { position = value; } }
        public PlayerIndex PlayerIndex { get { return playerIndex; } set { playerIndex = value; } }
        public bool Weapon_TS { get { return weapon_TS; } set { weapon_TS = value; } }
        public bool Pickup_Blue { get { return pickup_blue; } set { pickup_blue = value; } }
        public bool Pickup_Red { get { return pickup_red; } set { pickup_red = value; } }
        public int Score { get { return score; } set { score = value; } }
        public List<GameObject> GetBulletList { get { return bulletList; } }
        public List<Item> GetItemList { get { return itemList; } }
        public Rectangle GetHitbox { get { return hitBox; } }
        public int Health { get { return health; } set { health = value; } }
        public bool Alive { get { return alive; } set { alive = value; } }
        public bool ScreenClear { get { return screenClear; } set { screenClear = value; } }
        public bool ScReady { get { return scReady; } set { scReady = value; } }
        #endregion


        public Player(Texture2D texture, Vector2 position, bool playerOne)
        {
            this.texture = texture;
            this.position = position;
            srcRec = new Rectangle(0, 25, 50, 50);
            hitBox = new Rectangle((int)position.X, (int)position.Y, srcRec.Width, srcRec.Height);
            bulletList = new List<GameObject>();
            itemList = new List<Item>();
            velocity = new Vector2(0, 0);
            health = 100;
            spriteTimer = 0;
            spriteInterval = 100;
            fireCD = 100;
            fireTimer = 0;
            score = 0;
            alive = true;
            weapon_standard = true;
            weapon_TS = false;
            scReady = true;

            if (playerOne)
            {
                playerIndex = PlayerIndex.One;
                color = Color.White;
            }
            else
            {
                playerIndex = PlayerIndex.Two;
                color = Color.OrangeRed;
            }
        }

        private void HandleWeapon()
        {
            if (weapon_TS)
            {
                currentWeapon = EquippedWeapons.Tripple;
                weapon_standard = false;
            }

            else
                currentWeapon = EquippedWeapons.Standard;

            /*if (weapon_standard)
                currentWeapon = EquippedWeapons.Standard;*/
        }
            

        private void HandleInput()
        {
            if (InputHandler.IsButtonDown(playerIndex, PlayerInput.Up))
            {
                currentDirection = Direction.Up;
                velocity = new Vector2(0, -1);
            }

            if (InputHandler.IsButtonDown(playerIndex, PlayerInput.Down))
            {
              //  currentDirection = Direction.Up;
                velocity = new Vector2(0, 1);
            }

            if (InputHandler.IsButtonDown(playerIndex, PlayerInput.Left))
            {
                currentDirection = Direction.Left;
                velocity = new Vector2(-1, 0);
            }              

             if (InputHandler.IsButtonDown(playerIndex, PlayerInput.Right))
            {
                currentDirection = Direction.Right;
                velocity = new Vector2(1, 0);
            }

             if (InputHandler.IsButtonDown(playerIndex, PlayerInput.Down) && InputHandler.IsButtonDown(playerIndex, PlayerInput.Left))
             {
                 currentDirection = Direction.Left;
                 velocity = new Vector2(-1, 1);
             }
             if (InputHandler.IsButtonDown(playerIndex, PlayerInput.Down) && InputHandler.IsButtonDown(playerIndex, PlayerInput.Right))
             {
                 currentDirection = Direction.Right;
                 velocity = new Vector2(1, 1);
             }

            if (InputHandler.IsButtonDown(playerIndex, PlayerInput.Up) && InputHandler.IsButtonDown(playerIndex, PlayerInput.Left))
            {
                currentDirection = Direction.Left;
                velocity = new Vector2(-1, -1);
            }

            if (InputHandler.IsButtonDown(playerIndex, PlayerInput.Up) && InputHandler.IsButtonDown(playerIndex, PlayerInput.Right))
            {
                currentDirection = Direction.Right;
                velocity = new Vector2(1, -1);
            }



            if ((InputHandler.IsButtonUp(playerIndex, PlayerInput.Up) && (InputHandler.IsButtonUp(playerIndex, PlayerInput.Down) 
                && (InputHandler.IsButtonUp(playerIndex, PlayerInput.Left) && (InputHandler.IsButtonUp(playerIndex, PlayerInput.Right))))))
                velocity = new Vector2(0, 0);



            if (InputHandler.IsButtonDown(playerIndex, PlayerInput.Red))
                if (ready)
                {
                    fire = true;
                    ready = false;
                }
                else
                    fire = false;


            if (InputHandler.IsButtonDown(playerIndex, PlayerInput.Blue))
                if (scReady)
                {
                    screenClear = true;
                    scReady = false;
                }


            if (InputHandler.IsButtonUp(playerIndex, PlayerInput.Green))
                    soundButton = false;
  
 
             if (InputHandler.IsButtonDown(playerIndex, PlayerInput.Green) && soundButton == false)
             {
                 soundButton = true;
                 if (SoundManager.SoundOn)
                 {
                     SoundManager.SoundOn = false;
                 }
                 else
                 {
                     SoundManager.SoundOn = true;
                 }
             }
                 
        }
        
        private void UpdatePosition()
        {
            position += velocity * 10;
            hitBox.X += (int)velocity.X * 10;
            hitBox.Y += (int)velocity.Y *10;
        }


        private void HandleAnimation(GameTime gameTime)
        {
          spriteTimer -= gameTime.ElapsedGameTime.Milliseconds;

          if (spriteTimer <= 0)
          {
              if (currentDirection == Direction.Up)
              {
                  srcRec.Y = 25;
                  spriteTimer = spriteInterval;
                  srcRec.X = (recCount % 4) * 50 + 25;
                  recCount++;
              }
              if (currentDirection == Direction.Right)
              {
                  srcRec.Y = 95;
                  spriteTimer = spriteInterval;
                  srcRec.X = (recCount % 4) * 50 + 25;
                  recCount++;
              }
              if (currentDirection == Direction.Left)
              {
                  srcRec.Y = 165;
                  spriteTimer = spriteInterval;
                  srcRec.X = (recCount % 4) * 50 + 25;
                  recCount++;
              }           
          }
        }

        private void HandleBullet()
        {
            if (fire)
            {
                if (SoundManager.SoundOn)
                    SoundManager.GetSound("pew").Play(0.01f, 0f, 0f);

                
                Vector2 bulletVelocity = new Vector2(0, 0);
                Vector2 bulletOffset = new Vector2(0, 0);
                Vector2 bulletPos = new Vector2(0, 0);
                Vector2 specialOffset = new Vector2(0, 0);
                int direction = 0;

                switch (currentDirection)
                {
                    case Direction.Up:
                        direction = 3;
                        break;
                    case Direction.Down:
                        direction = 3;
                        break;
                    case Direction.Left:
                        direction = 2;
                        break;            
                    case Direction.Right:
                        direction = 1;
                        break;                                                  
                    default:
                        break;
                }

                switch (currentWeapon)
                {
                    case EquippedWeapons.Standard:
                        if (pickup_blue)
                        bullet = new Bullet_Blue(position, direction, playerIndex);

                        if (pickup_red)
                            bullet = new Bullet_Red(position, direction, playerIndex);

                        if (!pickup_blue && !pickup_red)
                            bullet = new Bullet_Standard(position, direction, playerIndex);
                        break;

                    case EquippedWeapons.Tripple:
                        if (pickup_blue)
                        {
                            bullet = new Bullet_Blue(position, direction, playerIndex);
                            bullet1 = new Bullet_Blue(position, direction, playerIndex);
                            bullet2 = new Bullet_Blue(position, direction, playerIndex);
                        }
                        if (pickup_red)
                        {
                            bullet = new Bullet_Red(position, direction, playerIndex);
                            bullet1 = new Bullet_Red(position, direction, playerIndex);
                            bullet2 = new Bullet_Red(position, direction, playerIndex);
                        }

                        if (!pickup_blue && !pickup_red)
                        {
                            bullet = new Bullet_Standard(position, direction, playerIndex);
                            bullet1 = new Bullet_Standard(position, direction, playerIndex);
                            bullet2 = new Bullet_Standard(position, direction, playerIndex);
                        }
        
  

                      //  bullet = new Bullet_TS(position, direction, playerIndex);
                        break;
                    default:
                        break;
                }

                switch (currentDirection)
                    {
                        case Direction.Up:
                            {
                                bulletVelocity = new Vector2(0, -bullet.Velocity.Y);
                                specialOffset = new Vector2(25, 0);
                                bulletOffset = new Vector2(0, -bullet.Hitbox.Height);                               
                                break;
                            }
                            
                        case Direction.Down:
                            {
                                specialOffset = new Vector2(25, 0);
                                bulletVelocity = new Vector2(0, -bullet.Velocity.Y);
                                bulletOffset = new Vector2(0, -bullet.Hitbox.Height);
                                break;
                            }

                        case Direction.Left:
                            {
                                specialOffset = new Vector2(0, 25);
                                bulletVelocity = new Vector2(-bullet.Velocity.X, 0);
                                bulletOffset = new Vector2(-bullet.Hitbox.Width, 0);
                                break;
                            }

                        case Direction.Right:
                            {
                                specialOffset = new Vector2(0, 25);
                                bulletVelocity = new Vector2(bullet.Velocity.X, 0);
                                bulletOffset = new Vector2(bullet.Hitbox.Width, 0 );                         
                                break;
                            }
                        default:
                            break;
                    }
        
                    bullet.Position += bulletOffset;
                    bullet.Hitbox = new Rectangle((int)bullet.Position.X, (int)bullet.Position.Y, bullet.Hitbox.Width, bullet.Hitbox.Height);   
                    bullet.Velocity = bulletVelocity;

                    if (currentWeapon == EquippedWeapons.Tripple)
                    {
                        bullet1.Position += bulletOffset;
                        bullet1.Position += specialOffset;
                        bullet1.Hitbox = new Rectangle((int)bullet.Position.X, (int)bullet.Position.Y, bullet.Hitbox.Width+20, bullet.Hitbox.Height);
                        bullet1.Velocity = bulletVelocity;

                        bullet2.Position += bulletOffset;
                        bullet2.Position -= specialOffset;
                        bullet2.Hitbox = new Rectangle((int)bullet.Position.X, (int)bullet.Position.Y, bullet.Hitbox.Width+20, bullet.Hitbox.Height);
                        bullet2.Velocity = bulletVelocity;

                        bulletList.Add(bullet1);
                        bulletList.Add(bullet2);
                    }            
                    bulletList.Add(bullet);
                    fire = false;
                }
            }

        private void HandleFireDelay(GameTime gameTime)
        {
           fireTimer += gameTime.ElapsedGameTime.Milliseconds;

           if (!ready)
           {
               if (fireTimer >= fireCD)
               {
                   ready = true;
                   fireTimer = 0;
               }
           }                  
        }
           
        public void Update(GameTime gameTime)
        {
            if (alive)
            {
                HandleWeapon();
                HandleFireDelay(gameTime);
                HandleAnimation(gameTime);
                UpdatePosition();
                HandleInput();
                HandleBullet();
            }   
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (alive)
                spriteBatch.Draw(texture, position, srcRec, color);

        }
    }
}
