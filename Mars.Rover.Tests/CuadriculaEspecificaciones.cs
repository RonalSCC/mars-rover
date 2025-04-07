namespace Mars.Rover.Tests;
using FluentAssertions;

public class CuadriculaEspecificaciones
{
    [Fact]
    public void Debe_iniciar_el_rover_su_exploracion_en_la_posicion_0_0_N()
    {
        // Arrange
        var cuadricula = new Cuadricula();
        var rover = new MRover();

        // Act
        cuadricula.IniciarExploracionRover(rover);

        // Assert
        rover.posicionActual.Should().Be("0 0 N");
    }
}

public class MRover
{
    public string posicionActual;
}

public class Cuadricula
{
    public void IniciarExploracionRover(MRover rover)
    {
        throw new NotImplementedException();
    }
}