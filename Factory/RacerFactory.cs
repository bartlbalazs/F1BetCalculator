using F1BetCalculator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace F1BetCalculator.Factory
{
    class RacerFactory
    {
        private IRacerService racerService = new RacerService();

        public IEnumerable<Racer> BuildRacers()
        {
            var result = new List<Racer>();
            racerService.GetRacers(
                (racers,exception)=>
                {
                    if (exception != null)
                        return;
                    result = new List<Racer>(racers.OrderBy(x=>x.Number));
                }
            );
            return result;
        }
    }
}
