﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;

namespace Tutorial
{
    class PlayerController : CharacterController
    {
        public PlayerController(Character player)
        {
            character = player;
            speed = 100;

        }

        private void MovementControl(FrameEvent evt)
        {
            Vector3 move;
            move = new Vector3();
            move = Vector3.ZERO;

            if (forward == true)
            {
                move += character.Model.Forward;
            }
            if (backward == true)
            {
                move -= character.Model.Forward;
            }
            if (left == true)
            {
                move += character.Model.Left;
            }
            if (right == true)
            {
                move -= character.Model.Left;
            }

            move = move.NormalisedCopy * speed;

            if (accellerate == true)
            {
                move = move * 2;
            }

            if(move != Vector3.ZERO)
            {
                character.Move(move * evt.timeSinceLastEvent);
            }

        }

        private void MouseControls()
        {
            character.Model.GameNode.Yaw(Mogre.Math.AngleUnitsToRadians(angles.y));
        }

        private void ShootingControls()
        {

        }
    }
}
