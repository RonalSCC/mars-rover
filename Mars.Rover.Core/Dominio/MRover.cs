namespace Mars.Rover.Core;

public class MRover
{
    public Posicion Posicion { get; private set; } = new(0,0,PuntosCardinales.Norte);
    public List<char> Comandos { get; private set; } = new();
    public string Mensaje { get; set; } = string.Empty;
    private int _cantidadComandosEjecutados = 0;

    public MRover(string comandos = "", int posicionX = 0, int posicionY = 0, PuntosCardinales direccion = PuntosCardinales.Norte)
    {
        Posicion.X = posicionX;
        Posicion.Y = posicionY;
        Posicion.Direccion = direccion;
        foreach (char comando in comandos)
        {
            Comandos.Add(comando);
        }
    }

    public void IniciarExploracionRover()
    {
        foreach (var comando in Comandos)
        {
            if (_cantidadComandosEjecutados == 10)
                break;
            switch (comando)
            {
                case ComandosRover.Avanzar: Avanzar(); break;
                case ComandosRover.GirarDerecha: GirarDerecha(); break;
                case ComandosRover.GirarIzquierda: GirarIzquierda(); break;
            }

            ValidarEspacio();
            _cantidadComandosEjecutados++;
        }

        Mensaje = MostrarPosicion();
    }

    private void Avanzar()
    {
        if (Posicion.Direccion == PuntosCardinales.Norte)
            Posicion.Y++;
        else if (Posicion.Direccion == PuntosCardinales.Este)
            Posicion.X++;
        else if (Posicion.Direccion == PuntosCardinales.Sur)
            Posicion.Y--;
        else
            Posicion.X--;
    }

    private void GirarDerecha()
    {
        Posicion.Direccion++;
    }

    private void GirarIzquierda()
    {
        if (Posicion.Direccion == PuntosCardinales.Norte)
            Posicion.Direccion = PuntosCardinales.Oeste;
        else
            Posicion.Direccion--;
    }

    private void ValidarEspacio()
    {
        if (Posicion.Y < 0 || Posicion.X < 0)
            throw new SinEspacioException();
    }
    
    public string MostrarPosicion()
    {
        string direccionText = ObtenerTextoPuntoCardinal();
        string mensaje =  $"POSICION ({Posicion.Y},{Posicion.X}) - DIRECCION {direccionText}";
        
        if(Comandos.Count() == 10)
            mensaje += " - EXPLORACION EXITOSA";
        else if (Comandos.Count() > 10)
            mensaje += " - EXPLORACION FINALIZADA - MAXIMO DE COMANDOS ALCANZADO";
        
        return mensaje;
    }

    private string ObtenerTextoPuntoCardinal() => Posicion.Direccion switch
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