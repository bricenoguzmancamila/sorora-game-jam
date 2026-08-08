using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour{
    public CanvasGroup fadePanel;
    public float duracionFade=1f;
    public string siguienteEscena;
    public float tiempoParaFadeOut=1f;
    void Start(){
        fadePanel.alpha=1f;
        StartCoroutine(FadeIn());
        Update();
    }
    void Update(){
        if (Input.GetKeyDown(KeyCode.A)){
            StartCoroutine(EsperarYFadeOut());
        }
    }
    IEnumerator FadeIn(){
        float tiemp=0f;
        while (tiemp<duracionFade){
            tiemp+=Time.deltaTime;
            fadePanel.alpha=1f-(tiemp/duracionFade);
            yield return null;
        }
        fadePanel.alpha=0f;
    }
    IEnumerator EsperarYFadeOut(){
        yield return new WaitForSeconds(1);
        float tiemp=0f;
        fadePanel.alpha=0f;
        while (tiemp<duracionFade){
            tiemp+=Time.deltaTime;
            fadePanel.alpha=tiemp/duracionFade;
            yield return null;
        }
        fadePanel.alpha = 1f;
        SceneManager.LoadScene(siguienteEscena);
    }
}