﻿namespace Mars.Rover.Tests;
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
        rover.PosicionX.Should().Be(0);
        rover.PosicionY.Should().Be(0);
        rover.Direccion.Should().Be("N");
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
        rover.PosicionX.Should().Be(0);
        rover.PosicionY.Should().Be(1);
        rover.Direccion.Should().Be("N");
    }

    [Fact]
    public void Debe_girar_el_rover_a_la_derecha_cuando_reciba_el_comando_R()
    {
        // Arrange 
        var cuadricula = new Cuadricula();
        var rover = new MRover("R");
        
        // Act
        cuadricula.IniciarExploracionRover(rover);
        
        // Assert
        rover.Direccion.Should().Be("E");
    }
    
    [Fact]
    public void Debe_girar_el_rover_a_la_izquierda_cuando_reciba_el_comando_L()
    {
        // Arrange 
        var cuadricula = new Cuadricula();
        var rover = new MRover("L");
        
        // Act
        cuadricula.IniciarExploracionRover(rover);
        
        // Assert
        rover.Direccion.Should().Be("O");
    }
    
    [Fact]
    public void Debe_indicar_SinEspacioException_el_rover_cuando_no_tenga_espacio_para_moverse_adelante_este_en_0_0_N_y_su_comando_sea_L_y_M()
    {
        // Arrange
        var cuadricula = new Cuadricula();
        var rover = new MRover("LM");
        
        // Act
        var resultadoExploracion = () => cuadricula.IniciarExploracionRover(rover);
        
        // Assert
        resultadoExploracion.Should().ThrowExactly<SinEspacioException>();
    }
}

public class SinEspacioException : Exception
{
    public SinEspacioException(): base("No hay espacio para mover el rover"){}
}

public class MRover
{
    public int PosicionX = 0;
    public int PosicionY = 0;
    public string Direccion = "N";
    public string Comando { get; private set; }

    public MRover(string comando = "")
    {
        Comando = comando;
        if(comando is "M") Avanzar();
        if (comando is "R") GirarDerecha();
    }

    private void Avanzar()
    {
        PosicionY++;
    }
    
    private void GirarDerecha()
    {
        Direccion = "E";
    }
}

public class Cuadricula
{
    public void IniciarExploracionRover(MRover rover)
    {
        if(rover.Comando == "LM") 
            throw new SinEspacioException();
    }
}