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
            /*if (player.HasState(State.JUMPING))
            {
                if(
            }
                
            if (pressedkeys.Contains<Keys>(Keys.Up))
            {
 
            }
            if (player.HasState(State.STANDING))
                dy += 3;*/
            player.Move(dx, dy);
        }

        public void ManageFloorStates(List<GameObject> terrain, Character player, Rectangle viewport)
        {
            foreach (GameObject tile in terrain)
            {
                //if the tile is on-screen
                if (viewport.Intersects(tile.BoundingBox))
                    if (player.BoundingBox.Intersects(tile.BoundingBox))
                    {
 
                    }
            }

        }

        public void ManageJumpStates()
        {
 
        }

    }
}
