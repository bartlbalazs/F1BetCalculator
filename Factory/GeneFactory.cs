using F1BetCalculator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace F1BetCalculator.Factory
{
    class GeneFactory
    {
        public IEnumerable<GeneBase> BuildBaseGenes(IEnumerable<Racer> racers)
        {
            var result = new List<GeneBase>();
            foreach (var racer in racers)
            {
                result.Add(new RacerGene() { Odds = 0, Priority = 0, Racer = racer });
            }
            return result;
        }
    }
}
