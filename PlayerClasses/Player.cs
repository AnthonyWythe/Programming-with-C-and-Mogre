using Mogre;
using Mogre.TutorialFramework;
using System;
using System.Collections;

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

        public override void Update(FrameEvent evt)
        {
            model.Animate(evt);
            controller.Update(evt);
        }

        public override void Shoot()
        {
           
        }

    }
}
