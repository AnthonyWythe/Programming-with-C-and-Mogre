using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;

namespace Tutorial
{
    class PlayerStats : CharacterStats
    {
        Stat score;

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
