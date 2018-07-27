using Mogre;
using Mogre.TutorialFramework;
using System;
using System.Collections;
using PhysicsEng;

namespace Tutorial
{
    class HealthPU : PowerUp
    {
        Entity pUEntity;
        SceneNode pUNode;

        PhysObj physObj;
        SceneNode controlNode;

        bool removeMe;

        public HealthPU(SceneManager mScenemgr, Stat Health) : base(mScenemgr)
        {
            this.Stat = Health;
            this.increase = 20;
        }

        protected override void LoadModel()
        {
            base.LoadModel();
            pUEntity = mSceneMgr.CreateEntity("Sphere.mesh");
            pUNode = mSceneMgr.CreateSceneNode();
            pUNode.Scale(0.5f, 0.5f, 0.5f);
            pUNode.AttachObject(pUEntity);



            controlNode = mSceneMgr.CreateSceneNode();
            controlNode.AddChild(pUNode);

            mSceneMgr.RootSceneNode.AddChild(controlNode);

            float radius = 0.5f;
            controlNode.Position += radius * Vector3.UNIT_Y;
            pUNode.Position -= radius * Vector3.UNIT_Y;

            physObj = new PhysObj(radius,"Ball",0.1f, 0.7f, 0.3f);
            physObj.SceneNode = controlNode;
            physObj.Position = controlNode.Position;
            physObj.AddForceToList(new WeightForce(physObj.InvMass));
            Physics.AddPhysObj(physObj);

            physObj.SceneNode = pUNode;

            pUEntity.GetMesh().BuildEdgeList();
        }

 

        public SceneNode PUNode
        {
            get { return pUNode; }
        }

        public SceneNode ControlNode
        {
            get { return pUNode; }
        }

        public void setPosition(Vector3 position)
        {
            controlNode.Translate(position);
        }


        /// <summary>
        /// This method determine wheter the bomb is colliding with a named object  type
        /// </summary>
        /// <param name="objName">The name of the potential colliding object</param>
        /// <returns>True if a collision with the named object type happens, false otherwhise</returns>
        public bool IsCollidingWith(string objName)
        {
            bool isColliding = false;
            foreach (Contacts c in physObj.CollisionList)
            {
                if (c.colliderObj.ID == objName || c.collidingObj.ID == objName)
                {
                    isColliding = true;
                    break;
                }
            }
            return isColliding;
        }

        /// <summary>
        /// This method update the bomb state
        /// </summary>
        /// <param name="evt"></param>
        public override void Update(FrameEvent evt)
        {
            removeMe = IsCollidingWith("Player");
        }
    }
}

