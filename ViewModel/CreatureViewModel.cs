using F1BetCalculator.Message;
using F1BetCalculator.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace F1BetCalculator.ViewModel
{
    public class CreatureViewModel : ViewModelBase
    {
        public IBreedable Creature{ get; set; }

        public class RacerDisplay
        {
            public string Name { get; set; }
            public double Bet { get; set; }
            public double Profit { get; set; }
        }

        /// <summary>
        /// The <see cref="RacerDisplays" /> property's name.
        /// </summary>
        public const string RacerDisplaysPropertyName = "RacerDisplays";

        private ObservableCollection<RacerDisplay> _racerDisplays = new ObservableCollection<RacerDisplay>();

        /// <summary>
        /// Sets and gets the RacerDisplays property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<RacerDisplay> RacerDisplays
        {
            get
            {
                return _racerDisplays;
            }

            set
            {
                if (_racerDisplays == value)
                {
                    return;
                }

                RaisePropertyChanging(RacerDisplaysPropertyName);
                _racerDisplays = value;
                RaisePropertyChanged(RacerDisplaysPropertyName);
            }
        }
        
        public CreatureViewModel(IBreedable baseCreature, int cash)
        {
            Creature = baseCreature;
            Messenger.Default.Register<CashMessage>(this, cashMessage);
            initialize(cash);
        }

        private void cashMessage(CashMessage message)
        {
            if (message != null)
            {
                initialize(message.Cash);
            }
        }

        private void initialize(int cash)
        {
            if (Creature != null)
            {
                RacerDisplays.Clear();

                var cost = (from g in Creature.Genes select g.Priority).Sum();

                foreach (var item in Creature.Genes)
                {
                    var display = new RacerDisplay();
                    display.Name = ((RacerGene)item).Racer.Name;
                    display.Bet = ((double)item.Priority / cost) * cash;
                    display.Profit = (item.Odds* display.Bet) - cash;
                    RacerDisplays.Add(display);
                }
            }
        }
    }
}
