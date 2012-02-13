using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Kirby
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class KirbyGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager Graphics;
        SpriteBatch Spritebatch;
        Rectangle Viewport;
        GameObject Player;
        Stage Stage;

        public KirbyGame()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            Spritebatch = new SpriteBatch(GraphicsDevice);
            Viewport = new Rectangle(
               Graphics.GraphicsDevice.Viewport.X,
               Graphics.GraphicsDevice.Viewport.Y,
               Graphics.GraphicsDevice.Viewport.Width,
               Graphics.GraphicsDevice.Viewport.Height);

            // TODO: use this.Content to load your game content here
            Player = new GameObject(new Vector2(0, 0), Content.Load<Texture2D>("Sprites\\Terrain\\Grass"));

            GameLoader loader = new GameLoader(this.Content);
            this.Stage = loader.LoadStage("C:\\Users\\Fofo\\Documents\\Visual Studio 2010\\Projects\\Kirby\\Kirby\\KirbyContent\\GamePropeties\\Stage1.txt", this.Viewport);

        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();


            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SkyBlue);
            Spritebatch.Begin();
            
            DrawStage();
            Spritebatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        public void DrawStage()
        {
            foreach (GameObject terrain in Stage.Terrain)
                Spritebatch.Draw(terrain.Sprite, terrain.Position, Color.White);
        }
    }
}
