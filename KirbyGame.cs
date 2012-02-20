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

        /*Sprite Enemy;
        Sprite Enemy2;*/

        public KirbyGame()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            /*Enemy = new Sprite();
            Enemy.scale = 1.0f;

            Enemy2 = new Sprite();
            Enemy2.scale = 1.0f;*/

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

            //Loading and positioning the enemies
            /*Enemy.LoadContent(this.Content, "Sprites\\enemy"); //O tambien a [...]\\Dropbox\\Sprites\\enemy
            Enemy.Position = new Vector2(300, 370);
            Enemy2.LoadContent(this.Content, "Sprites\\enemy");
            Enemy2.Position = new Vector2(550, 220);*/

            // TODO: use this.Content to load your game content here

            GameLoader loader = new GameLoader(this.Content);
            Statemanager = new StateManager();

            this.Stage = loader.LoadStage("C:\\Users\\Fofo\\Documents\\Visual Studio 2010\\Projects\\Kirby\\Kirby\\KirbyContent\\GamePropeties\\Stage1.txt", this.Viewport);
            Player = new Character(Stage.StartPosition, Content.Load<Texture2D>("Sprites\\Kirby\\KirbyInhaling"));
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

            //Enemy.Draw(this.Spritebatch);
            //Enemy2.Draw(this.Spritebatch);
            
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
