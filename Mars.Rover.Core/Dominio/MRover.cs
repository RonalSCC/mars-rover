namespace Mars.Rover.Core;

public class MRover
{
    public int PosicionX = 0;
    public int PosicionY = 0;
    public PuntosCardinales Direccion = PuntosCardinales.Norte;
    public List<char> Comandos { get; private set; } = new();
    public string Mensaje { get; set; } = string.Empty;

    public MRover(string comandos = "", int posicionX = 0, int posicionY = 0, PuntosCardinales direccion = PuntosCardinales.Norte)
    {
        PosicionX = posicionX;
        PosicionY = posicionY;
        Direccion = direccion;
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

        Mensaje = MostrarPosicion();
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
        if (Direccion == PuntosCardinales.Norte)
            Direccion = PuntosCardinales.Oeste;
        else
            Direccion--;
    }

    private void ValidarEspacio()
    {
        if (PosicionY < 0 || PosicionX < 0)
            throw new SinEspacioException();
    }
    
    public string MostrarPosicion()
    {
        string direccionText = ObtenerTextoPuntoCardinal();
        string mensaje =  $"POSICION ({PosicionY},{PosicionX}) - DIRECCION {direccionText}";
        
        if(Comandos.Count() == 10)
            mensaje += " - EXPLORACION EXITOSA";
        
        return mensaje;
    }

    private string ObtenerTextoPuntoCardinal() => Direccion switch
    {
        PuntosCardinales.Norte => "Norte",
        PuntosCardinales.Este => "Este",
        PuntosCardinales.Sur => "Sur",
        PuntosCardinales.Oeste => "Oeste",
        _ => "Sin dirección"
    };
}

public static class ComandosRover
{
    public const char Avanzar = 'M';
    public const char GirarDerecha = 'R';
    public const char GirarIzquierda = 'L';
}