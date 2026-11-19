using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(SpriteRenderer))]
public class EnemigoIA : MonoBehaviour
{
    [Header("persec")]
    [SerializeField] private Transform jugador; 
    [SerializeField] private float distanciaVision = 5f;

    [Header("pat")]
    [SerializeField] private float radioPatrulla = 4f;
    [SerializeField] private float esperaEntrePuntos = 1f;

    [Header("animacion")]
    [SerializeField] private float framesPorSegundo = 8f;
    [SerializeField] private Sprite[] arriba;
    [SerializeField] private Sprite[] abajo;
    [SerializeField] private Sprite[] izquierda;
    [SerializeField] private Sprite[] derecha;
    [SerializeField] private bool voltearDerechaParaIzquierda = false;

    private NavMeshAgent agente;
    private SpriteRenderer sr;
    private Sprite[] animacionActual;
    private float tiempo;
    private int frame;
    private float esperando;

    void Awake()
    {
        agente = GetComponent<NavMeshAgent>();
        sr = GetComponent<SpriteRenderer>();
        agente.updateRotation = false;
        agente.updateUpAxis = false;
        animacionActual = abajo;
    }

    void Start()
    {
        if (jugador == null)
        {
            GameObject j = GameObject.FindWithTag("player");
            if (j != null) jugador = j.transform;
        }
    }

    void Update()
    {
        bool loVe = jugador != null &&
                    Vector2.Distance(transform.position, jugador.position) <= distanciaVision;

        if (loVe) agente.SetDestination(jugador.position);
        else Patrullar();

        Animar(agente.velocity);
    }

    void Patrullar()
    {
        if (agente.pathPending) return;
        if (agente.remainingDistance > agente.stoppingDistance) return;

        esperando += Time.deltaTime;
        if (esperando < esperaEntrePuntos) return;
        esperando = 0f;

        Vector3 puntoAzar = transform.position + (Vector3)(Random.insideUnitCircle * radioPatrulla);
        if (NavMesh.SamplePosition(puntoAzar, out NavMeshHit hit, radioPatrulla, NavMesh.AllAreas))
            agente.SetDestination(hit.position);
    }

    void Animar(Vector3 velocidad)
    {
        if (velocidad.sqrMagnitude < 0.01f)
        {
            frame = 0;
            tiempo = 0f;
            MostrarFrame();
            return;
        }

        bool voltear = false;
        if (Mathf.Abs(velocidad.x) > Mathf.Abs(velocidad.y))
        {
            if (velocidad.x > 0)
                animacionActual = derecha;
            else if (voltearDerechaParaIzquierda)
            {
                animacionActual = derecha;
                voltear = true;
            }
            else
                animacionActual = izquierda;
        }
        else
        {
            animacionActual = velocidad.y > 0 ? arriba : abajo;
        }
        sr.flipX = voltear;

        tiempo += Time.deltaTime;
        if (tiempo >= 1f / framesPorSegundo)
        {
            tiempo -= 1f / framesPorSegundo;
            frame++;
        }
        MostrarFrame();
    }

    void MostrarFrame()
    {
        if (animacionActual == null || animacionActual.Length == 0) return;
        sr.sprite = animacionActual[frame % animacionActual.Length];
    }

    void OnTriggerEnter2D(Collider2D otro)
{
    if (otro.CompareTag("player"))
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
}

}