using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/*para no tocar el otro fade manager xq me da vara joder algo de las otras escenas*/

public class FadeLaberinto : MonoBehaviour
{
    public CanvasGroup fadePanel;
    public float duracionFade = 1f;
    public string siguienteEscena;

    private bool cambiandoEscena = false;

    void Start()
    {
        fadePanel.alpha = 1f;
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float tiempo = 0f;
        while (tiempo < duracionFade)
        {
            tiempo += Time.deltaTime;
            fadePanel.alpha = 1f - (tiempo / duracionFade);
            yield return null;
        }
        fadePanel.alpha = 0f;
        fadePanel.blocksRaycasts = false;
    }

    /* la puerta llama esto para pasar a la siguiente escena */
    public void IrASiguienteEscena()
    {
        if (cambiandoEscena) return;
        StartCoroutine(FadeOutYCargar());
    }

    IEnumerator FadeOutYCargar()
    {
        cambiandoEscena = true;
        fadePanel.blocksRaycasts = true;
        Time.timeScale = 0f;

        float tiempo = 0f;
        while (tiempo < duracionFade)
        {
            tiempo += Time.unscaledDeltaTime;
            fadePanel.alpha = tiempo / duracionFade;
            yield return null;
        }
        fadePanel.alpha = 1f;

        Time.timeScale = 1f;
        SceneManager.LoadScene(siguienteEscena);
    }
}