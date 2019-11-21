using RiskManagement.Model.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Windows.Media;

namespace RiskManagement.Model
{
    [DataContract]
    public class DataModel
    {
        [DataMember]
        public Project CurrentProject { get; set; }
        public Iteration CurrentIteration { get; set; }
        public Risk CurrentRisk { get; set; }
        public ExpertGuess[] ExpertGuesses { get; set; }
        public List<ProbabilityTableRow> ProbabilityTable { get; set; }
        public List<LossTableRow> LossTable { get; set; }
        public List<RiskEventRow> RiskEventTable { get; set; }
        public List<ProjectInfoRow> ProjectInfoTable { get; set; }
        public double MaxData { get; set; }
        public double MinData { get; set; }
        public double MprData { get; set; }
        public string FirstIntervalData { get; set; }
        public string SecondIntervalData { get; set; }
        public string ThirdIntervalData { get; set; }
        public DataModel()
        {
            //TODO remove this cause this is for test only
            /*var iterationList = new List<Iteration>();
            CurrentProject = new Project()
            {
                Name = "Sample project",
                Iterations = iterationList,
                ExpertWeights = new Expert[10]
                {
                    new Expert() { ProbabilityWeights = new double[4] { 2, 6, 2, 2 },
                                   LossWeights        = new double[4] { 3, 7, 2, 4 } },
                    new Expert() { ProbabilityWeights = new double[4] { 4, 8, 9, 10 },
                                   LossWeights        = new double[4] { 5, 3, 1, 7 } },
                    new Expert() { ProbabilityWeights = new double[4] { 5, 9, 2, 5 },
                                   LossWeights        = new double[4] { 9, 5, 7, 7 } },
                    new Expert() { ProbabilityWeights = new double[4] { 1, 9, 4, 1 },
                                   LossWeights        = new double[4] { 5, 5, 10, 2 } },
                    new Expert() { ProbabilityWeights = new double[4] { 7, 2, 10, 8 },
                                   LossWeights        = new double[4] { 3, 8, 3, 6 } },
                    new Expert() { ProbabilityWeights = new double[4] { 10, 4, 5, 9 },
                                   LossWeights        = new double[4] { 5, 5, 10, 10 } },
                    new Expert() { ProbabilityWeights = new double[4] { 6, 10, 1, 9 },
                                   LossWeights        = new double[4] { 8, 6, 1, 6 } },
                    new Expert() { ProbabilityWeights = new double[4] { 8, 5, 8, 7 },
                                   LossWeights        = new double[4] { 7, 9, 3, 5 } },
                    new Expert() { ProbabilityWeights = new double[4] { 9, 1, 9, 9 },
                                   LossWeights        = new double[4] { 4, 10, 10, 8 } },
                    new Expert() { ProbabilityWeights = new double[4] { 9, 7, 9, 9 },
                                   LossWeights        = new double[4] { 6, 8, 2, 2 } }
                },
                InitialCost = 140000.0
            };
            var risksList = new List<Risk>();
            iterationList.Add(new Iteration()
            {
                Date = new DateTime(2019, 10, 9),
                Risks = risksList
            });
            risksList.Add(new Risk()
            {
                Category = 2,
                RiskType = 3,
                Probabilities = new double[10] { 0.4, 0.6, 0.1, 0.15, 0.25, 0.1, 0.33, 0.5, 0.2, 0.3 },
                Losses = new double[10] { 0.5, 0.3, 0.1, 0.4, 0.25, 0.33, 0.6, 0.1, 0.2, 0.15 },
                Description = "Hey this is my first risk!",
                EstimatedCost = 20000,
                Solution = new Solution()
            });
            */
        }

        public static string DataPath { get; set; } = null;
        public static readonly Color[] ExpertColors = {
            Color.FromRgb(255, 255, 0),
            Color.FromRgb(255, 255, 153),
            Color.FromRgb(204, 255, 204),
            Color.FromRgb(204, 255, 255),
            Color.FromRgb(255, 204, 153),
            Color.FromRgb(255, 204, 0),
            Color.FromRgb(255, 153, 0),
            Color.FromRgb(255, 102, 0)
        };

