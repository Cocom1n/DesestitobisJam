using UnityEngine;
using UnityEngine.AI;

public class MovimientoAleatorio : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent agent;
    [SerializeField] private float radioBusqueda = 10f; 
    [SerializeField] private float tiempoBusqueda = 4f;  //el tiempo maximo que seva a quedar yendo hasta el punto       
    [SerializeField] private float tiempoEspera = 1f; //El tiempo q se keda esperando
    private float timerYendo;
    private float timerSpera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        timerYendo = tiempoBusqueda;
    }

    // Update is called once per frame
    void Update()
    {
        timerYendo += Time.deltaTime;
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            timerSpera += Time.deltaTime;
        }
        else
        {
            timerSpera = 0f;
        }
        
        if (timerYendo >= tiempoBusqueda || agent.remainingDistance <= agent.stoppingDistance && timerSpera >= tiempoEspera)
        {
            Vector3 newPos = RandomNavSphere(transform.position, radioBusqueda, -1);
            agent.SetDestination(newPos);
            timerYendo = 0;
            timerSpera = 0;
        }
    }
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;

        UnityEngine.AI.NavMeshHit navHit;
        UnityEngine.AI.NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
}
