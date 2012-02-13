using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Kirby
{
    class Stage
    {
        public List<GameObject> Terrain;
        public List<Enemy> Enemies;
        public int Length;
        public int Height;
        public Vector2 StartPosition;
        
        public Stage()
        {
            Terrain = new List<GameObject>();
            Enemies = new List<Enemy>();
            
        }



    }
}