        public static DataModel Load()
        {
            DataModel model;
            if (File.Exists(DataPath))
                 model = DataSerializer.DeserializeData<DataModel>(DataPath);
            else
            {
                model = new DataModel();
                model.CurrentProject = new Project();
                model.CurrentProject.Iterations = new List<Iteration>();
                model.CurrentProject.Iterations.Add(new Iteration()
                {
                    Date = DateTime.Today,
                    Risks = new List<Risk>()
                });
            }
            ConfigureModel(model);
            return model;
        }

        public static void ConfigureModel(DataModel model)
        {
            model.CurrentIteration = model.CurrentProject.Iterations[model.CurrentProject.Iterations.Count - 1];
            model.CurrentRisk = null;
            model.ExpertGuesses = new ExpertGuess[10];
            for (int i = 0; i < 10; ++i)
                model.ExpertGuesses[i] = new ExpertGuess();
            model.ProbabilityTable = new List<ProbabilityTableRow>();
            model.LossTable = new List<LossTableRow>();
            model.RiskEventTable = new List<RiskEventRow>();
            model.ProjectInfoTable = new List<ProjectInfoRow>();
        }

        public static DataModel Create(RecentFilesModel rfm)
        {
            DataModel model = new DataModel();
            model.CurrentProject = new Project()
            {
                Name = rfm.ProjectName,
                InitialCost = rfm.InitialCost.Value,
                Iterations = new List<Iteration>(),
                ExpertWeights = rfm.Experts
            };
            model.CurrentProject.Iterations.Add(new Iteration()
            {
                Number = 0,
                Date = DateTime.Today,
                Risks = new List<Risk>()
            });
            ConfigureModel(model);
            model.Save(DataPath);
            return model;
        }

        private static double sum(ColumnsGroupItem[] collection)
        {
            double sum = 0.0;
            foreach(var item in collection)
                sum += (double)item.Value;
            return sum;
        }

        private static string getNominalRiskProbability(double probability)
        {
            if (probability < 0.1)
                return "Дуже низька";
            else if (probability < 0.25)
                return "Низька";
            else if (probability < 0.5)
                return "Середня";
            else if (probability < 0.75)
                return "Висока";
            else
                return "Дуже висока";
        }

