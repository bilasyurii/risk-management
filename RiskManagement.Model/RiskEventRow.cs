namespace RiskManagement.Model
{
    public class RiskEventRow
    {
        public int? RowHeader { get; set; } = null;
        public string Name { get; set; } = "";
        public int Number { get; set; }
        public double? Percent { get; set; }
        public bool IsBold { get; set; } = false;
    }
}
