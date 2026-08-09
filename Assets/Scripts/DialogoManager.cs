using UnityEngine;
using TMPro;
using System.Collections;

public class DialogoManager : MonoBehaviour{
    public GameObject protagonista;
    public GameObject dialogo;
    public TMP_Text textoDialogo;
    public string[] lineas;
    public float delay=1f;
    public float velocidadLetra=0.05f;
    private int lineaActual=-1;

    public CanvasGroup fadePersonaje;
    public float duracionFade=1f;

    void Start(){
        fadePersonaje.alpha=0f;
        protagonista.SetActive(false);
        dialogo.SetActive(false);
        Invoke(nameof(inicializacion),delay);
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Space) && lineaActual>=0){
            lineaSiguiente();
        }
    }

    void inicializacion(){
        protagonista.SetActive(true);
        StartCoroutine(FadeIn(fadePersonaje));
        Invoke(nameof(primeraLinea),delay);
    }

    void primeraLinea()
    {
        dialogo.SetActive(true);;
        lineaActual=0;
        StartCoroutine(letraXLetra());
    }

    IEnumerator FadeIn(CanvasGroup panel){
        float tiemp=0f;
        while (tiemp<duracionFade){
            tiemp+=Time.deltaTime;
            panel.alpha=tiemp/duracionFade;
            yield return null;
        }
        panel.alpha=1f;
    }

    void lineaSiguiente(){
        ++lineaActual;
        if (lineaActual < lineas.Length){
            StartCoroutine(letraXLetra());
        }
        else{
            dialogo.SetActive(false);
        }
    }
    IEnumerator letraXLetra(){
        textoDialogo.text="";
        foreach (char letra in lineas[lineaActual]){
            textoDialogo.text+=letra;
            yield return new WaitForSeconds(velocidadLetra);
        }
    }
}