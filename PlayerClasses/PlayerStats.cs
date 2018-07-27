using Mogre;
using Mogre.TutorialFramework;
using System;
using System.Collections;

namespace Tutorial
{
    class PlayerStats : CharacterStats
    {
        Stat score;

        public Stat Score
        {
            get { return score; }
        }

        public PlayerStats()
        {
            InitStats();
 
        }
        protected override void InitStats()
        {
            score = new Score();
            base.InitStats();
            score.InitValue(0);
            health.InitValue(100);
            shield.InitValue(100);
            lives.InitValue(3);
            
        }
    }
    }
