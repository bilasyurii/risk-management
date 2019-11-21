namespace RiskManagement.Model
{
    public class ProjectInfoRow
    {
        public string Text { get; set; } = "";
        public double Overal { get; set; }
        public double First { get; set; }
        public double Second { get; set; }
        public double Third { get; set; }
        public double Fourth { get; set; }
        public bool OneDigit { get; set; } = true;
        public string CostCoef { get; set; } = "";
    }
}
