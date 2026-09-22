using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

/*script de agente IA para el movimiento de el enemigo*/

public class EnemigoIA : MonoBehaviour {

    /*para que el enemigo sepa dónde está el jugador*/
    [SerializeField] Transform _player;
    private NavMeshAgent _agent;
    

    void Start()
    {
        _agent=GetComponent<NavMeshAgent>();
        _agent.updateUpAxis=false;
        _agent.updateRotation=false;
    }

    
    void Update()
    {
        /*lo q le dice al agent que busque al position*/
        _agent.SetDestination(_player.position);
    }
}
