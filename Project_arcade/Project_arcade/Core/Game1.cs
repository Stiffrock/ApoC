using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.IO;
using System.Xml.Serialization;
using System.IO.IsolatedStorage;

namespace Project_arcade
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
       // GamePlay gamePlay;
        Menu menu;
        Texture2D bullet_default, rock_default, superdude_spritesheet, bg_1, rock_1, rock_2, rock_3,
            projectile_1, pickup_tex, health_pickup_tex, logo, cloud_1, cloud_2, stars_1, debris_1, debris_2, debris_3, UI, HpBar;
        SpriteFont default_font, menu_font_1;
        SoundEffect pew, powerup, objecthit, explosion, playerhit;
       

#if (!ARCADE)
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
#else
		public override string GameDisplayName { get { return "Project_arcade"; } }
#endif

        public Game1()
        {
#if (!ARCADE)
            graphics = new GraphicsDeviceManager(this);
#endif

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Content.RootDirectory = "Content";
            // TODO: Add your initialization logic here
            base.Initialize();
        }
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
       

        protected override void LoadContent()
        {
#if (!ARCADE)
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
#endif
            bullet_default = Content.Load<Texture2D>("projectile1_sheet");
            bg_1 = Content.Load<Texture2D>("bg_5");
            logo = Content.Load<Texture2D>("Logo_1");
            rock_2 = Content.Load<Texture2D>("rock_2");
            rock_3 = Content.Load<Texture2D>("rock_3");
            debris_1 = Content.Load<Texture2D>("debris_1");
            debris_2 = Content.Load<Texture2D>("debris_2");
            debris_3 = Content.Load<Texture2D>("debris_3");
            pickup_tex = Content.Load<Texture2D>("Health_Pickup2");
            health_pickup_tex = Content.Load<Texture2D>("Health_Pickup3");
            projectile_1 = Content.Load<Texture2D>("projectile_1");
            superdude_spritesheet = Content.Load<Texture2D>("superdude_spritesheet11");
            cloud_1 = Content.Load<Texture2D>("cloud_cluster_1");
            cloud_2 = Content.Load<Texture2D>("cloud_cluster_2");
            stars_1 = Content.Load<Texture2D>("star_foreground");
            rock_default = Content.Load<Texture2D>("rockPlaceholder");
            UI = Content.Load<Texture2D>("Project_arcade_UI3");
            HpBar = Content.Load<Texture2D>("HpBar5");

            pew = Content.Load<SoundEffect>("pew2");
            powerup = Content.Load<SoundEffect>("Pickup2");
            objecthit = Content.Load<SoundEffect>("Object_hit");
            explosion = Content.Load<SoundEffect>("Explosion");
            playerhit = Content.Load<SoundEffect>("playerhit");

            default_font = Content.Load<SpriteFont>("NewSpriteFont");
            menu_font_1 = Content.Load<SpriteFont>("MenuFont");


            FontManager.AddFont("default_font", default_font);
            FontManager.AddFont("menu_font", menu_font_1);

            SoundManager.AddSound("pew", pew);
            SoundManager.AddSound("powerup", powerup);
            SoundManager.AddSound("objecthit", objecthit);
            SoundManager.AddSound("explosion", explosion);
            SoundManager.AddSound("playerhit", playerhit);

            TextureManager.AddTexture(UI, "UI");
            TextureManager.AddTexture(HpBar, "HpBar");
            TextureManager.AddTexture(cloud_1, "cloud_1");
            TextureManager.AddTexture(cloud_2, "cloud_2");
            TextureManager.AddTexture(debris_1, "debris_1");
            TextureManager.AddTexture(debris_2, "debris_2");
            TextureManager.AddTexture(debris_3, "debris_3");
            TextureManager.AddTexture(stars_1, "stars_1");
            TextureManager.AddTexture(bullet_default, "bullet_default");
            TextureManager.AddTexture(logo, "Logo_1");
            TextureManager.AddTexture(pickup_tex, "pickup");
            TextureManager.AddTexture(health_pickup_tex, "health_pickup");
            TextureManager.AddTexture(rock_default, "rock_default");
            TextureManager.AddTexture(superdude_spritesheet, "sd_spritesheet");
            TextureManager.AddTexture(bg_1, "bg_1");
            TextureManager.AddTexture(projectile_1, "projectile_1");
            TextureManager.AddTexture(rock_2, "rock_2");
            TextureManager.AddTexture(rock_3, "rock_3");
            

            menu = new Menu();

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
#if (!ARCADE)
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
#endif
            // TODO: Add your update logic here
            menu.Update(gameTime);
            //if (menu.Start)
             //   gamePlay.Update(gameTime);




            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            menu.Draw(spriteBatch);
            spriteBatch.End();
           // if (menu.Start)
          //   gamePlay.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
