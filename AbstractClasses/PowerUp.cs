using System;
using Mogre;
using PhysicsEng;

namespace Tutorial
{
    abstract class PowerUp:Collectable
    {
        protected Stat stat;

        public Stat Stat
        {
            set { stat = value; }
        }

        protected PowerUp(SceneManager mSceneMgr)
        {
            this.mSceneMgr = mSceneMgr;
            LoadModel();
        }

        protected int increase;
        virtual protected void LoadModel() { }

        public override void Update(FrameEvent evt)
        {
            
        }
    }
}
