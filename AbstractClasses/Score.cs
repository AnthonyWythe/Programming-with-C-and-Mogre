using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;

namespace Tutorial
{
    class Score : Stat
    {
        private void Increase(int val)
        {
            val = val + 1;
        }

        private void Decrease(int val)
        {
            val = val - 1;
        }
    }
}
