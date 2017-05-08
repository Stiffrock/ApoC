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
    static class SoundManager
    {
        static Dictionary<string, SoundEffect> Sounds = new Dictionary<string, SoundEffect>();
        static Dictionary<string, SoundEffectInstance> SoundInstance = new Dictionary<string, SoundEffectInstance>();

        static bool soundOn = true;

        public static bool SoundOn { get { return soundOn; } set { soundOn = value; } }

        public static void AddSoundInstance(string key, SoundEffectInstance soundEffect)
        {
            SoundInstance.Add(key, soundEffect);
        }

        public static SoundEffectInstance GetSoundInstance(string key)
        {
            return SoundInstance[key];
        }


        public static void AddSound(string key, SoundEffect soundEffect)
        {
            Sounds.Add(key, soundEffect);
        }

        public static SoundEffect GetSound(string key)
        {
            return Sounds[key];
        }

    
    }
}
