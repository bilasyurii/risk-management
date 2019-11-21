using AutoMapper;
using Microsoft.Win32;
using Ninject;
using RiskManagement.Model;
using RiskManagement.UI.Base;
using RiskManagement.UI.DI;
using RiskManagement.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace RiskManagement.UI
{
    public partial class App : Application
    {
        DataModel model;
        RecentFilesModel recentFilesModel;
        DataViewModel viewModel;
        RecentFilesViewModel recentFilesViewModel;
        IMapper mapper;
        MainWindow window;
        MenuItem risksBoxContextItem;

        public App()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            kernel.Bind<IMapper>().ToProvider(new FakeMapperProvider()).InSingletonScope().Named("fake");
            kernel.Bind<IMapper>().ToProvider(new MapperProvider()).InSingletonScope().Named("real");
            mapper = kernel.Get<IMapper>("real");
            recentFilesModel = RecentFilesModel.Load();
            recentFilesViewModel = mapper.Map<RecentFilesModel, RecentFilesViewModel>(recentFilesModel);
            kernel.Bind<MainWindow>().ToSelf()
                .InSingletonScope()
                .WithConstructorArgument("rfvm", recentFilesViewModel)
                .WithPropertyValue("DataContext", viewModel);
            window = kernel.Get<MainWindow>();
            string projectPath = window.ProjectPath;
            int projectIndexInList = recentFilesViewModel.RecentFiles.IndexOf(projectPath);
            if (projectIndexInList >= 0)
                recentFilesViewModel.RecentFiles.RemoveAt(projectIndexInList);
            recentFilesViewModel.RecentFiles.Insert(0, projectPath);
            recentFilesModel = mapper.Map<RecentFilesViewModel, RecentFilesModel>(recentFilesViewModel);
            DataModel.DataPath = projectPath;
            recentFilesModel.Save(RecentFilesModel.RecentFilesPath);
            if (recentFilesModel.NewProjectFolder == null)
                model = DataModel.Load();
            else
                model = DataModel.Create(recentFilesModel);
            viewModel = mapper.Map<DataModel, DataViewModel>(model);
            window.DataContext = viewModel;
            window.risksBox.ItemsSource = viewModel.CurrentIteration.Risks;
            window.risksBox.SelectionChanged += SelectedRisk;
            window.addRiskButton.Click += AddRiskClicked;
            window.Closing += onClosing;
            window.MainTabs.SelectionChanged += TabChanged;
            window.resultTabs.SelectionChanged += TabChanged;
            window.DeleteRiskItem.Click += DeleteRiskClicked;
            window.OpenItem.Click += OpenClicked;
            window.SaveItem.Click += SaveClicked;
            window.SaveAsItem.Click += SaveAsClicked;
            window.ExitItem.Click += ExitClicked;
            window.CreateIteration.Click += CreateIterationClicked;
            window.DeleteIteration.Click += DeleteIterationClicked;
            window.OpenPreviousIteration.Click += OpenPreviousIterationClicked;
            window.OpenNextIteration.Click += OpenNextIterationClicked;
            window.categoryComboBox.SelectionChanged += CategoryChanged;
            window.cancelSolution.Click += solutionCanceled;
            ContextMenu risksBoxContextMenu = new ContextMenu();
            risksBoxContextItem = new MenuItem();
            risksBoxContextItem.IsEnabled = false;
            risksBoxContextItem.Header = "Видалити";
            risksBoxContextItem.Click += RiskDeleteClicked;
            risksBoxContextMenu.Items.Add(risksBoxContextItem);
            window.ContextMenu = risksBoxContextMenu;
            checkIfIterationButtonsAvailable();
            checkIfDeletingRiskAvailable();
            CategoryChanged(null, null);
            window.Show();
        }

        private void RiskDeleteClicked(object sender, RoutedEventArgs e)
        {
            if (viewModel.CurrentRisk != null)
            {
                viewModel.CurrentIteration.Risks.Remove(viewModel.CurrentRisk);
                viewModel.CurrentRisk = null;
                var selection = window.risksBox.SelectedIndex;
                window.risksBox.Items.Refresh();
                window.risksBox.SelectedIndex = Math.Min(selection,
                                                         viewModel.CurrentIteration.Risks.Count - 1);
                checkIfDeletingRiskAvailable();
                CategoryChanged(null, null);
            }
        }

        private void solutionCanceled(object sender, RoutedEventArgs e)
        {
            window.solutionComboBox.SelectedIndex = -1;
        }

        private void checkIfDeletingRiskAvailable()
        {
            bool available = viewModel.CurrentRisk != null;
            window.DeleteRiskItem.IsEnabled = available;
            risksBoxContextItem.IsEnabled = available;
        }

        private void checkIfIterationButtonsAvailable()
        {
            window.DeleteIteration.IsEnabled = false;
            window.OpenPreviousIteration.IsEnabled = false;
            window.OpenNextIteration.IsEnabled = false;
            if (viewModel.CurrentProject.Iterations.Count > 1)
                window.DeleteIteration.IsEnabled = true;
            var index = viewModel.CurrentProject.Iterations.IndexOf(viewModel.CurrentIteration);
            if (index > 0)
                window.OpenPreviousIteration.IsEnabled = true;
            if (index < viewModel.CurrentProject.Iterations.Count - 1)
                window.OpenNextIteration.IsEnabled = true;
        }

        private void OpenNextIterationClicked(object sender, RoutedEventArgs e)
        {
            var index = viewModel.CurrentProject.Iterations.IndexOf(viewModel.CurrentIteration);
            if (index >= viewModel.CurrentProject.Iterations.Count - 1)
                return;
            viewModel.CurrentIteration = viewModel.CurrentProject.Iterations[index + 1];
            viewModel.CurrentRisk = null;
            window.risksBox.ItemsSource = viewModel.CurrentIteration.Risks;
            window.risksBox.SelectedIndex = -1;
            window.risksBox.Items.Refresh();
            checkIfIterationButtonsAvailable();
            CategoryChanged(null, null);
        }

        private void OpenPreviousIterationClicked(object sender, RoutedEventArgs e)
        {
            var index = viewModel.CurrentProject.Iterations.IndexOf(viewModel.CurrentIteration);
            if (index == 0)
                return;
            viewModel.CurrentIteration = viewModel.CurrentProject.Iterations[index - 1];
            viewModel.CurrentRisk = null;
            window.risksBox.ItemsSource = viewModel.CurrentIteration.Risks;
            window.risksBox.SelectedIndex = -1;
            window.risksBox.Items.Refresh();
            checkIfIterationButtonsAvailable();
            CategoryChanged(null, null);
        }

        private void DeleteIterationClicked(object sender, RoutedEventArgs e)
        {
            if (viewModel.CurrentProject.Iterations.Count < 2)
                return;
            viewModel.CurrentProject.Iterations.Remove(viewModel.CurrentIteration);
            viewModel.CurrentIteration = viewModel.CurrentProject.Iterations[viewModel.CurrentProject.Iterations.Count - 1];
            viewModel.CurrentRisk = null;
            window.risksBox.ItemsSource = viewModel.CurrentIteration.Risks;
            window.risksBox.SelectedIndex = -1;
            window.risksBox.Items.Refresh();
            checkIfIterationButtonsAvailable();
            CategoryChanged(null, null);
        }

        private void OpenClicked(object sender, RoutedEventArgs e)
        {
            var msgBoxResult = MessageBox.Show("Зберегти відкритий проект?",
                                               "Відкриття нового проекту", 
                                               MessageBoxButton.YesNoCancel, 
                                               MessageBoxImage.Question, 
                                               MessageBoxResult.OK);
            switch(msgBoxResult)
            {
                case MessageBoxResult.Yes:
                    SaveClicked(null, null);
                    break;
                case MessageBoxResult.No:
                    break;
                case MessageBoxResult.Cancel:
                    return;
                default:
                    throw new InvalidOperationException("Error while opening new project");
            }
            var openFileDialog = new OpenFileDialog();
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;
            openFileDialog.DefaultExt = "dat";
            openFileDialog.Filter = "Project data (*.dat)|*.dat";
            openFileDialog.RestoreDirectory = true;
            if(openFileDialog.ShowDialog() == true)
            {
                DataModel.DataPath = openFileDialog.FileName;
                model = DataModel.Load();
                DataModel.DataPath = openFileDialog.FileName;
                viewModel = mapper.Map<DataModel, DataViewModel>(model);
                window.DataContext = viewModel;
                window.risksBox.ItemsSource = viewModel.CurrentIteration.Risks;
                window.risksBox.SelectedIndex = -1;
                window.risksBox.Items.Refresh();
                int projectIndexInList = recentFilesViewModel.RecentFiles.IndexOf(DataModel.DataPath);
                if (projectIndexInList >= 0)
                    recentFilesViewModel.RecentFiles.RemoveAt(projectIndexInList);
                recentFilesViewModel.RecentFiles.Insert(0, DataModel.DataPath);
                recentFilesModel = mapper.Map<RecentFilesViewModel, RecentFilesModel>(recentFilesViewModel);
                recentFilesModel.Save(RecentFilesModel.RecentFilesPath);
                CategoryChanged(null, null);
            }
        }

        private void SaveClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                model = mapper.Map<DataViewModel, DataModel>(viewModel);
                model.Save(DataModel.DataPath);
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Error while saving file");
            }
        }

        private void CreateIterationClicked(object sender, RoutedEventArgs e)
        {
            Iteration iteration = new Iteration()
            {
                Number = viewModel.CurrentProject.Iterations[viewModel.CurrentProject.Iterations.Count - 1].Number + 1,
                Date = DateTime.Today,
                Risks = new List<Risk>()
            };
            viewModel.CurrentProject.Iterations.Add(iteration);
            viewModel.CurrentIteration = iteration;
            viewModel.CurrentRisk = null;
            window.risksBox.ItemsSource = viewModel.CurrentIteration.Risks;
            window.risksBox.SelectedIndex = -1;
            window.risksBox.Items.Refresh();
            checkIfIterationButtonsAvailable();
            CategoryChanged(null, null);
        }

        private void SaveAsClicked(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.AddExtension = true;
            saveFileDialog.DefaultExt = "dat";
            saveFileDialog.Filter = "Project data (*.dat)|*.dat";
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() == true)
            {
                model = mapper.Map<DataViewModel, DataModel>(viewModel);
                model.Save(saveFileDialog.FileName);
            }
        }

        private void ExitClicked(object sender, RoutedEventArgs e)
        {
            window.Close();
        }

        private static DoubleAnimation fadeIn = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(500));
        private static DoubleAnimation fadeOut = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(500));

        private void SelectedRisk(object sender, SelectionChangedEventArgs e)
        {
            var list = (sender as ListBox);
            if (list.SelectedIndex >= 0)
            {
                if(window.RiskEditGrid.Visibility != Visibility.Visible)
                {
                    window.RiskEditGrid.Visibility = Visibility.Visible;
                    window.RiskEditGrid.BeginAnimation(UIElement.OpacityProperty, fadeIn);
                    window.PleaseSelectRiskText.BeginAnimation(UIElement.OpacityProperty, fadeOut);
                }
                viewModel.CurrentRisk = viewModel.CurrentIteration.Risks[list.SelectedIndex];
                for(int i = 0; i < 10; ++i)
                    viewModel.ExpertGuesses[i] = new ExpertGuess(viewModel.CurrentRisk, 
                                                                 viewModel.CurrentProject.ExpertWeights[i], i);
            }
            else
            {
                window.RiskEditGrid.Visibility = Visibility.Hidden;
                window.RiskEditGrid.BeginAnimation(UIElement.OpacityProperty, fadeOut);
                window.PleaseSelectRiskText.BeginAnimation(UIElement.OpacityProperty, fadeIn);
            }
            checkIfDeletingRiskAvailable();
        }

        private void AddRiskClicked(object sender, RoutedEventArgs e)
        {
            if (window.riskComboBox.Items.Count <= 0)
                return;
            Risk newRisk = new Risk()
            {
                Category = window.categoryComboBox.SelectedIndex,
                Description = "",
                Losses = new double[10],
                Probabilities = new double[10],
                Solution = new Solution()
            };
            newRisk.RiskType = Array.IndexOf(Risk.RiskNames[newRisk.Category],
                                             ((ComboBoxItem)window.riskComboBox.SelectedItem).Content);
            viewModel.CurrentIteration.Risks.Add(newRisk);
            window.risksBox.SelectedIndex = window.risksBox.Items.Count - 1;
            window.risksBox.Items.Refresh();
            checkIfDeletingRiskAvailable();
            CategoryChanged(null, null);
        }

        private void CategoryChanged(object sender, EventArgs e)
        {
            window.riskComboBox.Items.Clear();
            foreach (var riskName in Risk.RiskNames[window.categoryComboBox.SelectedIndex])
                if (!viewModel.CurrentIteration.Risks.Exists(r => r.RiskName == riskName))
                    window.riskComboBox.Items.Add(new ComboBoxItem() { Content = riskName });
            if (window.riskComboBox.Items.Count <= 0)
                window.addRiskButton.IsEnabled = false;
            else
                window.addRiskButton.IsEnabled = true;
            window.riskComboBox.SelectedIndex = 0;
        }

        private void DeleteRiskClicked(object sender, RoutedEventArgs e)
        {
            if(viewModel.CurrentRisk != null)
            {
                viewModel.CurrentIteration.Risks.Remove(viewModel.CurrentRisk);
                viewModel.CurrentRisk = null;
                var selection = window.risksBox.SelectedIndex;
                window.risksBox.Items.Refresh();
                window.risksBox.SelectedIndex = Math.Min(selection, 
                                                         viewModel.CurrentIteration.Risks.Count - 1);
                checkIfDeletingRiskAvailable();
                CategoryChanged(null, null);
            }
        }

        private void TabChanged(object sender, SelectionChangedEventArgs e)
        {
            if (window.MainTabs.SelectedIndex == 1)
            {
                if (window.resultTabs.SelectedIndex == 0)
                {
                    model.updateRiskEventTable();
                    window.riskEvents.ItemsSource = model.RiskEventTable;
                    window.riskEvents.Items.Refresh();
                }
                else if (window.resultTabs.SelectedIndex == 1)
                {
                    model.updateResultsTable();
                    window.resultDataGrid.ItemsSource = model.ProbabilityTable;
                    window.resultDataGrid.Items.Refresh();
                }
                else if(window.resultTabs.SelectedIndex == 2)
                {
                    model.updateLossTable();
                    window.lossDataGrid.ItemsSource = model.LossTable;
                    window.lossDataGrid.Items.Refresh();
                    viewModel.MaxData = model.MaxData;
                    viewModel.MinData = model.MinData;
                    viewModel.MprData = model.MprData;
                    viewModel.FirstIntervalData = model.FirstIntervalData;
                    viewModel.SecondIntervalData = model.SecondIntervalData;
                    viewModel.ThirdIntervalData = model.ThirdIntervalData;
                }
                else if(window.resultTabs.SelectedIndex == 3)
                {
                    model.updateProjectInfoTable();
                    window.projectDataGrid.ItemsSource = model.ProjectInfoTable;
                    window.projectDataGrid.Items.Refresh();
                }
            }
        }

        protected void onClosing(object sender, CancelEventArgs e)
        {
            if(DataModel.DataPath != null && DataModel.DataPath != "")
            {
                var msgBoxResult = MessageBox.Show("Бажаєте зберегти файл перед виходом?",
                                                   "Вихід",
                                                   MessageBoxButton.YesNoCancel,
                                                   MessageBoxImage.Question,
                                                   MessageBoxResult.OK);
                switch (msgBoxResult)
                {
                    case MessageBoxResult.Yes:
                        SaveClicked(null, null);
                        break;
                    case MessageBoxResult.No:
                        break;
                    case MessageBoxResult.Cancel:
                        e.Cancel = true;
                        break;
                    default:
                        throw new InvalidOperationException("Error while closing project");
                }
            }
        }
    }
}
