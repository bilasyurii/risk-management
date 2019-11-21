using AutoMapper;
using Ninject;
using RiskManagement.Model;
using RiskManagement.UI.ViewModels;
using RiskManagement.UI.Views;
using System.Windows;
using System.Windows.Controls;

namespace RiskManagement.UI
{
    public partial class MainWindow : Window
    {
        public string ProjectPath { get; private set; }
        public MainWindow(RecentFilesViewModel rfvm)
        {
            StartUpWindow startup = new StartUpWindow(rfvm) { DataContext = rfvm };
            startup.ShowDialog();
            ProjectPath = startup.ProjectPath;
            InitializeComponent();
            foreach (var category in Risk.CategoryNames)
                categoryComboBox.Items.Add(new ComboBoxItem() { Content = category });
            categoryComboBox.SelectedIndex = 0;
            riskComboBox.SelectedIndex = 0;
        }

        private void AboutItemClicked(object sender, RoutedEventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }
    }
}
