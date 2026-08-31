using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class DialogoInternoInteractivo : MonoBehaviour{

    /*definiciones*/
    public GameObject dialogo;
    public TMP_Text textoDialogo;
    public string[] lineas;
    public float delay=1f;
    public float velocidadLetra=0.05f;
    public float retrasoCambio=0.5f;
    public string siguienteEscena;
    public InterManager interManager;
    private int lineaActual=-1;

    /*start*/
    void Start(){
        dialogo.SetActive(false);
        Invoke(nameof(inicializacion),delay);
    }
    /*update*/
    void Update(){
        if (Input.GetKeyDown(KeyCode.Space)&&lineaActual>=0){
            Invoke(nameof(lineaSiguiente),retrasoCambio);
        }
    }
    /*inicializacion*/
    void inicializacion(){
        dialogo.SetActive(true);
        lineaActual=0;
        StartCoroutine(letraXLetra());
    }
    /*linea siguiente*/
    void lineaSiguiente(){
        ++lineaActual;
        if (lineaActual<lineas.Length){
            if (lineaActual==lineas.Length-1){
                interManager.Bloquear();
            }
            StartCoroutine(letraXLetra());
        }else{
            SceneManager.LoadScene(siguienteEscena);
        }
    }
    /*linea de texto sale letra por letra*/
    IEnumerator letraXLetra(){
        textoDialogo.text="";
        foreach (char letra in lineas[lineaActual]){
            textoDialogo.text+=letra;
            yield return new WaitForSeconds(velocidadLetra);
        }
    }
}