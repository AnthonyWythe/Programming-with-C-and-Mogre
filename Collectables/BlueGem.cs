using System;
using System.Collections.Generic;
using Mogre;

namespace Tutorial
{
    class BlueGem : Gem
    {
        Entity gemEntity;
        SceneNode gemNode;

        public BlueGem(SceneManager mSceneMgr, Stat score) : base(mSceneMgr, score)
        {
            increase = 10;
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

        }

        public SceneNode GemNode
        {
            get { return gemNode; }
        }
    }
}
