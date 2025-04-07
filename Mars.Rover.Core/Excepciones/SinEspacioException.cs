namespace Mars.Rover.Core;

public class SinEspacioException : Exception
{
    public SinEspacioException() : base("No hay espacio para mover el rover")
    {
    }
}