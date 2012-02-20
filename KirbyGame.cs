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

    public class KirbyGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager Graphics;
        SpriteBatch Batch;
        StateManager Manager;
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
            Batch = new SpriteBatch(GraphicsDevice);
            Viewport = new Rectangle(
               Graphics.GraphicsDevice.Viewport.X,
               Graphics.GraphicsDevice.Viewport.Y,
               Graphics.GraphicsDevice.Viewport.Width,
               Graphics.GraphicsDevice.Viewport.Height);

            GameLoader loader = new GameLoader(this.Content);
            Manager = new StateManager();

            this.Stage = loader.LoadStage("GameProperties\\Stage1.txt", this.Viewport);
            Player = new Character(Stage.StartPosition, Content.Load<Texture2D>("Sprites\\Kirby\\KirbyStandingR"));
            Player.Move(0, -Player.Height);
            Player.MaxHealth = Player.CurrentHealth = 100;
            Player.ContactDamage = 10;

        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState(PlayerIndex.One).GetPressedKeys().Contains<Keys>(Keys.Escape))
                this.Exit();

            Manager.ManagePlayerStates(Player, Keyboard.GetState(PlayerIndex.One).GetPressedKeys(), Stage.Height);
            Manager.ManageEnemyStates(Stage.Terrain, Stage.Enemies, Player);
            Manager.ManageFloorStates(Stage.Terrain, Player, ref Viewport);
            Manager.ManageViewPortStates(Stage, Player, ref Viewport);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightGreen);
            Batch.Begin();
            
            DrawStage();
            DrawEnemies();
            DrawPlayer();
            DrawStats();
            Batch.End();

            base.Draw(gameTime);
        }

        public void DrawStage()
        {
            foreach (GameObject terrain in Stage.Terrain)
                Batch.Draw(terrain.Sprite, Vector2.Add(terrain.Position, Stage.ScreenDisplacement), Color.White);
        }

        public void DrawPlayer()
        {
            Batch.Draw(Player.Sprite, Vector2.Add(Player.Position,Stage.ScreenDisplacement), 
                Color.White);
        }

        public void DrawEnemies()
        {
            foreach (Enemy enemy in Stage.Enemies)
                if(enemy.HasState(State.ALIVE))
                    Batch.Draw(enemy.Sprite, Vector2.Add(enemy.Position, Stage.ScreenDisplacement), 
                        Color.White);
        }

        public void DrawStats()
        {
            Batch.DrawString(Content.Load<SpriteFont>("elFont"), Player.Center.X.ToString() + "," + Player.Center.Y.ToString(), new Vector2(0, 0), Color.Blue);
            Batch.DrawString(Content.Load<SpriteFont>("elFont"), Viewport.Center.X.ToString(), new Vector2(0, 20), Color.Blue);
            Batch.DrawString(Content.Load<SpriteFont>("elFont"), Stage.ScreenDisplacement.X.ToString(), new Vector2(0, 40), Color.Blue);
            Batch.DrawString(Content.Load<SpriteFont>("elFont"), Player.CurrentHealth.ToString(), new Vector2(0, 60), Color.Blue);
        }
    }
}