        public void updateResultsTable()
        {
            List<ProbabilityTableRow> tempProbabilityTable = new List<ProbabilityTableRow>();
            tempProbabilityTable.Add(new ProbabilityTableRow()
            {
                Guesses = new ColumnsGroup("Оцінки експертів"),
                GuessesWeighted = new ColumnsGroup("Оцінки експертів з урахуванням вагомості")
            });
            tempProbabilityTable.Add(new ProbabilityTableRow()
            {
                Guesses = new ColumnsGroup(),
                GuessesWeighted = new ColumnsGroup()
            });
            tempProbabilityTable.Add(new ProbabilityTableRow()
            {
                Guesses = new ColumnsGroup("Коефіцієнти вагомості експертів"),
                GuessesWeighted = new ColumnsGroup("")
            });
            var parts = new List<Risk>[4];
            for (int i = 0; i < 4; ++i)
                parts[i] = new List<Risk>();
            foreach (Risk risk in CurrentIteration.Risks)
                parts[risk.Category].Add(risk);
            Risk temp;
            double WeightsSum;
            ColumnsGroup group;
            double[] weightedGuessesPerExpertSums = new double[10];
            double averageRiskProbabilityPerCat, avgWeighted;
            ProbabilityTableRow categoryRow;
            for(int cat = 0; cat < 4; ++cat)
            {
                if (parts[cat].Count == 0)
                    continue;
                WeightsSum = CurrentProject.getProbabilityWeightsSum(cat);
                categoryRow = new ProbabilityTableRow()
                {
                    Number = cat + 1,
                    Name = Risk.FullCategoryNames[cat],
                    Guesses = new ColumnsGroup(CurrentProject.ExpertWeights, cat, true, new SolidColorBrush(ExpertColors[cat])),
                    IsBold = true,
                    Average = WeightsSum,
                    AverageColor = new SolidColorBrush(Color.FromRgb(0, 128, 128)),
                    AverageWeightedColor = new SolidColorBrush(Color.FromRgb(51, 153, 102)),
                    AverageTextWhite = true,
                    AverageWeightedTextWhite = true,
                };
                tempProbabilityTable.Add(categoryRow);
                Array.Clear(weightedGuessesPerExpertSums, 0, 10);
                averageRiskProbabilityPerCat = 0.0;
                for (int risk = 0; risk < parts[cat].Count; ++risk)
                {
                    temp = parts[cat][risk];
                    group = new ColumnsGroup(temp, CurrentProject.ExpertWeights, true, true);
                    for(int expert = 0; expert < 10; ++expert)
                        weightedGuessesPerExpertSums[expert] += (double)((ColumnsGroupItem[])group.Collection)[expert].Value;
                    avgWeighted = sum((ColumnsGroupItem[])group.Collection) / WeightsSum;
                    averageRiskProbabilityPerCat += avgWeighted;
                    tempProbabilityTable.Add(new ProbabilityTableRow()
                    {
                        Number = risk + 1,
                        Name = temp.RiskName,
                        Guesses = new ColumnsGroup(temp, CurrentProject.ExpertWeights, true, false),
                        GuessesWeighted = group,
                        Average = temp.AverageProbability,
                        AverageWeighted = avgWeighted,
                        AverageColor = new SolidColorBrush(Color.FromRgb(204, 255, 255)),
                        AverageWeightedColor = new SolidColorBrush(Color.FromRgb(204, 255, 255)),
                        ProbabilityGrade = getNominalRiskProbability(avgWeighted)
                    });
                }
                for(int expert = 0; expert < 10; ++expert)
                    weightedGuessesPerExpertSums[expert] /= (parts[cat].Count * CurrentProject.ExpertWeights[expert].ProbabilityWeights[cat]);
                categoryRow.GuessesWeighted = new ColumnsGroup(weightedGuessesPerExpertSums, new SolidColorBrush(Color.FromRgb(204, 255, 204)));
                averageRiskProbabilityPerCat /= parts[cat].Count;
                categoryRow.AverageWeighted = averageRiskProbabilityPerCat;
                categoryRow.ProbabilityGrade = getNominalRiskProbability(averageRiskProbabilityPerCat);
            }
            ProbabilityTable = tempProbabilityTable;
        }

        public static string getRiskPriority(double cost, double min, double max)
        {
            double inverval = (max - min) / 3;
            if (cost < min + inverval)
                return "НИЗЬКИЙ";
            else if (cost < max - inverval)
                return "СЕРЕДНІЙ";
            return "ВИСОКИЙ";
        }
        public double[] calculateFinalPrices()
        {
            double[] finalPrices = new double[4];
            var parts = new List<Risk>[4];
            for (int i = 0; i < 4; ++i)
                parts[i] = new List<Risk>();
            foreach (Risk risk in CurrentIteration.Risks)
                parts[risk.Category].Add(risk);
            Risk temp;
            ColumnsGroup group;
            double WeightsSum, avgWeighted, additional;
            for (int cat = 0; cat < 4; ++cat)
            {
                if (parts[cat].Count == 0)
                    continue;
                WeightsSum = CurrentProject.getLossWeightsSum(cat);
                for (int risk = 0; risk < parts[cat].Count; ++risk)
                {
                    temp = parts[cat][risk];
                    group = new ColumnsGroup(temp, CurrentProject.ExpertWeights, false, true);
                    avgWeighted = sum((ColumnsGroupItem[])group.Collection) / WeightsSum;
                    additional = avgWeighted * temp.EstimatedCost;
                    finalPrices[cat] += temp.EstimatedCost + additional;
                }
            }
            return finalPrices;
        }

