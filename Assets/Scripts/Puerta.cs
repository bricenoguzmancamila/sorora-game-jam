using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SpriteRenderer))]
public class Puerta : MonoBehaviour
{
    [Header("animacion")]
    [SerializeField] private Sprite[] frames;            // los 5 frames de la puerta, en orden
    [SerializeField] private float framesPorSegundo = 8f;

    [Header("colision")]
    [SerializeField] private Collider2D colliderSolido;

    [Header("salir")]
    [SerializeField] private FadeLaberinto fade;
    [SerializeField] private string escenaSiguiente = "";
    [SerializeField] private GameObject panelGanaste; 

    [Header("op")]
    [SerializeField] private GameObject mensajeSinLlave; 
    [SerializeField] private AudioClip sonidoAbrir;

    private SpriteRenderer sr;
    private bool abriendo = false;
    private bool abierta = false;
    private bool saliendo = false;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        if (frames.Length > 0) sr.sprite = frames[0];
        if (panelGanaste != null) panelGanaste.SetActive(false);
        if (mensajeSinLlave != null) mensajeSinLlave.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D otro)
    {
        InventarioJugador inv = otro.GetComponentInParent<InventarioJugador>();
        if (inv == null || abriendo || abierta) return;

        if (inv.tieneLlave)
            StartCoroutine(Abrir());
        else if (mensajeSinLlave != null)
            StartCoroutine(MostrarMensaje());
    }

    void OnTriggerStay2D(Collider2D otro)
    {
        if (abierta && !saliendo && otro.GetComponentInParent<InventarioJugador>() != null)
            Salir();
    }

    IEnumerator Abrir()
    {
        abriendo = true;

        if (sonidoAbrir != null)
            AudioSource.PlayClipAtPoint(sonidoAbrir, Camera.main.transform.position);

        foreach (Sprite f in frames)
        {
            sr.sprite = f;
            yield return new WaitForSeconds(1f / framesPorSegundo);
        }

        if (colliderSolido != null) colliderSolido.enabled = false;

        yield return new WaitForSeconds(0.4f);
        abierta = true;
        abriendo = false;
    }

    IEnumerator MostrarMensaje()
    {
        mensajeSinLlave.SetActive(true);
        yield return new WaitForSeconds(2f);
        mensajeSinLlave.SetActive(false);
    }

    void Salir()
    {
        saliendo = true;

        if (fade != null)
        {
            fade.IrASiguienteEscena();
        }
        else if (!string.IsNullOrEmpty(escenaSiguiente))
        {
            SceneManager.LoadScene(escenaSiguiente);
        }
        else if (panelGanaste != null)
        {
            panelGanaste.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}