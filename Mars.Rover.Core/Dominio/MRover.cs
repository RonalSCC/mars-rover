namespace Mars.Rover.Core;

public class MRover
{
    public Posicion Posicion { get; private set; } = new(0,0,PuntosCardinales.Norte);
    private List<char> Comandos { get; set; } = new();
    public string Mensaje { get; set; } = "POSICION (0,0) - DIRECCION Norte";
    
    private int _cantidadComandosEjecutados = 0;

    public MRover(string comandos = "", Posicion? posicionInicial = null)
    {
        if(posicionInicial != null) Posicion = posicionInicial;
        
        foreach (char comando in comandos)
            Comandos.Add(comando);
    }

    public void IniciarExploracion()
    {
        foreach (var comando in Comandos)
        {
            if (_cantidadComandosEjecutados == 10) break;
            switch (comando)
            {
                case ComandosRover.Avanzar: Avanzar(); break;
                case ComandosRover.GirarDerecha: GirarDerecha(); break;
                case ComandosRover.GirarIzquierda: GirarIzquierda(); break;
            }

            ValidarEspacio();
            AumentarComandosEjecutados();
        }

        ActualizarMensajeSegunPosicion();
    }

    private void Avanzar()
    {
        switch (Posicion.Direccion)
        {
            case PuntosCardinales.Norte: Posicion.Y++; break;
            case PuntosCardinales.Este: Posicion.X++; break;
            case PuntosCardinales.Sur: Posicion.Y--; break;
            default: Posicion.X--; break;
        }
    }

    private void GirarDerecha()
    {
        if(Posicion.Direccion == PuntosCardinales.Oeste)
            Posicion.Direccion = PuntosCardinales.Norte;
        else
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
    
    private void AumentarComandosEjecutados()
    {
        _cantidadComandosEjecutados++;
    }
    
    private void ActualizarMensajeSegunPosicion()
    {
        string direccionText = ObtenerTextoPuntoCardinal();
        string mensaje =  $"POSICION ({Posicion.Y},{Posicion.X}) - DIRECCION {direccionText}";
        
        mensaje += ObtenerMensajeComplementario();
        Mensaje = mensaje;
    }

    private string ObtenerTextoPuntoCardinal() => Posicion.Direccion switch
    {
        PuntosCardinales.Norte => "Norte",
        PuntosCardinales.Este => "Este",
        PuntosCardinales.Sur => "Sur",
        PuntosCardinales.Oeste => "Oeste",
        _ => "Sin dirección"
    };
    
    private string ObtenerMensajeComplementario() => Comandos.Count() switch
    {
        10 => " - EXPLORACION EXITOSA",
        > 10 => " - EXPLORACION FINALIZADA - MAXIMO DE COMANDOS ALCANZADO",
        _ => string.Empty
    };
}

public static class ComandosRover
{
    public const char Avanzar = 'M';
    public const char GirarDerecha = 'R';
    public const char GirarIzquierda = 'L';
}