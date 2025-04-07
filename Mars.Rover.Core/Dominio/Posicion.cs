namespace Mars.Rover.Core;

public class Posicion(int x, int y, PuntosCardinales direccion)
{
    public int X { get; set; } = x;
    public int Y { get; set; } = y;
    public PuntosCardinales Direccion { get; set; } = direccion;
}