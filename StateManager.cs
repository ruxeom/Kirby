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

        public void ManagePlayerStates(Character player, Keys[] pressedkeys)
        {
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
                        dy -= 10;
                }
                if (player.HasState(State.STANDING))
                {
                    player.MaxHeight = ((int)player.Position.Y - 250);
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

        public void ManageFloorStates(List<GameObject> terrain, Character player, Rectangle viewport)
        {
            foreach (GameObject tile in terrain)
            {
                //if the tile is on-screen
                Vector2 playercenter = player.Center;
                Vector2 floorcenter;
                if (viewport.Intersects(tile.BoundingBox))
                    if (player.BoundingBox.Intersects(tile.BoundingBox))
                    {
                        floorcenter = tile.Center;
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

        public void ManageJumpStates()
        {
 
        }

    }
}
