using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Kirby
{
    class GameLoader
    {
        PropertyReader PropReader;
        ContentManager Content;

        public GameLoader(ContentManager content)
        {
            PropReader = new PropertyReader();
            Content = content;
        }

        public Stage LoadStage(string propertyfile, Rectangle viewport)
        {
            Dictionary<String,String> props = PropReader.ReadStageProperties(propertyfile);
            Stage stage = new Stage();
            
            stage.Length = Convert.ToInt32(props["length"]) * 50;
            stage.Height = Convert.ToInt32(props["height"]) * 50;

            string[] terrain = props["terrain"].Split(' ');

            for(int i = 0; i < terrain.Length; i++)
            {
                string[] pair = props[terrain[i]].Split(' ');
                for (int j = 0; j < pair.Length; j++)
                {
                    string[] coords = pair[j].Split(',');
                    Vector2 realcoords = GetRealCoordenates(coords, viewport.Bottom);
                    stage.AddTerrain(new GameObject(realcoords, Content.Load<Texture2D>("Sprites\\Terrain\\" + terrain[i])));
                }
            }

            string[] startposition = props["start-position"].Split(',');
            stage.StartPosition = GetRealCoordenates(startposition, viewport.Bottom);
            
            return stage;
        }

        public Vector2 GetRealCoordenates(string[] coords, int height)
        {
            int x = Convert.ToInt32(coords[0]) * 50;
            int y = height - (Convert.ToInt32(coords[1]) + 1) * 50;
            return new Vector2(x, y);
        }
    }
}
