using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Kirby
{
    class Sprite
    {
        public Vector2 Position = new Vector2();
        private Texture2D mSpriteTexture;           
        public Rectangle size;                      //Size of the Sprite
        public float scale = 1.0f;                  //Scale of the Sprite (Ex: 2.0f is two times bigger)
        public string AssetName;                    //Active name of the Sprite's texture
        private float mScale = 1.0f;

        //When the scale is modified throught he property, the Size of the 
        //sprite is recalculated with the new scale applied.
        public float Scale
        {
            get { return mScale; }
            set
            {
                mScale = value;

                //Recalculate the Size of the Sprite with the new scale
                size = new Rectangle(0, 0, (int)(mSpriteTexture.Width * Scale), (int)(mSpriteTexture.Height * Scale));
            }
        }

        //Load the texture for the sprite
        public void LoadContent(ContentManager theContentManager, string theAssetName)
        {
            mSpriteTexture = theContentManager.Load<Texture2D>(theAssetName);
            AssetName = theAssetName;
            size = new Rectangle(0, 0, (int)(mSpriteTexture.Width * scale), (int)(mSpriteTexture.Height * scale));
        }

        //Update the Sprite and change it's position based on the passed in speed, direction and elapsed time.
        public void Update(GameTime theGameTime, Vector2 theSpeed, Vector2 theDirection)
        {
            Position += theDirection * theSpeed * (float)theGameTime.ElapsedGameTime.TotalSeconds;
        }

        //Draw the sprite to the screen
        public void Draw(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(mSpriteTexture, Position,
                new Rectangle(0, 0, mSpriteTexture.Width, mSpriteTexture.Height), Color.White,
                0.0f, Vector2.Zero, scale, SpriteEffects.None, 0);
        }

    }
}
