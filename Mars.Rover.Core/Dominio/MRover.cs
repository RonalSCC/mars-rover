namespace Mars.Rover.Core;

public class MRover
{
    public Posicion Posicion { get; private set; } = new(0,0,PuntosCardinales.Norte);
    public List<char> Comandos { get; private set; } = new();
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

        ActualizarMensajePosicion();
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
    
    public void ActualizarMensajePosicion()
    {
        string direccionText = ObtenerTextoPuntoCardinal();
        string mensaje =  $"POSICION ({Posicion.Y},{Posicion.X}) - DIRECCION {direccionText}";
        
        if(Comandos.Count() == 10)
            mensaje += " - EXPLORACION EXITOSA";
        else if (Comandos.Count() > 10)
            mensaje += " - EXPLORACION FINALIZADA - MAXIMO DE COMANDOS ALCANZADO";
        
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
}

public static class ComandosRover
{
    public const char Avanzar = 'M';
    public const char GirarDerecha = 'R';
    public const char GirarIzquierda = 'L';
}