
using RiskManagement.Model;

namespace RiskManagement.Model
{
    public class ExpertGuess
    {
        private Risk _risk = null;
        private Expert _expert = null;
        private int _index = 0;
        public ExpertGuess() { }
        public ExpertGuess(Risk risk, Expert expert, int index)
        {
            _risk = risk;
            _expert = expert;
            _index = index;
            RowHeader = "Експерт " + (_index + 1);
        }
        public string RowHeader { get; private set; }
        public double? ProbabilityWeight
        {
            get
            {
                if (_expert == null)
                    return null;
                return _expert.ProbabilityWeights[_risk.Category];
            }
            set
            {
                if (_expert != null || value != null)
                    _expert.ProbabilityWeights[_risk.Category] = value ?? 0;
            }
        }

        public double? LossWeight
        {
            get
            {
                if (_expert == null)
                    return null;
                return _expert.LossWeights[_risk.Category];
            }
            set
            {
                if (_expert != null || value != null)
                    _expert.LossWeights[_risk.Category] = value ?? 0;
            }
        }

        public double? Probability
        {
            get
            {
                if (_risk == null)
                    return null;
                return _risk.Probabilities[_index];
            }
            set
            {
                if (_risk != null || value != null)
                    _risk.Probabilities[_index] = value ?? 0;
                if (_risk.Probabilities[_index] < 0.0)
                    _risk.Probabilities[_index] = 0.0;
                else if (_risk.Probabilities[_index] > 1.0)
                    _risk.Probabilities[_index] = 1.0;
            }
        }
        public double? Loss
        {
            get
            {
                if (_risk == null)
                    return null;
                return _risk.Losses[_index];
            }
            set
            {
                if (_risk != null || value != null)
                    _risk.Losses[_index] = value ?? 0;
                if (_risk.Losses[_index] < 0.0)
                    _risk.Losses[_index] = 0.0;
                else if (_risk.Losses[_index] > 1.0)
                    _risk.Losses[_index] = 1.0;
            }
        }
    }
}
