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
    public class StateManager 
    {
        public StateManager()
        {
 
        }

        public void ManagePlayerStates(Character player, Keys[] pressedkeys, int stagelimit)
        {
            if (player.HasState(State.DYING))
                return;


            if (player.CurrentHealth <= 0 || player.Position.Y > stagelimit)
            {
                player.CurrentHealth = 0;
                player.AddState(State.ALIVE);
                player.AddState(State.DYING);
                return;
            }

            int dx = 0, dy = 0;
            //Here we manage Horizontal movement
            if (pressedkeys.Contains<Keys>(Keys.Left))
            {
                dx -= 5;
                player.RemoveState(State.FACERIGHT);
                player.AddState(State.FACELEFT);
                player.AddState(State.MOVING);
            }
            else if (pressedkeys.Contains<Keys>(Keys.Right))
            {
                dx += 5;
                player.RemoveState(State.FACELEFT);
                player.AddState(State.FACERIGHT);
                player.AddState(State.MOVING);
            }
            else
                player.RemoveState(State.MOVING);

            //Here we manage vertical movement based on jumping actions
            if (pressedkeys.Contains<Keys>(Keys.Up))
            {
                if (player.HasState(State.JUMPING))
                {
                    if (player.Position.Y < player.MaxHeight)
                    {
                        dy = (int)player.Position.Y - player.MaxHeight;
                        player.RemoveState(State.JUMPING);
                        player.AddState(State.FALLING);
                    }
                    else
                        dy -= 15;
                }
                if (player.HasState(State.STANDING))
                {
                    player.MaxHeight = ((int)player.Position.Y - 200);
                    if (player.MaxHeight < 0)
                        player.MaxHeight = 0;
                    player.RemoveState(State.STANDING);
                    player.AddState(State.JUMPING);
                    dy -= 3;
                }
            }
            else 
            {
                if (player.HasState(State.JUMPING))
                {
                    player.AddState(State.FALLING);
                    player.RemoveState(State.JUMPING);
                }
            }
                                       
            dy += 5;
            player.Move(dx, dy);
        }

        public void ManageEnemyStates(List<GameObject> terrain, List<Enemy> enemies, Character player)
        {
            //TODO: Manage player/enemy interaction and enemy states.
            foreach (Enemy enemy in enemies)
            {
                if (enemy.HasState(State.ALIVE) && player.BoundingBox.Intersects(enemy.BoundingBox))
                {
                    int dx = (int)(player.Center.X - enemy.Center.X);
                    enemy.CurrentHealth -= player.ContactDamage;
                    player.CurrentHealth -= enemy.ContactDamage;

                    enemy.Position.X -= dx;
                    player.Position.X += dx;

                    if (enemy.CurrentHealth <= 0)
                    {
                        enemy.RemoveState(State.ALIVE);
                        enemy.AddState(State.DYING);
                    }
                }
            }
        }

        public void ManageFloorStates(List<GameObject> terrain, Character player, ref Rectangle viewport)
        {            
            foreach (GameObject tile in terrain)
            {
                //Vector2 playercenter = player.Center;
                //Vector2 floorcenter;
                //if the tile is on-screen
                if (viewport.Intersects(tile.BoundingBox))
                    if (player.BoundingBox.Intersects(tile.BoundingBox) && tile.HasState(State.SOLID))
                    {
                        //floorcenter = tile.Center;
                        if (player.HasState(State.FALLING))
                        {
                            player.AddState(State.STANDING);
                            player.RemoveState(State.FALLING);
                        }
                        player.Move(0, (int)(tile.Position.Y - player.Position.Y - player.Height));
                        break;
                    }
            }

        }

        public void ManageViewPortStates(Stage stage, Character player, ref Rectangle viewport)
        {
            if (stage.HasState(State.FACELEFT))
            {
                int dx = (int)(player.Center.X - viewport.Center.X);
                if (dx > 0)
                {
                    stage.ScreenDisplacement.X -= dx;
                    viewport.X += dx;
                    stage.RemoveState(State.FACELEFT);
                }
                else if (player.Position.X < 0)
                    player.Position.X = 0;
            }
            else if (stage.HasState(State.FACERIGHT))
            {
                int dx = (int)(player.Center.X - viewport.Center.X);
                if (dx < 0)
                {
                    stage.ScreenDisplacement.X -= dx;
                    viewport.X += dx;
                    stage.RemoveState(State.FACERIGHT);
                }
                else if (player.Position.X > stage.Length - player.Width)
                    player.Position.X = stage.Length - player.Width;
            }
            else
            {
                if (player.Center.X < viewport.Width / 2)
                {
                    stage.AddState(State.FACELEFT);
                    stage.ScreenDisplacement.X = viewport.X = 0;
                }
                else if (player.Center.X > stage.Length - viewport.Width / 2)
                {
                    stage.AddState(State.FACERIGHT);
                    viewport.X = stage.Length - viewport.Width;
                    stage.ScreenDisplacement.X = viewport.Width - stage.Length;
                }
                else
                {
                    int dx = (int)(player.Center.X - viewport.Center.X);
                    stage.ScreenDisplacement.X -= dx;
                    viewport.X += dx;
                }
            }
        }

    }
}
