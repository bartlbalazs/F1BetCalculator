using F1BetCalculator.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace F1BetCalculator.Fittness
{
    public interface IFittnessFunction
    {
        void Evaluate(IEnumerable<IBreedable> population);
    }
}
