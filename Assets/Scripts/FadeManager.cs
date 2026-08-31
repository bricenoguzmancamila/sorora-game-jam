using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour{
    public CanvasGroup fadePanel;
    public float duracionFade = 1f;
    public string siguienteEscena;
    public Dialogo dialogo;

    void Start(){
        fadePanel.alpha = 1f;
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn(){
        float tiempo=0f;
        while (tiempo<duracionFade){
            tiempo+=Time.deltaTime;
            fadePanel.alpha=1f-(tiempo/duracionFade);
            yield return null;
        }
        fadePanel.alpha=0f;
        dialogo.IniciarDialogo();
        StartCoroutine(EsperarDialogoYFadeOut());
    }
    /*espera a que el dialogo termine con yield return y luego pasa un segundo y tira el fadeout*/
    IEnumerator EsperarDialogoYFadeOut(){
        /*espera a que dialogo termine con yield return*/
        yield return new WaitUntil(() => dialogo.DialogoTerminado());
        yield return new WaitForSeconds(0.005f);
        float tiempo=0f;
        fadePanel.alpha=0f;
        while (tiempo<duracionFade){
            tiempo += Time.deltaTime;
            fadePanel.alpha = tiempo / duracionFade;
            yield return null;
        }
        fadePanel.alpha = 1f;
        SceneManager.LoadScene(siguienteEscena);
    }
}