using Model.@enum;

namespace Model.Entity;

public class CircuitCondition
{ // Todo remove the setters 
    public int CircuitId  { get; set; }
    public int AvgTemp  { get; set; }
    public Condition AvgCondition  { get; set; }
}