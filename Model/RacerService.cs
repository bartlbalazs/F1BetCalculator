using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace F1BetCalculator.Model
{
    class RacerService : IRacerService
    {
        private const string xmlFileName = "RacerList.xml";
        private const string racerElementName = "racer";
        private const string numberElementName = "number";
        private const string nameElementName = "name";
        private const string teamElementName = "team";

        public void GetRacers(Action<IEnumerable<Racer>, Exception> callback)
        {
            try
            {
                XDocument xdoc = XDocument.Load(xmlFileName);


                var racers = (from n in xdoc.Descendants(racerElementName)
                                 select new Racer()
                                 {
                                     Number = Int16.Parse((from n1 in n.Descendants(numberElementName) select n1).FirstOrDefault().Value),
                                     Name = (from n1 in n.Descendants(nameElementName) select n1).FirstOrDefault().Value,
                                     Team = (from n1 in n.Descendants(teamElementName) select n1).FirstOrDefault().Value,
                                 }).ToList();

                callback(racers, null);
            }

            catch (Exception e)
            {
                callback(null, e);
            }
        }
    }
}
