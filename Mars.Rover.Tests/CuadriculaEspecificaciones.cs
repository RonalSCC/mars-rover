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

    [Fact]
    public void Debe_el_rover_indicar_su_posicion_cuando_finalice_su_exploracion_segun_una_serie_de_comandos()
    {
        // Arrange
        var cuadricula = new Cuadricula();
        var rover = new MRover("MMRMMRMM");
        
        // Act
        cuadricula.IniciarExploracion(rover);
        
        // Assert
        rover.PosicionX.Should().Be(2);
        rover.PosicionY.Should().Be(0);
        rover.Direccion.Should().Be(PuntosCardinales.Sur);
        rover.Mensaje.Should().Be("POSICION (0,2) - DIRECCION Sur");
    }
}

public class SinEspacioException : Exception
{
    public SinEspacioException(): base("No hay espacio para mover el rover"){}
}

public static class ComandosRover
{
    public const char Avanzar = 'M';
    public const char GirarDerecha = 'R';
    public const char GirarIzquierda = 'L';
}

public enum PuntosCardinales
{
    Norte,
    Este,
    Sur,
    Oeste
}

public class MRover
{
    public int PosicionX = 0;
    public int PosicionY = 0;
    public PuntosCardinales Direccion = PuntosCardinales.Norte;
    public List<char> Comandos { get; private set; } = new();
    public string Mensaje { get; set; } = string.Empty;

    public MRover(string comandos = "")
    {
        foreach (char comando in comandos)
        {
            Comandos.Add(comando);
        }
    }
    
    public void IniciarExploracionRover()
    {
        foreach (var comando in Comandos)
        {
            switch (comando)
            {
                case ComandosRover.Avanzar: Avanzar(); break;
                case ComandosRover.GirarDerecha: GirarDerecha(); break;
                case ComandosRover.GirarIzquierda: GirarIzquierda(); break;
            }

            ValidarEspacio();
        }
    }
    private void Avanzar()
    {
        if (Direccion == PuntosCardinales.Norte)
            PosicionY++;
        else if (Direccion == PuntosCardinales.Este)
            PosicionX++;
        else if (Direccion == PuntosCardinales.Sur)
            PosicionY--;
        else
            PosicionX--;
    }
    
    private void GirarDerecha()
    {
        Direccion++;
    }
    
    private void GirarIzquierda()
    {
        if(Direccion == PuntosCardinales.Norte)
            Direccion = PuntosCardinales.Oeste;
        else
            Direccion--;
    }
    
    private void ValidarEspacio()
    {
        if (PosicionY < 0 || PosicionX < 0)
            throw new SinEspacioException();
    }
}

public class Cuadricula
{
    public void IniciarExploracion(MRover rover)
    {
        rover.IniciarExploracionRover();
    }
}