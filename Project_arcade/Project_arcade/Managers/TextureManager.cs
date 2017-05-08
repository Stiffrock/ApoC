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
    static class TextureManager
    {
        static Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
        public static void AddTexture(Texture2D texture, string name)
        {
            textures.Add(name, texture);
        }

        public static Texture2D GetTexture(string name)
        {
            return textures[name];
        }     
    }

}
