using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RiskManagement.Model
{
    [DataContract]
    public class Project
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public List<Iteration> Iterations { get; set; }
        [DataMember]
        public Expert[] ExpertWeights { get; set; } 
        private double _initialCost;
        [DataMember]
        public double InitialCost {
            get { return _initialCost; }
            set
            {
                if(categoryCosts == null)
                    categoryCosts = new double[4];
                _initialCost = Math.Max(value, 1.0);
                double sum = 0.0, temp;
                int riskCount = 0;
                for (int i = 0; i < 4; ++i)
                    riskCount += Risk.RiskNames[i].Length;
                for(int i = 0; i < 3; ++i)
                {
                    temp = (InitialCost * Risk.RiskNames[i].Length) / riskCount;
                    categoryCosts[i] = temp;
                    sum += temp;
                }
                categoryCosts[3] = InitialCost - sum;
            }
        }
        public double getProbabilityWeightsSum(int cat)
        {
            double sum = 0.0;
            foreach (Expert expert in ExpertWeights)
                sum += expert.ProbabilityWeights[cat];
            return sum;
        }
        public double getLossWeightsSum(int cat)
        {
            double sum = 0.0;
            foreach (Expert expert in ExpertWeights)
                sum += expert.LossWeights[cat];
            return sum;
        }

        private double[] categoryCosts;

        public double this[int cat]
        {
            get
            {
                return categoryCosts[cat];
            }

        }
    }
}
