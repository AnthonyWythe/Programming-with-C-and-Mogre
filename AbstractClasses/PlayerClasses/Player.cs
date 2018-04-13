using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;

namespace Tutorial
{
    class Player : Character
    {


        public Player(SceneManager mSceneMgr)
        {
            this.model = new PlayerModel(mSceneMgr);
            this.controller = new PlayerController(this);
            this.stats = new PlayerStats();
            
            
        }

        private void Update(FrameEvent evt)
        {
            model.Animate(evt);
            controller.Update(evt);
        }

        private void Shoot()
        {
           
        }

    }
}
