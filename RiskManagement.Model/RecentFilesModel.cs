using RiskManagement.Model.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace RiskManagement.Model
{
    [DataContract]
    public class RecentFilesModel
    {
        [DataMember]
        public List<string> RecentFiles { get; set; } = new List<string>();
        public Expert[] Experts { get; set; }
        public string ProjectName { get; set; } = "";
        public string NewProjectFolder { get; set; } = null;
        private double? _initialCost = null;
        public double? InitialCost
        {
            get { return _initialCost; }
            set
            {
                if (value == null)
                    _initialCost = null;
                else
                    _initialCost = Math.Max(value.Value, 1.0);
            }
        }
        public static string RecentFilesPath = "recent.dat";

        public static RecentFilesModel Load()
        {
            RecentFilesModel model;
            if (File.Exists(RecentFilesPath))
                model = DataSerializer.DeserializeData<RecentFilesModel>(RecentFilesPath);
            else
                model = new RecentFilesModel();
            for (int i = model.RecentFiles.Count - 1; i >= 0; --i)
                if (!File.Exists(model.RecentFiles[i]))
                    model.RecentFiles.RemoveAt(i);
            model.Experts = new Expert[10];
            for (int i = 0; i < 10; ++i)
                model.Experts[i] = new Expert()
                {
                    Number = i + 1,
                    ProbabilityWeights = new double[4],
                    LossWeights = new double[4]
                };
            return model;
        }

        public void Save(string path)
        {
            DataSerializer.SerializeData<RecentFilesModel>(path, this);
        }
    }
}
