using Model.@enum;

namespace Model.dto;

public class CircuitConditions
{
    public Circuit Circuit { get; set; }
    public double Temp { get; set; }
    public double Rain { get; set; } // edit to be boolean, is raining 
}