namespace RiskManagement.UI.ViewModels
{
    public class ProjectInfoRowViewModel : ViewModelBase
    {
        private string _text;
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                onPropertyChanged("Text");
            }
        }
        private double _overal;
        public double Overal
        {
            get { return _overal; }
            set
            {
                _overal = value;
                onPropertyChanged("Overal");
            }
        }
        private double _first;
        public double First
        {
            get { return _first; }
            set
            {
                _first = value;
                onPropertyChanged("First");
            }
        }
        private double _second;
        public double Second
        {
            get { return _second; }
            set
            {
                _second = value;
                onPropertyChanged("Second");
            }
        }
        private double _third;
        public double Third
        {
            get { return _third; }
            set
            {
                _third = value;
                onPropertyChanged("Third");
            }
        }
        private double _fourth;
        public double Fourth
        {
            get { return _fourth; }
            set
            {
                _fourth = value;
                onPropertyChanged("Fourth");
            }
        }
        private bool _oneDigit;
        public bool OneDigit
        {
            get { return _oneDigit; }
            set
            {
                _oneDigit = value;
                onPropertyChanged("OneDigit");
            }
        }
        private string _costCoef;
        public string CostCoef
        {
            get { return _costCoef; }
            set
            {
                _costCoef = value;
                onPropertyChanged("CostCoef");
            }
        }
    }
}
