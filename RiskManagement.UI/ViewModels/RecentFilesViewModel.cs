using RiskManagement.Model;
using System;
using System.Collections.ObjectModel;

namespace RiskManagement.UI.ViewModels
{
    public class RecentFilesViewModel : ViewModelBase
    {
        private ObservableCollection<string> _recentFiles;
        public ObservableCollection<string> RecentFiles
        {
            get { return _recentFiles; }
            set
            {
                _recentFiles = value;
                onPropertyChanged("RecentFiles");
            }
        }
        private ObservableCollection<Expert> _experts;
        public ObservableCollection<Expert> Experts
        {
            get { return _experts; }
            set
            {
                _experts = value;
                onPropertyChanged("Experts");
            }
        }
        private string _projectName;
        public string ProjectName
        {
            get { return _projectName; }
            set
            {
                _projectName = value;
                onPropertyChanged("ProjectName");
            }
        }
        private string _newProjectFolder;
        public string NewProjectFolder
        {
            get { return _newProjectFolder; }
            set
            {
                _newProjectFolder = value;
                onPropertyChanged("NewProjectFolder");
            }
        }
        private double? _initialCost;
        public double? InitialCost
        {
            get { return _initialCost; }
            set
            {
                if (value == null)
                    _initialCost = null;
                else
                    _initialCost = Math.Max(value.Value, 1.0);
                onPropertyChanged("InitialCost");
            }
        }
    }
}
