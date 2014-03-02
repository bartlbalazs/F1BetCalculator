using F1BetCalculator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace F1BetCalculator.Fittness
{
    public class FittnessFunction : IFittnessFunction
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
            double fittness = 0;
            var cost = (from g in creature.Genes select g.Priority).Sum();

            foreach (var gene in creature.Genes) 
            {
                double gain = 0;
                gain = (gene.Odds * gene.Priority)-cost;

                if (gain < 0)
                {
                    gain = gain * 5;
                }

                if (gain == 0)
                {
                    gain = -cost*2;
                }

                fittness += (gain / cost)*(1.0/gene.Odds);
            }
            creature.Fittness = fittness;
        }
    }
}
