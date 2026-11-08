using UnityEngine;
using UnityEngine.AI;

/*script de agente IA para el movimiento de el enemigo*/

public class EnemigoIA : MonoBehaviour {

    /*para que el enemigo sepa dónde está el jugador*/
    [SerializeField] Transform _player;
    [SerializeField] float _attackRange;
    [SerializeField] float _speed;
    [SerializeField] Transform[] _wayPoints;
    public int _currentWaypointIndex;
    private NavMeshAgent _agent;
    

    void Start()
    {
        _agent=GetComponent<NavMeshAgent>();
        _agent.updateUpAxis=false;
        _agent.updateRotation=false;
        _agent.speed = _speed;
    }

    
    void Update()
    {
        float playerDistance = Vector3.Distance(_player.position,transform.position);
        Debug.Log($"Distance:(playerDistance)");
        if(playerDistance<_attackRange){
            /*lo q le dice al agent que busque al position*/
            _agent.SetDestination(_player.position);
            return; 
        }
        if (_agent.remainingDistance < 0.5f)
        {
            _currentWaypointIndex = (_currentWaypointIndex+1) % _wayPoints.Length;
            _agent.SetDestination(_wayPoints[_currentWaypointIndex].position);  
        }
    }
}
