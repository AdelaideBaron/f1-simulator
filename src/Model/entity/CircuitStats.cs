namespace Model.Entity;

public class CircuitStats
{
    public int CircuitId  { get; set; }
    public int AvgPitstops  { get; set; }
    public decimal AvgLaptime  { get; set; }
    public decimal AvgPitstopTime  { get; set; }
}