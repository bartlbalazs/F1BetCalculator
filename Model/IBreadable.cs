using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace F1BetCalculator.Model
{
    public interface IBreedable
    {
        List<GeneBase> Genes { get; set; }
        double Fittness { get; set; }
        GeneBase RandomGene {get;}
        IBreedable breed(IBreedable pair);
        void InitToRandom();
    }
}
