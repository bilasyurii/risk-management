using RiskManagement.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace RiskManagement.UI.ViewModels
{
    public class DataViewModel : ViewModelBase
    {
        public DataViewModel()
        {
            CancelSolution = new Command(PerformCancelSolution);
        }

        public ICommand CancelSolution { get; set; }

        private void PerformCancelSolution(object args)
        {
            CurrentRisk.Solution.SolutionID = null;
        }

        private Project _currentProject;
        public Project CurrentProject
        {
            get { return _currentProject; }
            set
            {
                _currentProject = value;
                onPropertyChanged("CurrentProject");
            }
        }


        private Iteration _currentIteration;
        public Iteration CurrentIteration
        {
            get { return _currentIteration; }
            set
            {
                _currentIteration = value;
                onPropertyChanged("CurrentIteration");
            }
        }

        private Risk _currentRisk;
        public Risk CurrentRisk
        {
            get { return _currentRisk; }
            set
            {
                _currentRisk = value;
                onPropertyChanged("CurrentRisk");
            }
        }

        private ObservableCollection<ExpertGuess> _expertGuesses;
        public ObservableCollection<ExpertGuess> ExpertGuesses
        {
            get { return _expertGuesses; }
            set
            {
                _expertGuesses = value;
                onPropertyChanged("ExpertGuesses");
            }
        }

        private ObservableCollection<ProbabilityTableRow> _probabilityTable;
        public ObservableCollection<ProbabilityTableRow> ProbabilityTable
        {
            get { return _probabilityTable; }
            set
            {
                _probabilityTable = value;
                onPropertyChanged("ProbabilityTable");
            }
        }

        private ObservableCollection<LossTableRow> _lossTable;
        public ObservableCollection<LossTableRow> LossTable
        {
            get { return _lossTable; }
            set
            {
                _lossTable = value;
                onPropertyChanged("LossTable");
            }
        }

        private ObservableCollection<RiskEventRow> _riskEventTable;
        public ObservableCollection<RiskEventRow> RiskEventTable
        {
            get { return _riskEventTable; }
            set
            {
                _riskEventTable = value;
                onPropertyChanged("RiskEventTable");
            }
        }
        private ObservableCollection<ProjectInfoRow> _projectInfoTable;
        public ObservableCollection<ProjectInfoRow> ProjectInfoTable
        {
            get { return _projectInfoTable; }
            set
            {
                _projectInfoTable = value;
                onPropertyChanged("ProjectInfoTable");
            }
        }
        private double _maxData;
        public double MaxData
        {
            get { return _maxData; }
            set
            {
                _maxData = value;
                onPropertyChanged("MaxData");
            }
        }
        private double _minData;
        public double MinData
        {
            get { return _minData; }
            set
            {
                _minData = value;
                onPropertyChanged("MinData");
            }
        }
        private double _mprData;
        public double MprData
        {
            get { return _mprData; }
            set
            {
                _mprData = value;
                onPropertyChanged("MprData");
            }
        }
        private string _firstIntervalData;
        public string FirstIntervalData
        {
            get { return _firstIntervalData; }
            set
            {
                _firstIntervalData = value;
                onPropertyChanged("FirstIntervalData");
            }
        }
        private string _secondIntervalData;
        public string SecondIntervalData
        {
            get { return _secondIntervalData; }
            set
            {
                _secondIntervalData = value;
                onPropertyChanged("SecondIntervalData");
            }
        }
        private string _thirdIntervalData;
        public string ThirdIntervalData
        {
            get { return _thirdIntervalData; }
            set
            {
                _thirdIntervalData = value;
                onPropertyChanged("ThirdIntervalData");
            }
        }
    }
}
