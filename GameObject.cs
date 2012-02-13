using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Kirby
{
    public class GameObject
    {
        public Vector2 Position;
        public Texture2D Sprite;
        public int ContactDamage;
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
            this.Position.X += dy;
        }

        public GameObject(Vector2 position, Texture2D sprite)
        {
            if(position != null)
                this.Position = position;
            else
                Position.X = Position.Y = 0;
            this.Sprite = sprite;
        }
    }

    public class Character : GameObject
    {
        int MaxHealth;
        int CurrentHealth;
        int Defense;
        //State

        public Character(Vector2 position, Texture2D sprite)
            : base(position, sprite)
        { }

        public void AddState()
        { }

        public bool HasState()
        {
            return false;
        }

        public void RemoveState()
        { }

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
