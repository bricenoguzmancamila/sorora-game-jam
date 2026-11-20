using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;


public class Dialogo : DialogoBase
{
    /* definiciones */
    public GameObject cajaDialogo;
    public GameObject personajeCara;
    public TMP_Text textoDialogo;
    public string[] lineas;
    public Sprite[] spritesPorLinea;   // opcional: un sprite por cada linea
    public float velocidadLetra = 0.05f;
    private int lineaActual = -1;
    private bool dialogoActivo = false;
    private bool dialogoTerminado = false;
    private Image imagenCara;

    void Awake()
    {
        cajaDialogo.SetActive(false);
        if (personajeCara != null)
        {
            imagenCara = personajeCara.GetComponent<Image>();
            personajeCara.SetActive(false);
        }
    }
    /*lo llama fade manager despues del fade-in al inicio*/




    public override void IniciarDialogo()
    {
        cajaDialogo.SetActive(true);
        if (personajeCara != null) { personajeCara.SetActive(true); }
        lineaActual = 0;
        dialogoActivo = true;
        dialogoTerminado = false;
        mostrarLinea();
    }
    /*update cuando dan click a espacio*/
    void Update()
    {
        if (!dialogoActivo) { return; }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            lineaSiguiente();
        }
    }
    /*fade manager revisa si el dialogo termino para poder hacer fade-out*/
    public override bool DialogoTerminado()
    {
        return dialogoTerminado;
    }
    /*pasa las lineas*/
    void lineaSiguiente()
    {
        lineaActual++;
        if (lineaActual < lineas.Length)
        {
            mostrarLinea();
        }
        else
        {
            cajaDialogo.SetActive(false);
            if (personajeCara != null) { personajeCara.SetActive(false); }
            dialogoActivo = false;
            dialogoTerminado = true;
        }
    }
    /*cambia el sprite (si la linea trae uno) y arranca el texto*/
    void mostrarLinea()
    {
        if (imagenCara != null
           && lineaActual < spritesPorLinea.Length
           && spritesPorLinea[lineaActual] != null)
        {
            imagenCara.sprite = spritesPorLinea[lineaActual];
        }
        StopAllCoroutines();
        StartCoroutine(letraXLetra());
    }
    /* la linea de texto sale letra por letra */
    IEnumerator letraXLetra()
    {
        textoDialogo.text = "";
        foreach (char letra in lineas[lineaActual])
        {
            textoDialogo.text += letra;
            yield return new WaitForSeconds(velocidadLetra);
        }
    }
}