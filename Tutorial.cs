using Mogre;
using Mogre.TutorialFramework;
using System;
using System.Collections;
using PhysicsEng;
using System.Collections.Generic;

namespace Tutorial
{
    class Tutorial : BaseApplication
    {
        Player player;
        SceneNode cameraNode;
        InputsManager inputsManager = InputsManager.Instance;



        Cube wallLeft;
        SceneNode wallLeftNode;
        Entity wallLeftEntity;

        Cube wallRight;
        SceneNode wallRightNode;
        Entity wallRightEntity;

        Cube wallTop;
        SceneNode wallTopNode;
        Entity wallTopEntity;

        Cube wallBottom;
        SceneNode wallBottomNode;
        Entity wallBottomEntity;

        BlueGem gemBlue;
        RedGem gemRed;

        ShieldPU shieldPU;
        LifePU lifePU;
        HealthPU healthPU;

        List<Bomb> bombs;
        List<Bomb> bombsToRemove;
        static public bool shoot;

        Robot robot;
        Robot robot2;
        Robot robot3;

        GameInterface gameHMD;

        Environment environment;

        Physics physics;

 

        public static void Main()
        {
            new Tutorial().Go();            // This method starts the rendering loop
        }

        /// <summary>
        /// This method create the initial scene
        /// </summary>
        protected override void CreateScene()
        {
            physics = new Physics();

            player = new Player(mSceneMgr);

            cameraNode = mSceneMgr.CreateSceneNode();
            cameraNode.AttachObject(mCamera);
            player.Model.GameNode.AddChild(cameraNode);

            inputsManager.PlayerController = (PlayerController)player.Controller;


            robot = new Robot(mSceneMgr);
            robot2 = new Robot(mSceneMgr);
            robot3 = new Robot(mSceneMgr);

            robot.RobotNode.Translate(100, -7, -100);
            robot2.RobotNode.Translate(200, -7, 200);
            robot3.RobotNode.Translate(-200, -7, -200);



            gemBlue = new BlueGem(mSceneMgr,((PlayerStats)player.Stats).Score);

            gemBlue.GemNode.Translate(100, 0, 0);

            gemRed = new RedGem(mSceneMgr, ((PlayerStats)player.Stats).Score);

            gemRed.GemNode.Translate(200, 0, 0);



            shieldPU = new ShieldPU(mSceneMgr, player.Stats.Shield);

            shieldPU.PUNode.Translate(-100, 0, 0);

            lifePU = new LifePU(mSceneMgr, player.Stats.Lives);

            lifePU.PUNode.Translate(0, 0, 0);

            healthPU = new HealthPU(mSceneMgr, player.Stats.Health);

            healthPU.ControlNode.Translate(0,300,0);



            bombs = new List<Bomb>();
            bombsToRemove = new List<Bomb>();





            wallLeft = new Cube(mSceneMgr);
            MeshPtr cubePtr = wallLeft.getCube("wallLeft", "Dirt", 20, 50, 1000);
            wallLeftEntity = mSceneMgr.CreateEntity("wallLeft_Entity", "wallLeft");
            wallLeftNode = mSceneMgr.RootSceneNode.CreateChildSceneNode("wallLeft_Node");
            wallLeftNode.AttachObject(wallLeftEntity);

            wallLeftNode.Translate(500,0,0);

            wallRight = new Cube(mSceneMgr);
            MeshPtr wallRightPtr = wallRight.getCube("wallRight", "Dirt", 20, 50, 1000);
            wallRightEntity = mSceneMgr.CreateEntity("wallRight_Entity", "wallRight");
            wallRightNode = mSceneMgr.RootSceneNode.CreateChildSceneNode("wallRight_Node");
            wallRightNode.AttachObject(wallRightEntity);

            wallRightNode.Translate(-500, 0, 0);

            wallTop = new Cube(mSceneMgr);
            MeshPtr wallTopPtr = wallTop.getCube("wallTop", "Dirt", 1000, 50, 20);
            wallTopEntity = mSceneMgr.CreateEntity("wallTop_Entity", "wallTop");
            wallTopNode = mSceneMgr.RootSceneNode.CreateChildSceneNode("wallTop_Node");
            wallTopNode.AttachObject(wallTopEntity);

            wallTopNode.Translate(0, 0, 500);

            wallBottom = new Cube(mSceneMgr);
            MeshPtr wallBottomPtr = wallBottom.getCube("wallBottom", "Dirt", 1000, 50, 20);
            wallBottomEntity = mSceneMgr.CreateEntity("wallBottom_Entity", "wallBottom");
            wallBottomNode = mSceneMgr.RootSceneNode.CreateChildSceneNode("wallBottom_Node");
            wallBottomNode.AttachObject(wallBottomEntity);

            wallBottomNode.Translate(0, 0, -500);




            PlayerStats playerStats = new PlayerStats();
            gameHMD = new GameInterface(mSceneMgr, mWindow, playerStats);

            environment = new Environment(mSceneMgr, mWindow);

            physics.StartSimTimer();
        }

