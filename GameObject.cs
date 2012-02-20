using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace Kirby
{
    public class GameObject : IStateable
    {
        public Vector2 Position;
        public Texture2D Sprite;
        public int ContactDamage;
        public List<int> StateList;
        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle(
                    (int)Position.X,
                    (int)Position.Y,
                    Sprite.Width,
                    Sprite.Height);
            }
        }

        public int Width
        {
            get 
            {
                if(this.Sprite != null)
                    return Sprite.Width;
                return 0;
            }
        }

        public int Height
        {
            get
            {
                if(this.Sprite != null)
                    return Sprite.Height;
                return 0;
            }
        }

        public void Move(int dx, int dy)
        {
            this.Position.X += dx;
            this.Position.Y += dy;
        }

        public GameObject(Vector2 position, Texture2D sprite)
        {
            if(position != null)
                this.Position = position;
            else
                Position.X = Position.Y = 0;
            this.Sprite = sprite;
            this.StateList = new List<int>();
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

    public class Character : GameObject
    {
        int MaxHealth;
        int CurrentHealth;
        int Defense;
        int MaxHeight;

        public Character(Vector2 position, Texture2D sprite)
            : base(position, sprite)
        {
            AddState(State.STANDING);
        }        

    }

    public class Enemy : Character
    {
        //AI

        public Enemy(Vector2 position, Texture2D sprite)
            : base(position, sprite)
        { }

        public void ExecuteAI()
        { }
    }
}
