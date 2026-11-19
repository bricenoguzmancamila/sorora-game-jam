using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class Jugador : MonoBehaviour
{
    [SerializeField] private float velocidad = 3f;
    [SerializeField] private float framesPorSegundo = 8f;

    [Header("sprites de caminar (en orden)")]
    [SerializeField] private Sprite[] arriba;
    [SerializeField] private Sprite[] abajo;
    [SerializeField] private Sprite[] izquierda;
    [SerializeField] private Sprite[] derecha;

    [Tooltip("no hay izq, mirror der")]
    [SerializeField] private bool voltearDerechaParaIzquierda = false;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Vector2 direccion;
    private Sprite[] animacionActual;
    private float tiempo;
    private int frame;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animacionActual = abajo;
    }

    void Update()
    {
        direccion = Vector2.zero;
        Keyboard k = Keyboard.current;
        if (k != null)
        {
            if (k.wKey.isPressed || k.upArrowKey.isPressed)    direccion.y += 1;
            if (k.sKey.isPressed || k.downArrowKey.isPressed)  direccion.y -= 1;
            if (k.dKey.isPressed || k.rightArrowKey.isPressed) direccion.x += 1;
            if (k.aKey.isPressed || k.leftArrowKey.isPressed)  direccion.x -= 1;
        }
        direccion = direccion.normalized;
        Animar();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = direccion * velocidad;
    }

    void Animar()
    {
        if (direccion == Vector2.zero)
        {
            frame = 0;
            tiempo = 0f;
            MostrarFrame();
            return;
        }

        bool voltear = false;
        if (Mathf.Abs(direccion.x) > Mathf.Abs(direccion.y))
        {
            if (direccion.x > 0)
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
            animacionActual = direccion.y > 0 ? arriba : abajo;
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
}