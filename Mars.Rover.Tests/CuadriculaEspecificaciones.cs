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

    [Fact]
    public void Debe_avanzar_el_rover_una_celda_hacia_adelante_cuando_tenga_espacio_en_la_cuadricula_y_se_indique_el_comando_M()
    {
        // Arrange
        var cuadricula = new Cuadricula();
        var rover = new MRover("M");
        
        // Act
        cuadricula.IniciarExploracionRover(rover);
        
        // Assert
        rover.posicionActual.Should().Be("0 1 N");
    }
}

public class MRover
{
    public string posicionActual = "0 0 N";

    public MRover(string comando = "")
    {
        if(!string.IsNullOrEmpty(comando)) Avanzar();
    }

    private void Avanzar()
    {
        posicionActual = "0 1 N";
    }
}

public class Cuadricula
{
    public void IniciarExploracionRover(MRover rover)
    {
    }
}