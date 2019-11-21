using System;
using System.Runtime.Serialization;

namespace RiskManagement.Model
{
    [DataContract]
    public class Expert
    {
        [DataMember]
        public double[] ProbabilityWeights { get; set; }
        [DataMember]
        public double[] LossWeights { get; set; }
        public int Number { get; set; } = 5;

        public double ProbabilityCat1
        {
            get { return ProbabilityWeights[0]; }
            set { ProbabilityWeights[0] = Math.Min(Math.Max(value, 5.0), 10.0); }
        }
        public double ProbabilityCat2
        {
            get { return ProbabilityWeights[1]; }
            set { ProbabilityWeights[1] = Math.Min(Math.Max(value, 5.0), 10.0); }
        }
        public double ProbabilityCat3
        {
            get { return ProbabilityWeights[2]; }
            set { ProbabilityWeights[2] = Math.Min(Math.Max(value, 5.0), 10.0); }
        }
        public double ProbabilityCat4
        {
            get { return ProbabilityWeights[3]; }
            set { ProbabilityWeights[3] = Math.Min(Math.Max(value, 5.0), 10.0); }
        }

        public double LossCat1
        {
            get { return LossWeights[0]; }
            set { LossWeights[0] = Math.Min(Math.Max(value, 5.0), 10.0); }
        }
        public double LossCat2
        {
            get { return LossWeights[1]; }
            set { LossWeights[1] = Math.Min(Math.Max(value, 5.0), 10.0); }
        }
        public double LossCat3
        {
            get { return LossWeights[2]; }
            set { LossWeights[2] = Math.Min(Math.Max(value, 5.0), 10.0); }
        }
        public double LossCat4
        {
            get { return LossWeights[3]; }
            set { LossWeights[3] = Math.Min(Math.Max(value, 5.0), 10.0); }
        }
    }
}
