using RiskManagement.Model;
using System.Windows.Media;

namespace RiskManagement.UI.ViewModels
{
    public class ProbabilityTableRowViewModel : ViewModelBase
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
        private double? _averageWeighted;
        public double? AverageWeighted
        {
            get { return _averageWeighted; }
            set
            {
                _averageWeighted = value;
                onPropertyChanged("AverageWeighted");
            }
        }
        private string _probabilityGrade;
        public string ProbabilityGrade
        {
            get { return _probabilityGrade; }
            set
            {
                _probabilityGrade = value;
                onPropertyChanged("ProbabilityGrade");
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
        private Brush _averageWeightedColor;
        public Brush AverageWeightedColor
        {
            get { return _averageWeightedColor; }
            set
            {
                _averageWeightedColor = value;
                onPropertyChanged("AverageWeightedColor");
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
        private bool _averageWeightedTextWhite;
        public bool AverageWeightedTextWhite
        {
            get { return _averageWeightedTextWhite; }
            set
            {
                _averageWeightedTextWhite = value;
                onPropertyChanged("AverageWeightedTextWhite");
            }
        }
    }
}
