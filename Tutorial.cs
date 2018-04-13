using Mogre;
using Mogre.TutorialFramework;
using System;

namespace Tutorial
{
    class Tutorial : BaseApplication
    {
        //PlayerModel playerModel;
        Player player;
        SceneNode cameraNode;

        public static void Main()
        {
            new Tutorial().Go();            // This method starts the rendering loop
        }

        /// <summary>
        /// This method create the initial scene
        /// </summary>
        protected override void CreateScene()
        {
            //playerModel = new PlayerModel(mSceneMgr);
            player = new Player(mSceneMgr);
            cameraNode = mSceneMgr.CreateSceneNode();
            cameraNode.AttachObject(mCamera);

            //player.Model.GameNode.AddChild(cameraNode);


        }

        /// <summary>
        /// This method destrois the scene
        /// </summary>
        protected override void DestroyScene()
        {
            //if (player != null)
            //{
                //player.Model.GameNode.Dispose();
            //}
            base.DestroyScene();
        }

        /// <summary>
        /// This method create a new camera
        /// </summary>
        protected override void CreateCamera()
        {
            mCamera = mSceneMgr.CreateCamera("PlayerCam");
            mCamera.Position = new Vector3(0, 100, 500);
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
            base.UpdateScene(evt);
            player.Update(evt);
            //mCamera.LookAt(player.Position);
        }



        /// <summary>
        /// This method set create a frame listener to handle events before, during or after the frame rendering
        /// </summary>
        protected override void CreateFrameListeners()
        {
            base.CreateFrameListeners();
        }

        /// <summary>
        /// This method initilize the inputs reading from keyboard adn mouse
        /// </summary>
        protected override void InitializeInput()
        {
            base.InitializeInput();
        }
    }
}