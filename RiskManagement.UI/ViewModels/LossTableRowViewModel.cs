using RiskManagement.Model;
using System.Windows.Media;

namespace RiskManagement.UI.ViewModels
{
    class LossTableRowViewModel : ViewModelBase
    {
        private int? _number;
        public int? Number
        {
            get { return _number; }
            set
            {
                _number = value;
                onPropertyChanged("Number");
            }
        }
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                onPropertyChanged("Name");
            }
        }
        private double? _cost;
        public double? Cost
        {
            get { return _cost; }
            set
            {
                _cost = value;
                onPropertyChanged("Cost");
            }
        }
        private ColumnsGroup _guesses;
        public ColumnsGroup Guesses
        {
            get { return _guesses; }
            set
            {
                _guesses = value;
                onPropertyChanged("Guesses");
            }
        }
        private double? _average;
        public double? Average
        {
            get { return _average; }
            set
            {
                _average = value;
                onPropertyChanged("Average");
            }
        }
        private ColumnsGroup _guessesWeighted;
        public ColumnsGroup GuessesWeighted
        {
            get { return _guessesWeighted; }
            set
            {
                _guessesWeighted = value;
                onPropertyChanged("GuessesWeighted");
            }
        }
        private double? _additionalCost;
        public double? AdditionalCost
        {
            get { return _additionalCost; }
            set
            {
                _additionalCost = value;
                onPropertyChanged("AdditionalCost");
            }
        }
        private string _riskPriority;
        public string RiskPriority
        {
            get { return _riskPriority; }
            set
            {
                _riskPriority = value;
                onPropertyChanged("RiskPriority");
            }
        }
        private bool _isBold;
        public bool IsBold
        {
            get { return _isBold; }
            set
            {
                _isBold = value;
                onPropertyChanged("IsBold");
            }
        }
        private Brush _averageColor;
        public Brush AverageColor
        {
            get { return _averageColor; }
            set
            {
                _averageColor = value;
                onPropertyChanged("AverageColor");
            }
        }
        private Brush _additionalCostColor;
        public Brush AdditionalCostColor
        {
            get { return _additionalCostColor; }
            set
            {
                _additionalCostColor = value;
                onPropertyChanged("AdditionalCostColor");
            }
        }
        private bool _averageTextWhite;
        public bool AverageTextWhite
        {
            get { return _averageTextWhite; }
            set
            {
                _averageTextWhite = value;
                onPropertyChanged("AverageTextWhite");
            }
        }
        private bool _additionalCostTextWhite;
        public bool AdditionalCostTextWhite
        {
            get { return _additionalCostTextWhite; }
            set
            {
                _additionalCostTextWhite = value;
                onPropertyChanged("AdditionalCostTextWhite");
            }
        }
        private double? _finalCost;
        public double? FinalCost
        {
            get { return _finalCost; }
            set
            {
                _finalCost = value;
                onPropertyChanged("FinalCost");
            }
        }
        private string _minMax;
        public string MinMax
        {
            get { return _minMax; }
            set
            {
                _minMax = value;
                onPropertyChanged("MinMax");
            }
        }
        private double? _minMaxValue;
        public double? MinMaxValue
        {
            get { return _minMaxValue; }
            set
            {
                _minMaxValue = value;
                onPropertyChanged("MinMaxValue");
            }
        }
    }
}
