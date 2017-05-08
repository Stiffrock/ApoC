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
    static class FontManager
    {
    
        static Dictionary<string, SpriteFont> Fonts = new Dictionary<string, SpriteFont>();

        public static void AddFont(string key, SpriteFont spriteFont)
        {
            Fonts.Add(key, spriteFont);
        }

        public static SpriteFont GetFont(string key)
        {
            return Fonts[key];
        }

    }
}
