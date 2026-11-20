using UnityEngine;

public class Llave : MonoBehaviour
{
    [SerializeField] private float alturaFlotar = 0.08f;
    [SerializeField] private float velocidadFlotar = 2.5f;
    [SerializeField] private AudioClip sonido;
    [SerializeField] private GameObject iconoUI;

    private Vector3 posInicial;

    void Start()
    {
        posInicial = transform.position;
        if (iconoUI != null) iconoUI.SetActive(false);
    }

    void Update()
    {
        transform.position = posInicial + Vector3.up * Mathf.Sin(Time.time * velocidadFlotar) * alturaFlotar;
    }

    void OnTriggerEnter2D(Collider2D otro)
    {
        InventarioJugador inv = otro.GetComponentInParent<InventarioJugador>();
        if (inv == null) return;

        inv.tieneLlave = true;
        if (sonido != null) AudioSource.PlayClipAtPoint(sonido, Camera.main.transform.position);
        if (iconoUI != null) iconoUI.SetActive(true);
        Destroy(gameObject);
    }
}