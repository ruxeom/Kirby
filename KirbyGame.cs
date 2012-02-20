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
        StateManager Statemanager;
        Rectangle Viewport;
        Character Player;
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

            GameLoader loader = new GameLoader(this.Content);
            Statemanager = new StateManager();

            this.Stage = loader.LoadStage("C:\\Users\\Fofo\\Documents\\Visual Studio 2010\\Projects\\Kirby\\Kirby\\KirbyContent\\GamePropeties\\Stage1.txt", this.Viewport);
            Player = new Character(Stage.StartPosition, Content.Load<Texture2D>("Sprites\\Kirby\\KirbyStanding"));
            Player.Move(0, -Player.Height);

        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            
            if (Keyboard.GetState(PlayerIndex.One).GetPressedKeys().Contains<Keys>(Keys.Escape))
                this.Exit();

            Statemanager.ManagePlayerStates(Player, Keyboard.GetState(PlayerIndex.One).GetPressedKeys());
            Statemanager.ManageFloorStates(Stage.Terrain, Player, Viewport);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightGreen);
            Spritebatch.Begin();
            
            DrawStage();
            DrawPlayer();
            Spritebatch.End();

            base.Draw(gameTime);
        }

        public void DrawStage()
        {
            foreach (GameObject terrain in Stage.Terrain)
                Spritebatch.Draw(terrain.Sprite, terrain.Position, Color.White);
        }

        public void DrawPlayer()
        {
            Spritebatch.Draw(Player.Sprite, Player.Position, Color.White);
        }
    }
}
