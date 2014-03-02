using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace F1BetCalculator.Model
{
    interface IRacerService
    {
        void GetRacers(Action<IEnumerable<Racer>, Exception> callback);
    }
}
