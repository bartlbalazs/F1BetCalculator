using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace F1BetCalculator.Model
{
    public abstract class GeneBase : IMutateable
    {
        protected static Random _rnd = new Random();
        public static int MaxPriority { get; set; }

        public bool IsSelected { get; set; }
        public int Priority { get; set; }
        public double Odds { get; set; }

        public abstract GeneBase Copy();

        public void Mutate()
        {
            if (_rnd.Next(0, 1) == 1)
            {
                Priority = Priority + _rnd.Next(1, MaxPriority / 5);
            }
            else
            {
                Priority = Priority + _rnd.Next(1, MaxPriority / 5);
            }

            if (Priority < 1)
                Priority = 1;

            if (Priority > MaxPriority)
                Priority = MaxPriority;
        }

        public void SetMaxPriority(int maxPriority)
        {
            MaxPriority = maxPriority;
        }

    }
}