        public void updateLossTable()
        {
            List<LossTableRow> tempLossTable = new List<LossTableRow>();
            tempLossTable.Add(new LossTableRow()
            {
                Guesses = new ColumnsGroup("Оцінки експертів"),
                GuessesWeighted = new ColumnsGroup("Оцінки експертів з урахуванням вагомості")
            });
            tempLossTable.Add(new LossTableRow()
            {
                Guesses = new ColumnsGroup(),
                GuessesWeighted = new ColumnsGroup()
            });
            tempLossTable.Add(new LossTableRow()
            {
                Guesses = new ColumnsGroup("Коефіцієнти вагомості експертів"),
                GuessesWeighted = new ColumnsGroup("")
            });
            int index = 2;
            var parts = new List<Risk>[4];
            for (int i = 0; i < 4; ++i)
                parts[i] = new List<Risk>();
            foreach (Risk risk in CurrentIteration.Risks)
                parts[risk.Category].Add(risk);
            Risk temp;
            ColumnsGroup group;
            double[] weightedGuessesPerExpertSums = new double[10];
            double WeightsSum, avgWeighted,
                   additionalSum, additional,
                   minCost = double.MaxValue, maxCost = double.MinValue,
                   minCostPerGroup, maxCostPerGroup;
            LossTableRow categoryRow;
            for (int cat = 0; cat < 4; ++cat)
            {
                if (parts[cat].Count == 0)
                    continue;
                minCostPerGroup = double.MaxValue;
                maxCostPerGroup = double.MinValue;
                WeightsSum = CurrentProject.getLossWeightsSum(cat);
                categoryRow = new LossTableRow()
                {
                    Number = cat + 1,
                    Name = Risk.FullCategoryNames[cat],
                    Cost = CurrentProject[cat],
                    Guesses = new ColumnsGroup(CurrentProject.ExpertWeights, cat, false, new SolidColorBrush(ExpertColors[cat + 4])),
                    IsBold = true,
                    Average = WeightsSum,
                    AverageColor = new SolidColorBrush(Color.FromRgb(0, 128, 128)),
                    AdditionalCostColor = new SolidColorBrush(Color.FromRgb(51, 153, 102)),
                    AverageTextWhite = true,
                    AdditionalCostTextWhite = true,
                };
                tempLossTable.Add(categoryRow);
                ++index;
                Array.Clear(weightedGuessesPerExpertSums, 0, 10);
                additionalSum = 0.0;
                for (int risk = 0; risk < parts[cat].Count; ++risk)
                {
                    temp = parts[cat][risk];
                    group = new ColumnsGroup(temp, CurrentProject.ExpertWeights, false, true);
                    for (int expert = 0; expert < 10; ++expert)
                        weightedGuessesPerExpertSums[expert] += (double)((ColumnsGroupItem[])group.Collection)[expert].Value;
                    avgWeighted = sum((ColumnsGroupItem[])group.Collection) / WeightsSum;
                    additional = avgWeighted * temp.EstimatedCost;
                    if (additional < minCostPerGroup)
                        minCostPerGroup = additional;
                    if (additional > maxCostPerGroup)
                        maxCostPerGroup = additional;
                    additionalSum += additional;
                    tempLossTable.Add(new LossTableRow()
                    {
                        Number = risk + 1,
                        Name = temp.RiskName,
                        Cost = temp.EstimatedCost,
                        Guesses = new ColumnsGroup(temp, CurrentProject.ExpertWeights, false, false),
                        GuessesWeighted = group,
                        Average = temp.AverageLoss * temp.EstimatedCost,
                        AdditionalCost = additional,
                        AverageColor = new SolidColorBrush(Color.FromRgb(204, 255, 255)),
                        AdditionalCostColor = new SolidColorBrush(Color.FromRgb(204, 255, 255)),
                        FinalCost = temp.EstimatedCost + additional
                    });
                    ++index;
                }
                for (int expert = 0; expert < 10; ++expert)
                    weightedGuessesPerExpertSums[expert] /= (parts[cat].Count * CurrentProject.ExpertWeights[expert].LossWeights[cat]);
                categoryRow.GuessesWeighted = new ColumnsGroup(weightedGuessesPerExpertSums, new SolidColorBrush(Color.FromRgb(204, 255, 204)));
                categoryRow.AdditionalCost = additionalSum;
                categoryRow.FinalCost = additionalSum + categoryRow.Cost;
                tempLossTable[index].MinMax = "Max=";
                tempLossTable[index].MinMaxValue = maxCostPerGroup;
                tempLossTable[index - 1].MinMax = "Min=";
                tempLossTable[index - 1].MinMaxValue = minCostPerGroup;
                if (minCostPerGroup < minCost)
                    minCost = minCostPerGroup;
                if (maxCostPerGroup > maxCost)
                    maxCost = maxCostPerGroup;
            }
            foreach (var row in tempLossTable)
                if(!row.IsBold && row.Number != null)
                    row.RiskPriority = getRiskPriority(row.AdditionalCost.Value, minCost, maxCost);
            MaxData = maxCost;
            MinData = minCost;
            MprData = (maxCost - minCost) / 3;
            FirstIntervalData = "1-й інтервал: [" + MinData.ToString("F2", CultureInfo.InvariantCulture) + " ; " + (MinData + MprData).ToString("F2", CultureInfo.InvariantCulture) + ")";
            SecondIntervalData = "2-й інтервал: [" + (MinData + MprData).ToString("F2", CultureInfo.InvariantCulture) + " ; " + (MinData + MprData * 2).ToString("F2", CultureInfo.InvariantCulture)  + ")";
            ThirdIntervalData = "3-й інтервал: [" + (MinData + MprData * 2).ToString("F2", CultureInfo.InvariantCulture)  + " ; " + MaxData.ToString("F2", CultureInfo.InvariantCulture) + "]";
            LossTable = tempLossTable;
        }

