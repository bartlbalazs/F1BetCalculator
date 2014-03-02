using F1BetCalculator.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace F1BetCalculator.Model
{
    class MainDataService
    {
        private readonly RacerFactory _racerFactory = new RacerFactory();
        private readonly GeneFactory _geneFactory = new GeneFactory();

        private IEnumerable<Racer> _racers = null;

        public IEnumerable<GeneBase> GetGenes()
        {
            if (_racers == null)
                loadRacers();

           return _geneFactory.BuildBaseGenes(_racers);
        }

        private void loadRacers()
        {
            _racers = _racerFactory.BuildRacers();
        }

        public IEnumerable<IBreedable> GetRandomCreatures(IEnumerable<GeneBase> selectedGenes, int count, int maxPriority)
        {
            setMaxPriorityToGenes(selectedGenes, maxPriority);

            var result = new List<IBreedable>();
            for (int i = 0; i < count; i++)
            {
                result.Add(createCreature(selectedGenes));
            }
            return result;
        }

        private void setMaxPriorityToGenes(IEnumerable<GeneBase> selectedGenes, int maxPriority)
        {
            foreach (var item in selectedGenes)
            {
                item.SetMaxPriority(maxPriority);
            }
        }

        private IBreedable createCreature(IEnumerable<GeneBase> genes)
        {
            var newCreature = new Creature();
            foreach (var gene in genes)
            {
                var newGene = gene.Copy();
                newGene.Mutate();
                newCreature.Genes.Add(newGene);
            }

            return newCreature;
        }

    }
}
