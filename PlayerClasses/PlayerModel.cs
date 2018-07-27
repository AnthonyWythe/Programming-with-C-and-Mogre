using Mogre;
using Mogre.TutorialFramework;
using System;
using System.Collections;
using PhysicsEng;

namespace Tutorial
{
    /// <summary>
    /// This class shows how to assemble a model using multiple meshes as a sub-graph of the scenegraph
    /// </summary>
    class PlayerModel : CharacterModel
    {
        //Enitity ModelElements
        ModelElement hullMdlElm, sphereMdlElm, powerCellsMdlElm;
        //Node ModelElements
        ModelElement hullGroupNode, wheelGroupNode, gunsGroupNode;
        //Game Node ModelElement
        ModelElement modelNode;


        public PlayerModel(SceneManager mSceneMgr)
        {
            this.mSceneMgr = mSceneMgr;
            LoadModelElements();
            AssembleModel();
        }

        protected override void LoadModelElements()
        {
            //loading model entities
            hullMdlElm = new ModelElement(mSceneMgr, "Main.mesh");
            sphereMdlElm = new ModelElement(mSceneMgr, "Sphere.mesh");
            powerCellsMdlElm = new ModelElement(mSceneMgr, "PowerCells.mesh");

            hullMdlElm.GameEntity.GetMesh().BuildEdgeList();
            sphereMdlElm.GameEntity.GetMesh().BuildEdgeList();
            powerCellsMdlElm.GameEntity.GetMesh().BuildEdgeList();


            //loading model nodes
            hullGroupNode = new ModelElement(mSceneMgr);
            wheelGroupNode = new ModelElement(mSceneMgr);
            gunsGroupNode = new ModelElement(mSceneMgr);
            //loading game node
            modelNode = new ModelElement(mSceneMgr);
            gameNode = modelNode.GameNode;

        }

        protected override void AssembleModel()
        {
            //Add hullGroupNode to gamenode
            modelNode.AddChild(hullGroupNode.GameNode);
            //Adding entities to hullGroupNode
            hullGroupNode.AddChild(powerCellsMdlElm.GameNode);
            hullGroupNode.AddChild(hullMdlElm.GameNode);
            //Adding nodes to hullGroupNode
            hullGroupNode.AddChild(wheelGroupNode.GameNode);
            hullGroupNode.AddChild(gunsGroupNode.GameNode);
            //Adding enitities to wheelGroupNode
            wheelGroupNode.AddChild(sphereMdlElm.GameNode);
            //Adding gamenode to RootSceneNode
            mSceneMgr.RootSceneNode.AddChild(gameNode);

            //physObj = new PhysObj();
            //physObj.AddForceToList(new WeightForce(physObj.InvMass));
            //physObj.SceneNode = gameNode;
        }

        public override void Dispose()
        {
            //Dispose entities
            hullMdlElm.Dispose();
            sphereMdlElm.Dispose();
            powerCellsMdlElm.Dispose();
            //Dispose scene nodes
            hullGroupNode.Dispose();
            wheelGroupNode.Dispose();
            gunsGroupNode.Dispose();
            //Dispose gamenode
            modelNode.Dispose();
        }

        public override void Animate(FrameEvent evt)
        {
            wheelGroupNode.GameNode.Pitch(Mogre.Math.AngleUnitsToDegrees(0.005f));
        }

        /// <summary>
        /// This method adds a child to the robot node
        /// </summary>
        /// <param name="child">The scene node to be set as a child</param>
        public void AddChild(SceneNode child)
        {
            modelNode.AddChild(child);
        }





    }
}

