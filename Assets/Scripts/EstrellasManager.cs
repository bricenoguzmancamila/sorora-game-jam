using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EstrellasManager : MonoBehaviour{
    public RectTransform[] estrellas;
    public float duracionAnimacionEstrellas=2f;
    public CanvasGroup fadePanel;
    public float duracionFade=1f;
    public string siguienteEscena;
    public float tiempoParaFadeOut=1f;
    void Start(){
        fadePanel.alpha=1f;
        StartCoroutine(FadeIn());
        StartCoroutine(AnimarEstrellas());
        StartCoroutine(EsperarYFadeOut());
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
    IEnumerator AnimarEstrellas(){
        Vector2[] posicionesOG=new Vector2[estrellas.Length];
        for (int estrella=0;estrella<estrellas.Length;++estrella){
            posicionesOG[estrella]=estrellas[estrella].anchoredPosition;
        }
        float tiemp=0f;
        while (tiemp<duracionAnimacionEstrellas)
        {
            tiemp+=Time.deltaTime;
            float progreso=tiemp/duracionAnimacionEstrellas;

            for (int estrella=0; estrella<estrellas.Length;++estrella)
            {
                int siguiente=(estrella+1)%estrellas.Length;
                estrellas[estrella].anchoredPosition=Vector2.Lerp(posicionesOG[estrella],posicionesOG[siguiente],progreso);
            }
            yield return null;
        }
    }
    IEnumerator EsperarYFadeOut(){
        yield return new WaitForSeconds(duracionAnimacionEstrellas);
        float tiemp=0f;
        fadePanel.alpha=0f;
        while (tiemp<duracionFade)
        {
            tiemp+=Time.deltaTime;
            fadePanel.alpha=tiemp/duracionFade;
            yield return null;
        }
        fadePanel.alpha = 1f;
        SceneManager.LoadScene(siguienteEscena);
    }
}