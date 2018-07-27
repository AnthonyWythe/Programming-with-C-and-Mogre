using System;
using Mogre;
using PhysicsEng;

namespace Tutorial
{
    /// <summary>
    /// This class implements a bomb
    /// </summary>
    class Bomb
    {
        SceneManager mSceneMgr;

        Entity bombEntity;
        SceneNode bombNode;

        PhysObj physObj;

        bool removeMe;                  // Flag to determine when the bomb should be removed
        
        /// <summary>
        /// Read only. This property gets whether the bomb should be removed from the game
        /// </summary>
        public bool RemoveMe
        {
            get { return removeMe; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mSceneMgr">A reference to the SceneManager</param>
        public Bomb(SceneManager mSceneMgr)
        {
            this.mSceneMgr = mSceneMgr;
            Load();
        }

        /// <summary>
        /// This method load the bomb mesh and its physics object
        /// </summary>
        private void Load()
        {
            removeMe = false;
            bombEntity = mSceneMgr.CreateEntity("Bomb.mesh");

            bombNode = mSceneMgr.CreateSceneNode();
            bombNode.Scale(2, 2, 2);
            bombNode.AttachObject(bombEntity);
            mSceneMgr.RootSceneNode.AddChild(bombNode);

            physObj = new PhysObj(10, "Bomb", 0.1f, 0.5f);
            physObj.SceneNode = bombNode;
            physObj.AddForceToList(new WeightForce(physObj.InvMass));

            Physics.AddPhysObj(physObj);
        }

        /// <summary>
        /// This method set the position of the bomb in the given location
        /// </summary>
        /// <param name="position">The location where to position the bomb</param>
        public void SetPosition(Vector3 position)
        {
            bombNode.Position = position;
            physObj.Position = position;
        }

        /// <summary>
        /// This method update the bomb state
        /// </summary>
        /// <param name="evt"></param>
        public void Update(FrameEvent evt)
        {
            removeMe = IsCollidingWith("Robot");
        }

        /// <summary>
        /// This method determine wheter the bomb is colliding with a named object  type
        /// </summary>
        /// <param name="objName">The name of the potential colliding object</param>
        /// <returns>True if a collision with the named object type happens, false otherwhise</returns>
        private bool IsCollidingWith(string objName)
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
        /// This method dispose of the bomb, destroying the physics object, and removing the bomb and its mesh from the scenegraph
        /// </summary>
        public void Dispose()
        {
            Physics.RemovePhysObj(physObj);
            physObj = null;

            bombNode.Parent.RemoveChild(bombNode);
            bombNode.DetachAllObjects();
            bombNode.Dispose();
            bombEntity.Dispose();
        }
    }
}
