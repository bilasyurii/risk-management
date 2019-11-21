using RiskManagement.Model;
using System;
using System.Collections.ObjectModel;

namespace RiskManagement.UI.ViewModels
{
    public class IterationViewModel : ViewModelBase
    {
        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                onPropertyChanged("Date");
            }
        }
        private ObservableCollection<Risk> _risks;
        public ObservableCollection<Risk> Risks
        {
            get { return _risks; }
            set
            {
                _risks = value;
                onPropertyChanged("Risks");
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
    }
}
