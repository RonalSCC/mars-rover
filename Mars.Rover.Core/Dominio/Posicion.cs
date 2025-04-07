namespace Mars.Rover.Core;

public class Posicion(int y, int x, PuntosCardinales direccion)
{
    public int Y { get; set; } = y;
    public int X { get; set; } = x;
    public PuntosCardinales Direccion { get; set; } = direccion;
}