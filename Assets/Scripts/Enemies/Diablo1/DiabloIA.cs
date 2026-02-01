using UnityEngine;

/** Cerebro principal del Diablo. Maneja estados y referencias */
public class DiabloIA : MonoBehaviour
{
    [Header("Estados")]
    public EstadoPatrulla EstadoPatrulla;
    public EstadoAtaque EstadoAtaque;
    public EstadoMuerte EstadoMuerte;
    public EstadoReaparicion EstadoReaparicion;

    [Header("Componentes")]
    public DiabloMover Movimiento;
    public DiabloRotacion Rotacion;
    public DiabloAtaque Ataque;
    public SensorVision[] Sensores;

    public IAgarraObjetos ObjetivoActual { get; private set; }
    public Transform PuntoManoObjetivo { get; private set; }

    private EstadoDiablo estadoActual;

    public bool TieneObjetivo => ObjetivoActual != null;

    private void Awake()
    {
        /** Inicializar estados */
        EstadoPatrulla.Inicializar(this);
        EstadoAtaque.Inicializar(this);
        EstadoMuerte.Inicializar(this);
        EstadoReaparicion.Inicializar(this);
    }

    public void CambiarEstado(EstadoDiablo nuevoEstado)
    {
        if (estadoActual != null)
            estadoActual.Salir();

        estadoActual = nuevoEstado;
        estadoActual.Entrar();
    }

    private void Update()
    {
        estadoActual.Actualizar();
    }

    /** Buscar objetivo usando sensores */
    public bool BuscarObjetivo()
    {
        for (int i = 0; i < Sensores.Length; i++)
        {
            if (Sensores[i] == null) continue;

            IAgarraObjetos obj = Sensores[i].DetectarObjetivo(out Transform mano);
            if (obj == null) continue;

            ObjetivoActual = obj;
            PuntoManoObjetivo = mano;
            return true;
        }

        ObjetivoActual = null;
        PuntoManoObjetivo = null;
        return false;
    }

    /** Metodo que es llamado cuando el Diablo muere */
    public void Matar()
    {
        CambiarEstado(EstadoMuerte);
    }

    /** Metodo que es llamado cuando el Diablo debe reaparecer */
    public void Reaparecer()
    {
        CambiarEstado(EstadoReaparicion);
    }

    public void AsignarPuntosDePatrullaje(Transform[] nuevosPuntos)
    {
        Movimiento.SetPuntosPatrulla(nuevosPuntos); // Asignamos los puntos de patrullaje
    }
}
