using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Kirby
{
    public class Stage : IStateable
    {
        public List<GameObject> Terrain;
        public List<Enemy> Enemies;
        public List<int> StateList;
        public int Length;
        public int Height;
        public Vector2 ScreenDisplacement;
        public Vector2 StartPosition;

        public Stage()
        {
            Terrain = new List<GameObject>();
            Enemies = new List<Enemy>();
            StateList = new List<int>();
            ScreenDisplacement = new Vector2(0, 0);
        }

        public void AddTerrain(GameObject terrain)
        {
            terrain.AddState(State.SOLID);
            this.Terrain.Add(terrain);
        }

        public void AddEnemy(Enemy enemy)
        {
            enemy.AddState(State.SOLID);
            this.Enemies.Add(enemy);
        }

        public void AddState(int state)
        {
            if (!this.HasState(state))
                StateList.Add(state);
        }

        public bool HasState(int state)
        {
            if (StateList.Contains(state))
                return true;
            return false;
        }

        public void RemoveState(int state)
        {
            StateList.Remove(state);
        }



    }
}
