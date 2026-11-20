using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

[System.Serializable]
public class LineaDialogo
{
    public string nombre;
    public int indicePersonaje;   // 0 = personaje 1, 1 = personaje 2
    public Sprite sprite;         // opcional: cambia el sprite del que habla
    [TextArea(2, 4)] public string texto;
}

public class DIalogoMultiple : DialogoBase
{
    /*definiciones*/
    public GameObject dialogo;
    public TMP_Text textoDialogo;
    public TMP_Text textoNombre;
    public Image[] personajes;          // las imagenes de los 2 personajes
    public LineaDialogo[] lineas;
    public Color colorApagado = new Color(0.4f, 0.4f, 0.4f, 1f);
    public float velocidadLetra = 0.05f;
    public float retrasoCambio = 0.5f;

    private int lineaActual = -1;
    private bool escribiendo = false;
    private bool cambiando = false;
    private bool terminado = false;
    private Coroutine escritura;

    void Awake()
    {
        dialogo.SetActive(false);
    }

    /*lo llama el FadeManager despues del fade-in*/
    public override void IniciarDialogo()
    {
        dialogo.SetActive(true);
        terminado = false;
        lineaActual = 0;
        mostrarLinea();
    }

    /*el FadeManager revisa esto para hacer el fade-out*/
    public override bool DialogoTerminado()
    {
        return terminado;
    }

    void Update()
    {
        if (lineaActual < 0 || terminado || cambiando) { return; }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (escribiendo)
            {
                /*primer espacio: completa la linea*/
                StopCoroutine(escritura);
                textoDialogo.text = lineas[lineaActual].texto;
                escribiendo = false;
            }
            else
            {
                /*segundo espacio: pasa a la siguiente*/
                StartCoroutine(lineaSiguiente());
            }
        }
    }

    IEnumerator lineaSiguiente()
    {
        cambiando = true;
        yield return new WaitForSeconds(retrasoCambio);
        ++lineaActual;
        if (lineaActual < lineas.Length)
        {
            mostrarLinea();
        }
        else
        {
            dialogo.SetActive(false);
            terminado = true;
        }
        cambiando = false;
    }

    void mostrarLinea()
    {
        LineaDialogo l = lineas[lineaActual];
        textoNombre.text = l.nombre;

        /*cambia el sprite del personaje que habla (si la linea trae uno)*/
        if (l.sprite != null)
        {
            personajes[l.indicePersonaje].sprite = l.sprite;
        }

        /*el que habla se ve normal, el otro oscurecido*/
        for (int i = 0; i < personajes.Length; i++)
        {
            personajes[i].color = (i == l.indicePersonaje) ? Color.white : colorApagado;
        }
        escritura = StartCoroutine(letraXLetra(l.texto));
    }

    IEnumerator letraXLetra(string texto)
    {
        escribiendo = true;
        textoDialogo.text = "";
        foreach (char letra in texto)
        {
            textoDialogo.text += letra;
            yield return new WaitForSeconds(velocidadLetra);
        }
        escribiendo = false;
    }
}