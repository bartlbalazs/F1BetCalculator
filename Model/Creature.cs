using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace F1BetCalculator.Model
{
    public class Creature : IMutateable, IBreedable
    {
        private static Random _rnd = new Random();
        public List<GeneBase> Genes { get; set; }
        public double Fittness { get; set; }

        public Creature()
        {
            Genes = new List<GeneBase>();
        }

        public IBreedable breed(IBreedable pair)
        {
            var result = new Creature();

            for (int i = 0; i < this.Genes.Count()/2; i++)
            {
                result.Genes.Add(this.Genes[i].Copy());
            }

            for (int i = this.Genes.Count() / 2; i < this.Genes.Count(); i++)
            {
                result.Genes.Add(pair.Genes[i].Copy());
            }

            result.RandomGene.Mutate();

            return result;
        }

        public GeneBase RandomGene
        {
	        get { return this.Genes[_rnd.Next(this.Genes.Count)]; }
        }

        public void Mutate()
        {
            Genes[_rnd.Next(Genes.Count())].Mutate();
        }

        public void InitToRandom()
        {
            foreach (var gene in Genes)
            {
                gene.Mutate();
            }
        }

        public override bool Equals(Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            Creature c = obj as Creature;
            if ((Object)c == null)
            {
                return false;
            }

            // Return true if the fields match:
            for (int i = 0; i < Genes.Count(); i++)
            {
                if (c.Genes.Count() - 1 < i)
                    return false;

                if (!Genes[i].Equals(c.Genes[i]))
                    return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return (from g in Genes select g.GetHashCode()).Sum();
        }
    }
}
