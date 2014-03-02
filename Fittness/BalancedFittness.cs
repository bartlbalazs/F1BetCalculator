using F1BetCalculator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace F1BetCalculator.Fittness
{
    public class BalancedFittness : IFittnessFunction
    {
        public void Evaluate(IEnumerable<IBreedable> population)
        {
            foreach (var creature in population)
            {
                evaluate(creature);
            }
        }

        private void evaluate(IBreedable creature)
        {
            var cost = (from g in creature.Genes select g.Priority).Sum();
            List<double> gains = new List<double>();

            foreach (var gene in creature.Genes)
            {
                gains.Add((gene.Odds * gene.Priority) - cost);
            }

            var avarage = gains.Average();
            double fittness = avarage/cost;

            foreach (var item in gains)
            {
                if (item < avarage)
                    fittness -= (avarage - item)/cost;
            }


            creature.Fittness = fittness;
        }
    }
}
