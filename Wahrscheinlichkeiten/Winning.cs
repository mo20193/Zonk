using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wahrscheinlichkeiten
{
    internal class Winning
    {
        public int SetPercent { get; set; }
        public int NumberOfWinnings { get; set; }


        public Winning(int setPercent, int numberOfWinnings)
        {
            SetPercent = setPercent;
            NumberOfWinnings = numberOfWinnings;
        }
    }
}
