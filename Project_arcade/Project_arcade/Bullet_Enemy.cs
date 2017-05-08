using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_arcade
{
    class Bullet_Enemy : GameObject
    {
        protected bool alive;

        public bool Alive { get { return alive; } set { alive = value; } }
    }
}
