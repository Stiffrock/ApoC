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
    /*Tripple Shooter*/
    class Weapon_TS : Weapon
    {
        private Bullet_Standard bullet_standard;
        private List<GameObject> bulletList;

        public Weapon_TS(Texture2D texture)
        {
            this.texture = texture;
            this.key = "Weapon_TS";
            this.rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }



        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

    }
}
