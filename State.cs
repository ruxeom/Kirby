using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kirby
{
    public class State
    {
        public static int STANDING     = 0;
        public static int WALKING      = 1;
        public static int JUMPING      = 2;
        public static int FALLING      = 3;
        public static int TAKINGDAMAGE = 4;
        public static int INVULNERABLE = 5;
        public static int FLOATING     = 6;
        public static int INHALING     = 7;
        public static int SPITTING     = 8;
        public static int ALIVE        = 9;
        public static int DYING       = 10;
    }

    public class Direction
    {
        public static int UP = 11;
        public static int DOWN = 12;
        public static int LEFT = 13;
        public static int RIGHT = 14;
    }
        
}
