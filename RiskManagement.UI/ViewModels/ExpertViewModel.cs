using System.Collections.ObjectModel;

namespace RiskManagement.UI.ViewModels
{
    public class ExpertViewModel : ViewModelBase
    {
        private ObservableCollection<double> _probabilityWeights;
        public ObservableCollection<double> ProbabilityWeights
        {
            get { return _probabilityWeights; }
            set
            {
                _probabilityWeights = value;
                onPropertyChanged("ProbabilityWeights");
            }
        }
        private ObservableCollection<double> _lossWeights;
        public ObservableCollection<double> LossWeights
        {
            get { return _lossWeights; }
            set
            {
                _lossWeights = value;
                onPropertyChanged("LossWeights");
            }
        }
        private int _number;
        public int Number
        {
            get { return _number; }
            set
            {
                _number = value;
                onPropertyChanged("Number");
            }
        }
        private double _probabilityCat1;
        public double ProbabilityCat1
        {
            get { return _probabilityCat1; }
            set
            {
                _probabilityCat1 = value;
                onPropertyChanged("ProbabilityCat1");
            }
        }
        private double _probabilityCat2;
        public double ProbabilityCat2
        {
            get { return _probabilityCat2; }
            set
            {
                _probabilityCat2 = value;
                onPropertyChanged("ProbabilityCat2");
            }
        }
        private double _probabilityCat3;
        public double ProbabilityCat3
        {
            get { return _probabilityCat3; }
            set
            {
                _probabilityCat3 = value;
                onPropertyChanged("ProbabilityCat3");
            }
        }
        private double _probabilityCat4;
        public double ProbabilityCat4
        {
            get { return _probabilityCat4; }
            set
            {
                _probabilityCat4 = value;
                onPropertyChanged("ProbabilityCat4");
            }
        }
        private double _lossCat1;
        public double LossCat1
        {
            get { return _lossCat1; }
            set
            {
                _lossCat1 = value;
                onPropertyChanged("LossCat1");
            }
        }
        private double _lossCat2;
        public double LossCat2
        {
            get { return _lossCat2; }
            set
            {
                _lossCat2 = value;
                onPropertyChanged("LossCat2");
            }
        }
        private double _lossCat3;
        public double LossCat3
        {
            get { return _lossCat3; }
            set
            {
                _lossCat3 = value;
                onPropertyChanged("LossCat3");
            }
        }
        private double _lossCat4;
        public double LossCat4
        {
            get { return _lossCat4; }
            set
            {
                _lossCat4 = value;
                onPropertyChanged("LossCat4");
            }
        }
    }
}