        public void updateRiskEventTable()
        {
            List<RiskEventRow> tempRiskEventTable = new List<RiskEventRow>();
            RiskEventRow categoryRow;
            int num, sumPerCat, risksPerCat, sumRisks = 0;
            double sumPercents = 0.0, percent;
            for(int cat = 0; cat < 4; ++cat)
                sumRisks += Risk.RiskNames[cat].Length;
            for (int cat = 0; cat < 4; ++cat)
            {
                risksPerCat = Risk.RiskNames[cat].Length;
                sumPerCat = 0;
                categoryRow = new RiskEventRow()
                {
                    IsBold = true,
                    Name = Risk.FullCategoryNames[cat],
                    RowHeader = cat + 1
                };
                tempRiskEventTable.Add(categoryRow);
                for(int rsk = 0; rsk < risksPerCat; ++rsk)
                {
                    num = (CurrentIteration.Risks.Find(r => r.RiskType == rsk && r.Category == cat) != null) ? 1 : 0;
                    sumPerCat += num;
                    tempRiskEventTable.Add(new RiskEventRow()
                    {
                        RowHeader = rsk + 1,
                        Name = Risk.RiskNames[cat][rsk],
                        Number = num
                    });
                }
                categoryRow.Number = sumPerCat;
                percent = (sumPerCat) / (double)sumRisks;
                categoryRow.Percent = percent;
                sumPercents += percent;
            }
            tempRiskEventTable.Add(new RiskEventRow()
            {
                Number = sumRisks,
                Percent = sumPercents
            });
            RiskEventTable = tempRiskEventTable;
        }

        public void updateProjectInfoTable()
        {
            List<ProjectInfoRow> tempProjectInfoTable = new List<ProjectInfoRow>();
            double[] finalPrices = calculateFinalPrices();
            double sum = finalPrices[0] + finalPrices[1] + finalPrices[2] + finalPrices[3];
            tempProjectInfoTable.Add(new ProjectInfoRow()
            {
                Text = "Початкова вартість реалізації проекту",
                Overal = CurrentProject.InitialCost,
                First = CurrentProject[0],
                Second = CurrentProject[1],
                Third = CurrentProject[2],
                Fourth = CurrentProject[3],
                CostCoef = (sum / CurrentProject.InitialCost).ToString("F3", CultureInfo.InvariantCulture)
            });
            tempProjectInfoTable.Add(new ProjectInfoRow()
            {
                Text = "Кінцева вартість реалізації проекту",
                OneDigit = false,
                First = finalPrices[0],
                Second = finalPrices[1],
                Third = finalPrices[2],
                Fourth = finalPrices[3],
                Overal = sum
            });
            ProjectInfoTable = tempProjectInfoTable;
        }

        public void Save(string path)
        {
            DataSerializer.SerializeData<DataModel>(path, this);
        }
    }
}
