using System;
using System.Text;

namespace F1BetCalculator.Model
{
    public class RacerGene : GeneBase
    {
        public override GeneBase Copy()
        {
            var result = new RacerGene()
            {
                Priority = this.Priority,
                Odds = this.Odds,
                Racer = this.Racer
            };
            return result;
        }

        public Racer Racer { get; set; }

        public override bool Equals(Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            RacerGene g = obj as RacerGene;
            if ((Object)g == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (Priority == g.Priority) && (Odds == g.Odds) && (Racer == g.Racer);
        }

        public override int GetHashCode()
        {
            return Priority ^ (int)(Odds + Racer.GetHashCode());
        }
    }
}
