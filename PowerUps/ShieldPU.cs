using Mogre;
using Mogre.TutorialFramework;
using System;
using System.Collections;

namespace Tutorial
{
    class ShieldPU : PowerUp
    {
        Entity pUEntity;
        SceneNode pUNode;

        public ShieldPU(SceneManager mScenemgr, Stat Shield) : base(mScenemgr)
        {
            this.Stat = Shield;
            this.increase = 50;
        }

        protected override void LoadModel()
        {
            base.LoadModel();
            pUEntity = mSceneMgr.CreateEntity("PowerCells.mesh");
            pUNode = mSceneMgr.CreateSceneNode();
            pUNode.AttachObject(pUEntity);
            pUNode.Scale(2f, 2f, 2f);
            pUNode.Translate(0, -15, 0);
            mSceneMgr.RootSceneNode.AddChild(pUNode);
            pUEntity.GetMesh().BuildEdgeList();
        }
        public SceneNode PUNode
        {
            get { return pUNode; }
        }

    }
}
