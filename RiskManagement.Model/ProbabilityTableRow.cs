
using System.Windows.Media;

namespace RiskManagement.Model
{
    public class ProbabilityTableRow
    {
        public int? Number { get; set; } = null;
        public string Name { get; set; } = "";
        public ColumnsGroup Guesses { get; set; }
        public double? Average { get; set; } = null;
        public ColumnsGroup GuessesWeighted { get; set; }
        public double? AverageWeighted { get; set; } = null;
        public string ProbabilityGrade { get; set; } = "";
        public bool IsBold { get; set; } = false;
        public Brush AverageColor { get; set; } = null;
        public Brush AverageWeightedColor { get; set; } = null;
        public bool AverageTextWhite { get; set; } = false;
        public bool AverageWeightedTextWhite { get; set; } = false;
        public Brush GradeColor
        {
            get
            {
                if (ProbabilityGrade == "Дуже низька")
                    return LossTableRow.trafficLightColours[0];
                if (ProbabilityGrade == "Низька")
                    return LossTableRow.trafficLightColours[1];
                else if (ProbabilityGrade == "Середня")
                    return LossTableRow.trafficLightColours[2];
                else if (ProbabilityGrade == "Висока")
                    return LossTableRow.trafficLightColours[3];
                else if (ProbabilityGrade == "Дуже висока")
                    return LossTableRow.trafficLightColours[4];
                return null;
            }
        }
    }
}
