using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiskManagement.UI.ViewModels
{
    public class RiskEventRowViewModel : ViewModelBase
    {
        private int? _rowHeader;
        public int? RowHeader
        {
            get { return _rowHeader; }
            set
            {
                _rowHeader = value;
                onPropertyChanged("RowHeader");
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
        private double? _percent;
        public double? Percent
        {
            get { return _percent; }
            set
            {
                _percent = value;
                onPropertyChanged("Percent");
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
    }
}
