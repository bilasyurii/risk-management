using RiskManagement.Model;
using System.Collections.ObjectModel;

namespace RiskManagement.UI.ViewModels
{
    public class ProjectViewModel : ViewModelBase
    {
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

        private ObservableCollection<Iteration> _iterations;
        public ObservableCollection<Iteration> Iterations
        {
            get { return _iterations; }
            set
            {
                _iterations = value;
                onPropertyChanged("Iterations");
            }
        }

        private ObservableCollection<Expert> _expertWeights;
        public ObservableCollection<Expert> ExpertWeights
        {
            get { return _expertWeights; }
            set
            {
                _expertWeights = value;
                onPropertyChanged("ExpertWeights");
            }
        }


        private double _initialCost;
        public double InitialCost
        {
            get { return _initialCost; }
            set
            {
                _initialCost = value;
                onPropertyChanged("InitialCost");
            }
        }
    }
}