        /// <summary>
        /// This method destrois the scene
        /// </summary>
        protected override void DestroyScene()
        {
            if (player != null)
            {
                player.Model.GameNode.Dispose();
            }
            base.DestroyScene();
            gameHMD.Dispose();
            environment.Dispose();
            robot.Dispose();
            robot2.Dispose();
            robot3.Dispose();


            foreach (Bomb bomb in bombs)
            {
                bomb.Dispose();
            }

            physics.Dispose();
        }

        /// <summary>
        /// This method create a new camera
        /// </summary>
        protected override void CreateCamera()
        {
            mCamera = mSceneMgr.CreateCamera("PlayerCam");
            mCamera.Position = new Vector3(0, 50, -125);
            mCamera.LookAt(new Vector3(0, 0, 0));
            mCamera.NearClipDistance = 5;
            mCamera.FarClipDistance = 1000;
            mCamera.FOVy = new Degree(70);

            mCameraMan = new CameraMan(mCamera);
            mCameraMan.Freeze = true;
            
        }

        /// <summary>
        /// This method create a new viewport
        /// </summary>
        protected override void CreateViewports()
        {
            Viewport viewport = mWindow.AddViewport(mCamera);
            viewport.BackgroundColour = ColourValue.Black;
            mCamera.AspectRatio = viewport.ActualWidth / viewport.ActualHeight;
        }

        
        
        /// <summary>
        /// This method update the scene after a frame has finished rendering
        /// </summary>
        /// <param name="evt"></param>
        protected override void UpdateScene(FrameEvent evt)
        {
            physics.UpdatePhysics(0.01f);
            base.UpdateScene(evt);

            if (shoot)
            {
                AddBomb();
            }

            foreach (Bomb bomb in bombs)
            {
                bomb.Update(evt);
                if (bomb.RemoveMe)
                    bombsToRemove.Add(bomb);
            }

            foreach (Bomb bomb in bombsToRemove)
            {
                bombs.Remove(bomb);
                bomb.Dispose();
            }
            bombsToRemove.Clear();

            shoot = false;

            inputsManager.ProcessInput(evt);
            player.Update(evt);
            mCamera.LookAt(player.Position);

            robot.Animate(evt);
            robot2.Animate(evt);
            robot3.Animate(evt);

            gameHMD.Update(evt);
            
            
        }

        private void AddBomb()
        {
            Bomb bomb = new Bomb(mSceneMgr);
            bomb.SetPosition(new Vector3(Mogre.Math.RangeRandom(0, 100), 100, Mogre.Math.RangeRandom(0, 100)));
            bombs.Add(bomb);
        }



        /// <summary>
        /// This method set create a frame listener to handle events before, during or after the frame rendering
        /// </summary>
        protected override void CreateFrameListeners()
        {
            base.CreateFrameListeners();
            mRoot.FrameRenderingQueued +=
                new FrameListener.FrameRenderingQueuedHandler(inputsManager.ProcessInput);
        }

        /// <summary>
        /// This method initilize the inputs reading from keyboard adn mouse
        /// </summary>
        protected override void InitializeInput()
        {
            base.InitializeInput();

            int windowHandle;
            mWindow.GetCustomAttribute("WINDOW", out windowHandle);
            inputsManager.InitInput(ref windowHandle);
        }
    }
}