using Mogre;
using Mogre.TutorialFramework;
using System;
using System.Collections;

namespace Tutorial
{
    class Cube
    {
        ManualObject cube;
        Entity cubeEntity;
        SceneNode cubeNode;

        SceneManager mSceneMgr;



        public Cube(SceneManager mSceneMgr)
        {
            this.mSceneMgr = mSceneMgr;

        }

        public MeshPtr getCube(string cubeName, string materialName, float width, float height, float depth)
        {
            cube = mSceneMgr.CreateManualObject(cubeName + "_ManObj");
            cube.Begin(materialName, RenderOperation.OperationTypes.OT_TRIANGLE_LIST);

            // --- Fills the Vertex buffer and define the texture coordinates for each vertex ---

            //--- Vertex 0 ---
            cube.Position(new Vector3(.5f * width, .5f * height, .5f * depth));
            cube.TextureCoord(new Vector2(0, 0));
            //--- Vertex 1 ---
            cube.Position(new Vector3(.5f * width, -.5f * height, .5f * depth));
            cube.TextureCoord(new Vector2(1, 0));
            //--- Vertex 2 ---
            cube.Position(new Vector3(.5f * width, .5f * height, -.5f * depth));
            cube.TextureCoord(new Vector2(0, 1));
            //--- Vertex 3 ---
            cube.Position(new Vector3(.5f * width, -.5f * height, -.5f * depth));
            cube.TextureCoord(new Vector2(1, 1));

            //--- Vertex 4 ---
            cube.Position(new Vector3(-.5f * width, .5f * height, .5f * depth));
            cube.TextureCoord(new Vector2(0, 0));
            //--- Vertex 5 ---
            cube.Position(new Vector3(-.5f * width, -.5f * height, .5f * depth));
            cube.TextureCoord(new Vector2(1, 0));
            //--- Vertex 6 ---
            cube.Position(new Vector3(-.5f * width, .5f * height, -.5f * depth));
            cube.TextureCoord(new Vector2(0, 1));
            //--- Vertex 7 ---
            cube.Position(new Vector3(-.5f * width, -.5f * height, -.5f * depth));
            cube.TextureCoord(new Vector2(1, 1));

            // --- Fills the Index Buffer ---
            //--------Face 1----------
            cube.Index(0);
            cube.Index(1);
            cube.Index(2);

            cube.Index(2);
            cube.Index(1);
            cube.Index(3);

            //--------Face 2----------
            cube.Index(4);
            cube.Index(6);
            cube.Index(5);

            cube.Index(6);
            cube.Index(7);
            cube.Index(5);

            //--------Face 3----------
            cube.Index(0);
            cube.Index(4);
            cube.Index(1);

            cube.Index(1);
            cube.Index(4);
            cube.Index(5);

            //--------Face 4----------
            cube.Index(0);
            cube.Index(6);
            cube.Index(4);

            cube.Index(0);
            cube.Index(2);
            cube.Index(6);

            //--------Face 5----------
            cube.Index(6);
            cube.Index(2);
            cube.Index(3);

            cube.Index(6);
            cube.Index(3);
            cube.Index(7);

            //--------Face 5----------
            cube.Index(3);
            cube.Index(1);
            cube.Index(7);

            cube.Index(1);
            cube.Index(5);
            cube.Index(7);

            cube.End();
            return cube.ConvertToMesh(cubeName);
        }

        public void Load()
        {
            getCube("cube", "Dirt", 100, 100, 100);

            cubeEntity = mSceneMgr.CreateEntity("Cube");
            cubeNode = mSceneMgr.CreateSceneNode();
            cubeNode.AttachObject(cubeEntity);
            mSceneMgr.RootSceneNode.AddChild(cubeNode);

            CubeMaterial();

            //cubeEntity.SetMaterialName("Dirt");
        }

        private void CubeMaterial()
        {
            using (MaterialPtr cubeMat = MaterialManager.Singleton.Create("groundMaterial",
                                    ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME))      // Creates a new Material
            {
                using (TextureUnitState cubeTexture =
             cubeMat.GetTechnique(0).GetPass(0).CreateTextureUnitState("Dirt.jpg")) // Sets the texture for the material
                {
                    cubeEntity.SetMaterialName("groundMaterial");                      // Applies the material to the entity
                }
            }
        }

        public void Dispose()
        {
            cubeNode.Parent.RemoveChild(cubeNode);
            cubeNode.DetachAllObjects();
            cubeNode.Dispose();
            cubeEntity.Dispose();
        }
    }
}