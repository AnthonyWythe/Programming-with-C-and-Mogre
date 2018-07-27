using Mogre;
using Mogre.TutorialFramework;
using System;
using System.Collections;

namespace Tutorial
{
    class LifePU : PowerUp
    {
        Entity pUEntity;
        SceneNode pUNode;

        public LifePU(SceneManager mScenemgr, Stat Lives) : base(mScenemgr)
        {
            this.Stat = Lives;
            this.increase = 1;
        }

        protected override void LoadModel()
        {
            base.LoadModel();
            pUEntity = mSceneMgr.CreateEntity("Heart.mesh");
            pUNode = mSceneMgr.CreateSceneNode();
            pUNode.Scale(4f, 4f, 4f);
            pUNode.AttachObject(pUEntity);
            mSceneMgr.RootSceneNode.AddChild(pUNode);

            pUEntity.GetMesh().BuildEdgeList();

            pUEntity.SetMaterialName("Heart");
        }
        public SceneNode PUNode
        {
            get { return pUNode; }
        }

    }
}

