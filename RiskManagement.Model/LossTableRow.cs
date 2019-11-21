
using System.Windows.Media;

namespace RiskManagement.Model
{
    public class LossTableRow
    {
        public static readonly Brush[] trafficLightColours =
        {
            new SolidColorBrush(Color.FromRgb(113, 218, 113)),
            new SolidColorBrush(Color.FromRgb(204, 255, 51)),
            new SolidColorBrush(Color.FromRgb(255, 255, 77)),
            new SolidColorBrush(Color.FromRgb(255, 194, 102)),
            new SolidColorBrush(Color.FromRgb(255, 153, 128))
        };
        public int? Number { get; set; } = null;
        public string Name { get; set; } = "";
        public double? Cost { get; set; } = null;
        public ColumnsGroup Guesses { get; set; }
        public double? Average { get; set; } = null;
        public ColumnsGroup GuessesWeighted { get; set; }
        public double? AdditionalCost { get; set; } = null;
        public string RiskPriority { get; set; } = "";
        public bool IsBold { get; set; } = false;
        public Brush AverageColor { get; set; } = null;
        public Brush AdditionalCostColor { get; set; } = null;
        public bool AverageTextWhite { get; set; } = false;
        public bool AdditionalCostTextWhite { get; set; } = false;
        public double? FinalCost { get; set; } = null;
        public Brush PriorityColor
        {
            get
            {
                if (RiskPriority == "НИЗЬКИЙ")
                    return trafficLightColours[0];
                else if (RiskPriority == "СЕРЕДНІЙ")
                    return trafficLightColours[2];
                else if(RiskPriority == "ВИСОКИЙ")
                    return trafficLightColours[4];
                return null;
            }
        }
        public string MinMax { get; set; } = "";
        public double? MinMaxValue { get; set; } = null;
    }
}
