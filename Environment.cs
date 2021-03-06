﻿using System;
using Mogre;
using PhysicsEng;

namespace Tutorial
{
    /// <summary>
    /// This class implements the game environment
    /// </summary>
    class Environment
    {
        SceneManager mSceneMgr;             // This field will contain a reference of the scene manages
        RenderWindow mWindow;               // This field will contain a reference to the rendering window

        Ground ground;

        Light light;  

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mSceneMgr">A reference to the scene manager</param>
        public Environment(SceneManager mSceneMgr, RenderWindow mWindow)
        {
            this.mSceneMgr = mSceneMgr;
            this.mWindow = mWindow;

            Load();                                 // This method loads  the environment
        }

        private void Load()
        {
            SetSky();
            //SetFog();
            SetLights();
            SetShadows();
            ground = new Ground(mSceneMgr);
            Physics.AddBoundary(ground.Plane);
        }

        /// <summary>
        /// This method dispose of any object instanciated in this class
        /// </summary>
        public void Dispose()
        {
            ground.Dispose();
        }

        private void SetSky()
        {
            //mSceneMgr.SetSkyDome(true, "Sky", 1f, 10, 500, true);

            //Plane sky = new Plane(Vector3.NEGATIVE_UNIT_Y, -100);
           // mSceneMgr.SetSkyPlane(true, sky, "Sky", 10, 5, true, 0.5f, 100, 100,
                //ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME);

            mSceneMgr.SetSkyBox(true, "SkyBox", 10, true);
        }

        private void SetFog()
        {
            ColourValue fadeColour = new ColourValue(0.9f, 0.9f, 1f);
            //mSceneMgr.SetFog(FogMode.FOG_LINEAR, fadeColour, 0.1f, 100, 1000);
            //mSceneMgr.SetFog(FogMode.FOG_EXP, fadeColour, 0.001f);
            mSceneMgr.SetFog(FogMode.FOG_EXP2, fadeColour, 0.0005f);
            mWindow.GetViewport(0).BackgroundColour = fadeColour;
        }

        /// <summary>
        /// This method sets the lights in the environment
        /// </summary>
        private void SetLights()
        {
            mSceneMgr.AmbientLight = new ColourValue(0.3f, 0.3f, 0.3f);                 // Set the ambient light in the scene

            light = mSceneMgr.CreateLight();                                            // Set an instance of a light;

            light.DiffuseColour = ColourValue.White;                                      // Sets the color of the light
            light.Position = new Vector3(0, 100, 0);                                    // Sets the position of the light

            //light.Type = Light.LightTypes.LT_DIRECTIONAL;                               // Sets the light to be a directional Light

            //light.Type = Light.LightTypes.LT_SPOTLIGHT;                                 // Sets the light to be a spot light
            //light.SetSpotlightRange(Mogre.Math.PI / 6, Mogre.Math.PI / 4, 0.001f);      // Sets the spot light parametes

            light.Direction = Vector3.NEGATIVE_UNIT_Y;                                  // Sets the light direction

            light.Type = Light.LightTypes.LT_POINT;                                     // Sets the light to be a point light

            float range = 2000;                                                         // Sets the light range
            float constantAttenuation = 0;                                              // Sets the constant attenuation of the light [0, 1]
            float linearAttenuation = 0;                                                // Sets the linear attenuation of the light [0, 1]
            float quadraticAttenuation = 0.0001f;                                       // Sets the quadratic  attenuation of the light [0, 1]

            light.SetAttenuation(range, constantAttenuation,
                      linearAttenuation, quadraticAttenuation); // Not applicable to directional lights
        }

        private void SetShadows()
        {
            mSceneMgr.ShadowTechnique = ShadowTechnique.SHADOWTYPE_STENCIL_MODULATIVE;
            //mSceneMgr.ShadowTechnique = ShadowTechnique.SHADOWTYPE_STENCIL_ADDITIVE;
        }
    }
}
