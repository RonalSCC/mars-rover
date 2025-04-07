using Mars.Rover.Core;

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
        cuadricula.IniciarExploracion(rover);

        // Assert
        rover.PosicionX.Should().Be(0);
        rover.PosicionY.Should().Be(0);
        rover.Direccion.Should().Be(PuntosCardinales.Norte);
    }

    [Fact]
    public void Debe_iniciar_el_rover_su_exploracion_sobre_la_cuadricula_en_la_posicion_1_2_N()
    {
        // Arrange
        var cuadricula = new Cuadricula();
        var rover = new MRover("M", 1,2, PuntosCardinales.Norte);

        // Act
        cuadricula.IniciarExploracion(rover);
        
        // Assert
        rover.PosicionX.Should().Be(1);
        rover.PosicionY.Should().Be(2);
        rover.Direccion.Should().Be(PuntosCardinales.Norte);
    }

    [Fact]
    public void Debe_avanzar_el_rover_una_celda_hacia_adelante_cuando_tenga_espacio_en_la_cuadricula_y_se_indique_el_comando_M()
    {
        // Arrange
        var cuadricula = new Cuadricula();
        var rover = new MRover("M");

        // Act
        cuadricula.IniciarExploracion(rover);

        // Assert
        rover.PosicionX.Should().Be(0);
        rover.PosicionY.Should().Be(1);
        rover.Direccion.Should().Be(PuntosCardinales.Norte);
    }

    [Fact]
    public void Debe_girar_el_rover_a_la_derecha_cuando_reciba_el_comando_R()
    {
        // Arrange 
        var cuadricula = new Cuadricula();
        var rover = new MRover("R");

        // Act
        cuadricula.IniciarExploracion(rover);

        // Assert
        rover.Direccion.Should().Be(PuntosCardinales.Este);
    }

    [Fact]
    public void Debe_girar_el_rover_a_la_izquierda_cuando_reciba_el_comando_L()
    {
        // Arrange 
        var cuadricula = new Cuadricula();
        var rover = new MRover("L");

        // Act
        cuadricula.IniciarExploracion(rover);

        // Assert
        rover.Direccion.Should().Be(PuntosCardinales.Oeste);
    }

    [Fact]
    public void Debe_mirar_el_rover_al_sur_cuando_se_le_indiquen_dos_comandos_de_giro_y_este_en_0_0_N()
    {
        // Arrange
        var cuadricula = new Cuadricula();
        var rover = new MRover("LL");

        // Act
        cuadricula.IniciarExploracion(rover);

        // Assert
        rover.Direccion.Should().Be(PuntosCardinales.Sur);
    }

    [Fact]
    public void Debe_indicar_SinEspacioException_el_rover_cuando_no_tenga_espacio_para_moverse_adelante_este_en_0_0_N_y_su_comando_sea_L_y_M()
    {
        // Arrange
        var cuadricula = new Cuadricula();
        var rover = new MRover("LM");

        // Act
        var resultadoExploracion = () => cuadricula.IniciarExploracion(rover);

        // Assert
        resultadoExploracion.Should().ThrowExactly<SinEspacioException>();
    }

    [Theory]
    [InlineData("MMRMMRMM", 2,0, PuntosCardinales.Sur,"POSICION (0,2) - DIRECCION Sur")]
    [InlineData("RMMMMM", 5,0, PuntosCardinales.Este,"POSICION (0,5) - DIRECCION Este")]
    [InlineData("MMMRMMMLM", 3,4, PuntosCardinales.Norte,"POSICION (4,3) - DIRECCION Norte")]
    public void Debe_el_rover_indicar_su_posicion_cuando_finalice_su_exploracion_segun_una_serie_de_comandos(string comando, int posicionXEsperada, int posicionYEsperada, PuntosCardinales direccionEsperada, string mensajeEsperado)
    {
        // Arrange
        var cuadricula = new Cuadricula();
        var rover = new MRover(comando);

        // Act
        cuadricula.IniciarExploracion(rover);

        // Assert
        rover.PosicionX.Should().Be(posicionXEsperada);
        rover.PosicionY.Should().Be(posicionYEsperada);
        rover.Direccion.Should().Be(direccionEsperada);
        rover.Mensaje.Should().Be(mensajeEsperado);
    }

    [Theory]
    [InlineData("MMMMMMMMMM", 0, 10, PuntosCardinales.Norte, "POSICION (10,0) - DIRECCION Norte - EXPLORACION EXITOSA")]
    [InlineData("MMMRMMMLMM", 3, 5, PuntosCardinales.Norte, "POSICION (5,3) - DIRECCION Norte - EXPLORACION EXITOSA")]
    [InlineData("MMMRMMMLMM", 3, 5, PuntosCardinales.Norte, "POSICION (5,3) - DIRECCION Norte - EXPLORACION EXITOSA")]
    public void Debe_el_rover_realizar_una_exploracion_cuando_se_le_indiquen_10_comandos_y_arrojar_un_mensaje_de_su_posicion_junto_a_exploracionExitosa(string comando, int posicionXEsperada, int posicionYEsperada, PuntosCardinales direccionEsperada, string mensajeEsperado)
    {
        // Arrange
        var cuadricula = new Cuadricula();
        var rover = new MRover(comando);
        
        // Act
        cuadricula.IniciarExploracion(rover);

        // Assert
        rover.PosicionX.Should().Be(posicionXEsperada);
        rover.PosicionY.Should().Be(posicionYEsperada);
        rover.Direccion.Should().Be(direccionEsperada);
        rover.Mensaje.Should().Be(mensajeEsperado);
    }
}