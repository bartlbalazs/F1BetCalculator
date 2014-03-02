using GalaSoft.MvvmLight;
using F1BetCalculator.Model;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using F1BetCalculator.Fittness;
using GalaSoft.MvvmLight.Messaging;
using F1BetCalculator.Message;

namespace F1BetCalculator.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly MainDataService _mainDataService = new MainDataService();
        private readonly BackgroundWorker _worker = new BackgroundWorker();
        private readonly IFittnessFunction _unbalancedFittness = new FittnessFunction();
        private readonly IFittnessFunction _balancedFittness = new BalancedFittness();

        #region properties

        public RelayCommand Start { get; set; }

        /// <summary>
        /// The <see cref="AvailableGenes" /> property's name.
        /// </summary>
        public const string AvailableGenesPropertyName = "AvailableGenes";

        private ObservableCollection<GeneBase> _availableGenes = null;

        /// <summary>
        /// Sets and gets the AvailableGenes property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<GeneBase> AvailableGenes
        {
            get
            {
                return _availableGenes;
            }

            set
            {
                if (_availableGenes == value)
                {
                    return;
                }

                RaisePropertyChanging(AvailableGenesPropertyName);
                _availableGenes = value;
                RaisePropertyChanged(AvailableGenesPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Granuality" /> property's name.
        /// </summary>
        public const string GranualityPropertyName = "Granuality";

        private int _granuality = 30;

        /// <summary>
        /// Sets and gets the Granuality property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int Granuality
        {
            get
            {
                return _granuality;
            }

            set
            {
                if (_granuality == value)
                {
                    return;
                }

                RaisePropertyChanging(GranualityPropertyName);
                _granuality = value;
                RaisePropertyChanged(GranualityPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Population" /> property's name.
        /// </summary>
        public const string PopulationCountPropertyName = "PopulationCount";

        private int _populationCount = 1000;

        /// <summary>
        /// Sets and gets the Population property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int PopulationCount
        {
            get
            {
                return _populationCount;
            }

            set
            {
                if (_populationCount == value)
                {
                    return;
                }

                RaisePropertyChanging(PopulationCountPropertyName);
                _populationCount = value;
                RaisePropertyChanged(PopulationCountPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Population" /> property's name.
        /// </summary>
        public const string PopulationPropertyName = "Population";

        private ObservableCollection<IBreedable> _population = null;

        /// <summary>
        /// Sets and gets the Population property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<IBreedable> Population
        {
            get
            {
                return _population;
            }

            set
            {
                if (_population == value)
                {
                    return;
                }

                RaisePropertyChanging(PopulationPropertyName);
                _population = value;
                RaisePropertyChanged(PopulationPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Generation" /> property's name.
        /// </summary>
        public const string GenerationPropertyName = "Generation";

        private int _generation = 40;

        /// <summary>
        /// Sets and gets the Generation property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int Generation
        {
            get
            {
                return _generation;
            }

            set
            {
                if (_generation == value)
                {
                    return;
                }

                RaisePropertyChanging(GenerationPropertyName);
                _generation = value;
                RaisePropertyChanged(GenerationPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="CurrentGeneration" /> property's name.
        /// </summary>
        public const string CurrentGenerationPropertyName = "CurrentGeneration";

        private int _currentGeneration = 0;

        /// <summary>
        /// Sets and gets the CurrentGeneration property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int CurrentGeneration
        {
            get
            {
                return _currentGeneration;
            }

            set
            {
                if (_currentGeneration == value)
                {
                    return;
                }

                RaisePropertyChanging(CurrentGenerationPropertyName);
                _currentGeneration = value;
                RaisePropertyChanged(CurrentGenerationPropertyName);
            }
        }


        /// <summary>
        /// The <see cref="PrevBestFittness" /> property's name.
        /// </summary>
        public const string CurrentBestFittnessPropertyName = "CurrentBestFittness";

        private double _currentBestFittness = double.MinValue;

        /// <summary>
        /// Sets and gets the CurrentBestFittness property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public double PrevBestFittness
        {
            get
            {
                return _currentBestFittness;
            }

            set
            {
                if (_currentBestFittness == value)
                {
                    return;
                }

                RaisePropertyChanging(CurrentBestFittnessPropertyName);
                _currentBestFittness = value;
                RaisePropertyChanged(CurrentBestFittnessPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Cash" /> property's name.
        /// </summary>
        public const string CashPropertyName = "Cash";

        private int _cash = 1000;

        /// <summary>
        /// Sets and gets the Cash property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int Cash
        {
            get
            {
                return _cash;
            }

            set
            {
                if (_cash == value)
                {
                    return;
                }

                RaisePropertyChanging(CashPropertyName);
                _cash = value;
                sendCashChangedessage();
                RaisePropertyChanged(CashPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="OrderingConditions" /> property's name.
        /// </summary>
        public const string OrderingConditionsPropertyName = "OrderingConditions";

        private ObservableCollection<GeneBase> _orderingConditionds = new ObservableCollection<GeneBase>();

        /// <summary>
        /// Sets and gets the OrderingConditions property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<GeneBase> OrderingConditions
        {
            get
            {
                return _orderingConditionds;
            }

            set
            {
                if (_orderingConditionds == value)
                {
                    return;
                }

                RaisePropertyChanging(OrderingConditionsPropertyName);
                _orderingConditionds = value;
                RaisePropertyChanged(OrderingConditionsPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="SelectedOrderingCondition" /> property's name.
        /// </summary>
        public const string SelectedOrderingConditionPropertyName = "SelectedOrderingCondition";

        private GeneBase _selectedOrderingCondition = null;

        /// <summary>
        /// Sets and gets the SelectedOrderingCondition property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public GeneBase SelectedOrderingCondition
        {
            get
            {
                return _selectedOrderingCondition;
            }

            set
            {
                if (_selectedOrderingCondition == value)
                {
                    return;
                }

                RaisePropertyChanging(SelectedOrderingConditionPropertyName);
                _selectedOrderingCondition = value;
                orderResults();
                RaisePropertyChanged(SelectedOrderingConditionPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="BestCreatures" /> property's name.
        /// </summary>
        public const string BestCreaturesPropertyName = "BestCreatures";

        private ObservableCollection<CreatureViewModel> _bestCreatures = new ObservableCollection<CreatureViewModel>();

        /// <summary>
        /// Sets and gets the BestCreatures property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<CreatureViewModel> BestCreatures
        {
            get
            {
                return _bestCreatures;
            }

            set
            {
                if (_bestCreatures == value)
                {
                    return;
                }

                RaisePropertyChanging(BestCreaturesPropertyName);
                _bestCreatures = value;
                RaisePropertyChanged(BestCreaturesPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="IsBusy" /> property's name.
        /// </summary>
        public const string IsBusyPropertyName = "IsBusy";

        private bool _isBusy = false;

        /// <summary>
        /// Sets and gets the IsBusy property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }

            set
            {
                if (_isBusy == value)
                {
                    return;
                }

                RaisePropertyChanging(IsBusyPropertyName);
                _isBusy = value;
                RaisePropertyChanged(IsBusyPropertyName);
                RaisePropertyChanged(NotBusyPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="NotBusy" /> property's name.
        /// </summary>
        public const string NotBusyPropertyName = "NotBusy";

        /// <summary>
        /// Sets and gets the NotBusy property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool NotBusy
        {
            get
            {
                return !IsBusy;
            }
        }

        /// <summary>
        /// The <see cref="IsBalanced" /> property's name.
        /// </summary>
        public const string IsBalancedPropertyName = "IsBalanced";

        private bool _isBalanced = true;

        /// <summary>
        /// Sets and gets the IsBalanced property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsBalanced
        {
            get
            {
                return _isBalanced;
            }

            set
            {
                if (_isBalanced == value)
                {
                    return;
                }

                RaisePropertyChanging(IsBalancedPropertyName);
                _isBalanced = value;
                RaisePropertyChanged(IsBalancedPropertyName);
            }
        }

        public IFittnessFunction SelectedFittnessFunction
        {
            get { return IsBalanced ? _balancedFittness : _unbalancedFittness; }
        }


        #endregion

        public MainViewModel()
        {
            initialize();
        }

        private void initialize()
        {
            AvailableGenes = new ObservableCollection<GeneBase>(_mainDataService.GetGenes());
            Start = new RelayCommand(start);
            _worker.DoWork += _worker_DoWork;
            _worker.RunWorkerCompleted += _worker_RunWorkerCompleted;
        }

        void _worker_DoWork(object sender, DoWorkEventArgs e)
        {
            CurrentGeneration = 0;
            PrevBestFittness = (from c in Population select c.Fittness).Max();

            while (CurrentGeneration < Generation)
            {
                selection();
                breeding();

                SelectedFittnessFunction.Evaluate(Population);
                var bestCreature = Population.Where(x => x.Fittness == (from c in Population select c.Fittness).Max()).First();
                PrevBestFittness = bestCreature.Fittness;
                CurrentGeneration++;
            }
        }

        private void selection()
        {
            var orderedPopulation = (Population.OrderByDescending(x => x.Fittness)).ToList();
            Population.Clear();
            for (int i = 0; i < orderedPopulation.Count()/2; i++)
            {
                Population.Add(orderedPopulation[i]);
            }
        }

        private void breeding()
        {
            var populationCount = Population.Count();
            for (int i = 0; i < populationCount/2; i++)
            {
                Population.Add(Population[i].breed(Population[populationCount-1-i]));
                Population.Add(Population[populationCount - 1 - i].breed(Population[i]));
            }

            if (populationCount % 2 == 1)
            {
                Population.Add(Population[0].breed(Population[1]));
                Population.Add(Population[1].breed(Population[0]));
            }
        }

        void _worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var creatures = new ObservableCollection<IBreedable>();
            var newCreatures = new ObservableCollection<CreatureViewModel>();
            Population = new ObservableCollection<IBreedable>(Population.OrderByDescending(x => x.Fittness));
            int i = 0;

            while (creatures.Count() < (Population.Count() / 10) && i < Population.Count())
            {
                if (!creatures.Contains(Population[i]))
                {
                    creatures.Add(Population[i]);
                    newCreatures.Add(new CreatureViewModel(Population[i], Cash));
                }

                i++;
            }

            OrderingConditions = new ObservableCollection<GeneBase>(Population[0].Genes);
            SelectedOrderingCondition = null;
            IsBusy = false;

            BestCreatures = new ObservableCollection<CreatureViewModel>(newCreatures);
            RaisePropertyChanged(BestCreaturesPropertyName);
            RaisePropertyChanged(OrderingConditionsPropertyName);

        }

        private void orderResults()
        {
            if ((BestCreatures == null || BestCreatures.Count() < 1) || SelectedOrderingCondition == null)
                return;

            BestCreatures = new ObservableCollection<CreatureViewModel>(
                BestCreatures.OrderByDescending( x => x.RacerDisplays.Where(y => y.Name.Equals(((RacerGene)SelectedOrderingCondition).Racer.Name)).First().Profit));
        }

        private void sendCashChangedessage()
        {
            Messenger.Default.Send(new CashMessage() { Cash = this.Cash });
        }

        private void start()
        {
            if (!IsBusy)
            {
                var selectedGenes = AvailableGenes.Where(x => x.IsSelected);

                if (selectedGenes.Count() > 0)
                {
                    IsBusy = true;
                    initializePopulation(selectedGenes);
                    _worker.RunWorkerAsync();
                }
            }
        }

        private void initializePopulation(IEnumerable<GeneBase> selectedGenes)
        {
            Population = new ObservableCollection<IBreedable>(_mainDataService.GetRandomCreatures(selectedGenes, PopulationCount, Granuality));
            SelectedFittnessFunction.Evaluate(Population);
        }
    }
}