using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;

namespace Tutorial
{
    /// <summary>
    /// This class shows how to assemble a model using multiple meshes as a sub-graph of the scenegraph
    /// </summary>
    class PlayerModel : CharacterModel
    {

        ModelElement mainHull;
        ModelElement powerCells;
        ModelElement sphere;
        ModelElement gun;
        ModelElement model;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mSceneMgr">A reference to the scene graph</param>
        public PlayerModel(SceneManager mSceneMgr)
        {
            this.mSceneMgr = mSceneMgr;

            LoadModelElements();
            AssembleModel();
        }

        /// <summary>
        /// This method loads the nodes and entities needed by the compound model
        /// </summary>
        private void LoadModelElements()
        {

            mainHull = new ModelElement(mSceneMgr, "Main.mesh");
            powerCells = new ModelElement(mSceneMgr, "PowerCells.mesh");
            sphere = new ModelElement(mSceneMgr, "Sphere.mesh");
            model = new ModelElement(mSceneMgr, "");
            gun = new ModelElement(mSceneMgr,"");

        }

        /// <summary>
        /// This method assemble the model attaching the entities to 
        /// each node and appending the nodes to each other
        /// </summary>
        private void AssembleModel()
        {


            mainHull.GameNode.AddChild(sphere.GameNode);
            model.GameNode.AddChild(powerCells.GameNode);
            model.GameNode.AddChild(mainHull.GameNode);
            model.GameNode.AddChild(gun.GameNode);

            mSceneMgr.RootSceneNode.AddChild(model.GameNode);
            model.GameNode.Yaw(180);
        }

        public void DisposeModel()
        {
            mainHull.GameNode.Dispose();
            powerCells.GameNode.Dispose();
            sphere.GameNode.Dispose();
            model.GameNode.Dispose();
        }




    }
}

