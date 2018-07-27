using Mogre;
using Mogre.TutorialFramework;
using System;
using System.Collections;

namespace Tutorial
{
    class Score : Stat
    {
        public override void Increase(int val)
        {
            val = val + 1;
        }

        private void Decrease(int val)
        {
            val = val - 1;
        }
    }
}
