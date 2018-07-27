using System;
using System.Collections.Generic;
using Mogre;

namespace Tutorial
{
    class RedGem : Gem
    {
        Entity gemEntity;
        SceneNode gemNode;

        public RedGem(SceneManager mSceneMgr, Stat score) : base(mSceneMgr, score)
        {
            increase = 50;
            LoadModel();
        }

        protected override void LoadModel()
        {
            base.LoadModel();
            gemEntity = mSceneMgr.CreateEntity("Gem.mesh");
            gemNode = mSceneMgr.CreateSceneNode();
            gemNode.AttachObject(gemEntity);
            mSceneMgr.RootSceneNode.AddChild(gemNode);
            gemEntity.GetMesh().BuildEdgeList();
            gemEntity.SetMaterialName("Heart");

        }

        public SceneNode GemNode
        {
            get { return gemNode; }
        }
    }
}
