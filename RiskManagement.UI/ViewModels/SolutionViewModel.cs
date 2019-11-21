namespace RiskManagement.UI.ViewModels
{
    class SolutionViewModel : ViewModelBase
    {
        private int? _solutionID;
        public int? SolutionID
        {
            get { return _solutionID; }
            set
            {
                _solutionID = value;
                onPropertyChanged("SolutionID");
            }
        }
    }
}
