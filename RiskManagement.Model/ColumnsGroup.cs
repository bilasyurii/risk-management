using System.Collections;
using System.Windows.Media;

namespace RiskManagement.Model
{
    public enum CGroupType
    {
        Header,
        ExpertNumbers,
        ExpertAverageGuess,
        ExpertProbabilityWeights,
        ExpertLossWeights,
        Probabilities,
        ProbabilitiesWeighted,
        Losses,
        LossesWeighted
    }
    public class ColumnsGroupItem
    {
        public object Value { get; set; }

        public Brush BackgroundColor { get; set; } = null;

        public static implicit operator ColumnsGroupItem(int i)
        {
            return new ColumnsGroupItem() { Value = i};
        }

        public bool TwoDigitMandatory { get; set; } = false;

        public static implicit operator ColumnsGroupItem(double d)
        {
            return new ColumnsGroupItem() { Value = d };
        }
    }

    public class ColumnsGroup
    {
        public CGroupType Type { get; private set; }
        public string HeaderText { get; set; }
        private static readonly ColumnsGroupItem[] expertNumbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        public Brush BackgroundColor { get; set; } = null;
        public bool TwoDigitMandatory { get; set; } = true;
        private double coeff = 1.0;
        public IEnumerable Collection {
            get {
                var collection = new ColumnsGroupItem[10];
                switch (Type)
                {
                    case CGroupType.ExpertNumbers:
                        return expertNumbers;
                    case CGroupType.ExpertAverageGuess:
                        for (int i = 0; i < 10; ++i)
                        {
                            collection[i] = _averageGuesses[i];
                            collection[i].BackgroundColor = BackgroundColor;
                            collection[i].TwoDigitMandatory = TwoDigitMandatory;
                        }
                        break;
                    case CGroupType.ExpertProbabilityWeights:
                        for (int i = 0; i < 10; ++i)
                        {
                            collection[i] = _experts[i].ProbabilityWeights[_category];
                            collection[i].BackgroundColor = BackgroundColor;
                            collection[i].TwoDigitMandatory = TwoDigitMandatory;
                        }
                        break;
                    case CGroupType.ExpertLossWeights:
                        for (int i = 0; i < 10; ++i)
                        {
                            collection[i] = _experts[i].LossWeights[_category];
                            collection[i].BackgroundColor = BackgroundColor;
                            collection[i].TwoDigitMandatory = TwoDigitMandatory;
                        }
                        break;
                    case CGroupType.Losses:
                        for (int i = 0; i < 10; ++i)
                        {
                            collection[i] = _risk.Losses[i];
                            collection[i].BackgroundColor = BackgroundColor;
                            collection[i].TwoDigitMandatory = TwoDigitMandatory;
                        }
                        break;
                    case CGroupType.Probabilities:
                        for (int i = 0; i < 10; ++i)
                        {
                            collection[i] = _risk.Probabilities[i] * coeff;
                            collection[i].BackgroundColor = BackgroundColor;
                            collection[i].TwoDigitMandatory = TwoDigitMandatory;
                        }
                        break;
                    case CGroupType.LossesWeighted:
                        for (int i = 0; i < 10; ++i)
                        {
                            collection[i] = _risk.Losses[i] * _experts[i].LossWeights[_category];
                            collection[i].BackgroundColor = BackgroundColor;
                            collection[i].TwoDigitMandatory = TwoDigitMandatory;
                        }
                        break;
                    case CGroupType.ProbabilitiesWeighted:
                        for (int i = 0; i < 10; ++i)
                        {
                            collection[i] = _risk.Probabilities[i] * _experts[i].ProbabilityWeights[_category] * coeff;
                            collection[i].BackgroundColor = BackgroundColor;
                            collection[i].TwoDigitMandatory = TwoDigitMandatory;
                        }
                        break;
                }
                return collection;
            }
        }
        public bool IsText { get { return Type == CGroupType.Header; } }
        private Risk _risk;
        private Expert[] _experts;
        private int _category;
        private double[] _averageGuesses;

        public ColumnsGroup(string headerText)
        {
            Type = CGroupType.Header;
            HeaderText = headerText;
        }

        public void checkBehaviour()
        {
            TwoDigitMandatory = (Type != CGroupType.ExpertNumbers && 
                                 Type != CGroupType.ExpertProbabilityWeights &&
                                 Type != CGroupType.ExpertLossWeights);
        }

        public ColumnsGroup(Risk risk, Expert[] experts, bool isProbabilities, bool isWeighted, Brush backgroundColor = null)
        {
            BackgroundColor = backgroundColor;
            _risk = risk;
            _experts = experts;
            _category = _risk.Category;
            if (risk.HasSolution())
                coeff = 0.8;
            Type = isProbabilities
                   ? (isWeighted
                      ? CGroupType.ProbabilitiesWeighted
                      : CGroupType.Probabilities)
                   : (isWeighted
                      ? CGroupType.LossesWeighted
                      : CGroupType.Losses);
            checkBehaviour();
        }

        public ColumnsGroup(Expert[] experts, int category, bool isProbabilities, Brush backgroundColor = null)
        {
            BackgroundColor = backgroundColor;
            _experts = experts;
            _category = category;
            Type = isProbabilities
                   ? CGroupType.ExpertProbabilityWeights
                   : CGroupType.ExpertLossWeights;
            checkBehaviour();
        }

        public ColumnsGroup(Brush backgroundColor = null)
        {
            BackgroundColor = backgroundColor;
            Type = CGroupType.ExpertNumbers;
            checkBehaviour();
        }

        public ColumnsGroup(double[] averageGuesses, Brush backgroundColor = null)
        {
            BackgroundColor = backgroundColor;
            _averageGuesses = new double[10];
            averageGuesses.CopyTo(_averageGuesses, 0);
            Type = CGroupType.ExpertAverageGuess;
            checkBehaviour();
        }
    }
}
