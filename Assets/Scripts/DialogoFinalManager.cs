using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class DialogoFinalManager : MonoBehaviour{
    public GameObject dialogo;
    public TMP_Text textoDialogo;
    public string[] lineas;
    public float delay=1f;
    public float velocidadLetra=0.05f;
    public string siguienteEscena;
    private int lineaActual=-1;

    void Start(){
        dialogo.SetActive(false);
        Invoke(nameof(inicializacion),delay);
    }
    void Update(){
        if (Input.GetKeyDown(KeyCode.Space)&&lineaActual>=0){
            lineaSiguiente();
        }
    }
    void inicializacion(){
        dialogo.SetActive(true);
        lineaActual=0;
        StartCoroutine(letraXLetra());
    }
    void lineaSiguiente(){
        ++lineaActual;
        if (lineaActual<lineas.Length){
            StartCoroutine(letraXLetra());
        }else{
            SceneManager.LoadScene(siguienteEscena);
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