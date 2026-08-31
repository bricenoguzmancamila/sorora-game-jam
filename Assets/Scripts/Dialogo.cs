using UnityEngine;
using TMPro;
using System.Collections;

public class Dialogo : MonoBehaviour
{
    /* definiciones */
    public GameObject cajaDialogo;
    public GameObject personajeCara;
    public TMP_Text textoDialogo;
    public string[] lineas;
    public float velocidadLetra = 0.05f;
    private int lineaActual = -1;
    private bool dialogoActivo = false;
    private bool dialogoTerminado = false;

    void Awake(){
        cajaDialogo.SetActive(false);
        personajeCara.SetActive(false);
    }
    /*lo llama fade manager despues del fade-in al inicio*/
    public void IniciarDialogo(){
        cajaDialogo.SetActive(true);
        personajeCara.SetActive(true);
        lineaActual=0;
        dialogoActivo=true;
        dialogoTerminado=false;
        StartCoroutine(letraXLetra());
    }
    /*update cuando dan click a espacio*/
    void Update(){
        if(!dialogoActivo){return;}
        if(Input.GetKeyDown(KeyCode.Space)){
            lineaSiguiente();
        }
    }
    /*fade manager revisa si el dialogo termino para poder hacer fade-out*/
    public bool DialogoTerminado(){
        return dialogoTerminado;
    }
    /*pasa las lineas*/
    void lineaSiguiente(){
        lineaActual++;
        if (lineaActual<lineas.Length){
            StopAllCoroutines();
            StartCoroutine(letraXLetra());
        }else{
            cajaDialogo.SetActive(false);
            personajeCara.SetActive(false);
            dialogoActivo=false;
            dialogoTerminado=true;
        }
    }
    /* la linea de texto sale letra por letra */
    IEnumerator letraXLetra(){
        textoDialogo.text="";
        foreach(char letra in lineas[lineaActual]){
            textoDialogo.text+=letra;
            yield return new WaitForSeconds(velocidadLetra);
        }
    }
}