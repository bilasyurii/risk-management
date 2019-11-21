using RiskManagement.Model;
using System.Collections.ObjectModel;

namespace RiskManagement.UI.ViewModels
{
    public class RiskViewModel : ViewModelBase
    {
        private int _category;
        public int Category
        {
            get { return _category; }
            set
            {
                _category = value;
                onPropertyChanged("Category");
            }
        }
        private int _riskType;
        public int RiskType
        {
            get { return _riskType; }
            set
            {
                _riskType = value;
                onPropertyChanged("RiskType");
            }
        }
        private ObservableCollection<double> _probabilities;
        public ObservableCollection<double> Probabilities
        {
            get { return _probabilities; }
            set
            {
                _probabilities = value;
                onPropertyChanged("Probabilities");
            }
        }
        private ObservableCollection<double> _losses;
        public ObservableCollection<double> Losses
        {
            get { return _losses; }
            set
            {
                _losses = value;
                onPropertyChanged("Losses");
            }
        }
        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                onPropertyChanged("Description");
            }
        }
        private double _estimatedCost;
        public double EstimatedCost
        {
            get { return _estimatedCost; }
            set
            {
                _estimatedCost = value;
                onPropertyChanged("EstimatedCost");
            }
        }
        private Solution _solution;
        public Solution Solution
        {
            get { return _solution; }
            set
            {
                _solution = value;
                onPropertyChanged("Solution");
            }
        }
    }
}
