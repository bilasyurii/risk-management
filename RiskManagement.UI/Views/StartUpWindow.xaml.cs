using Microsoft.Win32;
using RiskManagement.UI.ViewModels;
using System.IO;
using System.Windows;

namespace RiskManagement.UI.Views
{
    public partial class StartUpWindow : Window
    {
        private RecentFilesViewModel rfvm;
        public StartUpWindow(RecentFilesViewModel rfvm)
        {
            this.rfvm = rfvm;
            InitializeComponent();
        }

        private void OpenRecentClicked(object sender, RoutedEventArgs e)
        {
            string selected = (string) ProjectsList.SelectedItem;
            if (File.Exists(selected))
            {
                ProjectPath = selected;
                Close();
            }
            else
            {
                rfvm.RecentFiles.RemoveAt(ProjectsList.SelectedIndex);
                ProjectsList.SelectedIndex = -1;
                MessageBox.Show("Обраний проект не знайдено",
                                "Помилка",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error,
                                MessageBoxResult.OK);
            }
        }

        private void ProjectsListSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (ProjectsList.SelectedIndex >= 0)
                OpenRecentButton.IsEnabled = true;
            else
                OpenRecentButton.IsEnabled = false;
        }

        public string ProjectPath { get; set; } = null;

        private void OpenProjectClicked(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;
            openFileDialog.DefaultExt = "dat";
            openFileDialog.Filter = "Project data (*.dat)|*.dat";
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == true)
            {
                ProjectPath = openFileDialog.FileName;
                Close();
            }
        }

        private void onClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(ProjectCreationGrid.Visibility == Visibility.Visible)
            {
                ProjectCreationGrid.Visibility = Visibility.Collapsed;
                ProjectOpeningGrid.Visibility = Visibility.Visible;
                e.Cancel = true;
            }
            else if(ProjectPath == null)
                Application.Current.Shutdown();
        }

        private void CreateNewProjectClicked(object sender, RoutedEventArgs e)
        {
            ProjectOpeningGrid.Visibility = Visibility.Collapsed;
            ProjectCreationGrid.Visibility = Visibility.Visible;
        }

        private void selectFolderClicked(object sender, RoutedEventArgs e)
        {
            var selectFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            if (selectFolderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                rfvm.NewProjectFolder = selectFolderDialog.SelectedPath;
                checkIfCreationAvailable();
            }
        }

        private void checkIfCreationAvailable()
        {
            createProjectButton.IsEnabled = projectNameBox.Text.Length > 0 &&
                                            rfvm.NewProjectFolder != null &&
                                            rfvm.InitialCost != null;
        }

        private void projectNameChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            checkIfCreationAvailable();
        }

        private void initialCostChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            checkIfCreationAvailable();
        }

        private void finishProjectCreation(object sender, RoutedEventArgs e)
        {
            ProjectPath = rfvm.NewProjectFolder + "\\" + rfvm.ProjectName + ".dat";
            Close();
        }
    }
}
