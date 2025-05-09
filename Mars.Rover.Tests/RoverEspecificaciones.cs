﻿using Mars.Rover.Core;

namespace Mars.Rover.Tests;

using FluentAssertions;

public class RoverEspecificaciones
{
    [Fact]
    public void Debe_iniciar_el_rover_su_exploracion_en_la_posicion_0_0_N()
    {
        // Arrange
        var rover = new MRover();

        // Act
        rover.IniciarExploracion();

        // Assert
        rover.Posicion.X.Should().Be(0);
        rover.Posicion.Y.Should().Be(0);
        rover.Posicion.Direccion.Should().Be(PuntosCardinales.Norte);
        rover.Mensaje.Should().Be("POSICION (0,0) - DIRECCION Norte");
    }

    [Fact]
    public void Debe_iniciar_el_rover_su_exploracion_sobre_la_cuadricula_en_la_posicion_1_2_N()
    {
        // Arrange
        var posicionInicial = new Posicion(2 ,1, PuntosCardinales.Norte);
        var rover = new MRover("", posicionInicial);

        // Act
        rover.IniciarExploracion();

        // Assert
        rover.Posicion.X.Should().Be(1);
        rover.Posicion.Y.Should().Be(2);
        rover.Posicion.Direccion.Should().Be(PuntosCardinales.Norte);
    }

    [Fact]
    public void
        Debe_avanzar_el_rover_una_celda_hacia_adelante_cuando_tenga_espacio_en_la_cuadricula_y_se_indique_el_comando_M()
    {
        // Arrange
        var rover = new MRover("M");

        // Act
        rover.IniciarExploracion();

        // Assert
        rover.Posicion.X.Should().Be(0);
        rover.Posicion.Y.Should().Be(1);
        rover.Posicion.Direccion.Should().Be(PuntosCardinales.Norte);
    }

    [Fact]
    public void Debe_girar_el_rover_a_la_derecha_cuando_reciba_el_comando_R()
    {
        // Arrange 
        var rover = new MRover("R");

        // Act
        rover.IniciarExploracion();

        // Assert
        rover.Posicion.Direccion.Should().Be(PuntosCardinales.Este);
    }

    [Fact]
    public void Debe_girar_el_rover_a_la_izquierda_cuando_reciba_el_comando_L()
    {
        // Arrange 
        var rover = new MRover("L");

        // Act
        rover.IniciarExploracion();

        // Assert
        rover.Posicion.Direccion.Should().Be(PuntosCardinales.Oeste);
    }

    [Theory]
    [InlineData("", PuntosCardinales.Norte)]
    [InlineData("LL", PuntosCardinales.Sur)]
    [InlineData("L", PuntosCardinales.Oeste)]
    [InlineData("R", PuntosCardinales.Este)]
    [InlineData("RR", PuntosCardinales.Sur)]
    public void Debe_mirar_el_rover_a_la_posicion_indicada_cuando_se_le_indiquen_comandos_de_giro_y_este_en_0_0_N(string comando, PuntosCardinales direccionEsperada)
    {
        // Arrange
        var rover = new MRover(comando);

        // Act
        rover.IniciarExploracion();

        // Assert
        rover.Posicion.Direccion.Should().Be(direccionEsperada);
    }

    [Fact]
    public void
        Debe_indicar_SinEspacioException_el_rover_cuando_no_tenga_espacio_para_moverse_adelante_este_en_0_0_N_y_su_comando_sea_L_y_M()
    {
        // Arrange
        var rover = new MRover("LM");

        // Act
        var resultadoExploracion = () => rover.IniciarExploracion();

        // Assert
        resultadoExploracion.Should().ThrowExactly<SinEspacioException>();
    }

    [Theory]
    [MemberData(nameof(CasosRoverConPosicionEsperada))]
    public void
        Debe_el_rover_realizar_una_exploracion_cuando_se_le_indiquen_10_comandos_y_arrojar_un_mensaje_de_su_posicion_junto_a_exploracionExitosa(
            string comando, Posicion posicionEsperada,string mensajeEsperado)
    {
        // Arrange
        var rover = new MRover(comando);

        // Act
        rover.IniciarExploracion();

        // Assert
        rover.Posicion.X.Should().Be(posicionEsperada.X);
        rover.Posicion.Y.Should().Be(posicionEsperada.Y);
        rover.Posicion.Direccion.Should().Be(posicionEsperada.Direccion);
        rover.Mensaje.Should().Be(mensajeEsperado);
    }
    
    [Theory]
    [MemberData(nameof(CasosRoverConPosicionInicialYEsperada))]
    public void
        Debe_el_rover_indicar_su_posicion_cuando_finalice_su_exploracion_segun_una_serie_de_comandos_y_una_posicion_inicial(
            string comando, Posicion? posicionInicial, Posicion posicionEsperada, string mensajeEsperado
        )
    {
        // Arrange
        var rover = new MRover(comando, posicionInicial);

        // Act
        rover.IniciarExploracion();

        // Assert
        rover.Posicion.X.Should().Be(posicionEsperada.X);
        rover.Posicion.Y.Should().Be(posicionEsperada.Y);
        rover.Posicion.Direccion.Should().Be(posicionEsperada.Direccion);
        rover.Mensaje.Should().Be(mensajeEsperado);
    }

    [Fact]
    public void
        Debe_realizar_la_exploración_el_rover_cuando_se_le_indiquen_más_de_10_comandos_y_arrojar_un_mensaje_de_su_posición_junto_a_Exploración_finalizada_Maximo_de_comandos_alcanzado()
    {
        // Arrange
        var rover = new MRover("MMMMMMMMMMRMMMMMMMMMM");
        
        // Act
        rover.IniciarExploracion();

        // Assert
        rover.Mensaje.Should()
            .Be("POSICION (10,0) - DIRECCION Norte - EXPLORACION FINALIZADA - MAXIMO DE COMANDOS ALCANZADO");
    }

    public static IEnumerable<object[]> CasosRoverConPosicionEsperada =>
        new List<object[]>
        {
            new object[]
            {
                "MMMMMMMMMM",
                new Posicion(10, 0, PuntosCardinales.Norte),
                "POSICION (10,0) - DIRECCION Norte - EXPLORACION EXITOSA",
            },
            new object[]
            {
                "MMMRMMMLMM",
                new Posicion(5, 3, PuntosCardinales.Norte),
                "POSICION (5,3) - DIRECCION Norte - EXPLORACION EXITOSA"
            },
            new object[]
            {
                "MMMRMRMMMR",
                new Posicion(0,1, PuntosCardinales.Oeste),
                "POSICION (0,1) - DIRECCION Oeste - EXPLORACION EXITOSA"
            },
            new object[]
            {
                "RMMLMMLMML",
                new Posicion(2,0, PuntosCardinales.Sur),
                "POSICION (2,0) - DIRECCION Sur - EXPLORACION EXITOSA"
            },
        };
    
    public static IEnumerable<object[]> CasosRoverConPosicionInicialYEsperada =>
        new List<object[]>
        {
            new object[]
            {
                "LMLMLMLMM",
                new Posicion(2,1, PuntosCardinales.Norte),
                new Posicion(3,1, PuntosCardinales.Norte),
                "POSICION (3,1) - DIRECCION Norte",
            },
            new object[]
            {
                "MMRMMRMRRM",
                new Posicion(3,3, PuntosCardinales.Este),
                new Posicion(1,5,PuntosCardinales.Este),
                "POSICION (1,5) - DIRECCION Este - EXPLORACION EXITOSA"
            }
        };
    

}