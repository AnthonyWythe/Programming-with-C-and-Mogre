﻿using Mogre;
using Mogre.TutorialFramework;
using System;
using System.Collections;

namespace Tutorial
{
    /// <summary>
    /// This class describes and initialize an element of the character model
    /// </summary>
    class ModelElement:MovableElement
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mSceneMgr">A reference to the scene manager</param>
        /// <param name="modelName">The name of the .mesh file which contains the geometry of the model</param>
        public ModelElement(SceneManager mSceneMgr, string modelName = "")
        {
             gameNode = mSceneMgr.CreateSceneNode();

            if (modelName != "")
            {
                gameEntity = mSceneMgr.CreateEntity(modelName);
                gameNode.AttachObject(gameEntity);


            }
        }

        /// <summary>
        /// This method moves the model element in the specified direction
        /// </summary>
        /// <param name="direction">A direction in which to move the model element</param>
        public override void Move(Vector3 direction)
        {
            // YOUR CODE FOR MOVING THE GAMENODE GOES HERE
            this.gameNode.Translate(direction);
        }

        /// <summary>
        /// This modeto rotate the model element as described by the quaternion given as parameter in the
        /// specified transformation space
        /// </summary>
        /// <param name="quaternion">The quaternion which describes the rotation axis and angle</param>
        /// <param name="transformSpace">The space in which to perfrom the rotation, local by default</param>
        public override void Rotate(Quaternion quaternion, 
                        Node.TransformSpace transformSpace = Node.TransformSpace.TS_LOCAL)
        {
            // YOUR CODE FOR ROTATING THE GAMENODE GOES HERE

        }

        /// <summary>
        /// This method adds a child to the node of this model element
        /// </summary>
        /// <param name="childNode"></param>
        public void AddChild(SceneNode childNode)
        {
            gameNode.AddChild(childNode);
        }
    }
}
