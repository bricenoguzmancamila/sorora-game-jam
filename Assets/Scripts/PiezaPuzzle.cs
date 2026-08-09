using UnityEngine;
using UnityEngine.EventSystems;

public class PiezaPuzzle : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler{
    public Vector2 posicionCorrecta;
    public float distanciaMax=50f;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private bool puesta=false;
    void Awake(){
        rectTransform=GetComponent<RectTransform>();
        canvasGroup=GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData){
        if (puesta){
            return;
        }
        canvasGroup.blocksRaycasts = false;
    }
    public void OnDrag(PointerEventData eventData){
        if (puesta){
            return;
        }
        rectTransform.anchoredPosition+=eventData.delta/rectTransform.lossyScale.x;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (puesta){
            return;
        }
        canvasGroup.blocksRaycasts=true;
        float distancia=Vector2.Distance(rectTransform.anchoredPosition,posicionCorrecta);
        if (distancia<distanciaMax){
            rectTransform.anchoredPosition=posicionCorrecta;
            puesta=true;
            PuzzleManager manager=FindObjectOfType<PuzzleManager>();
            if (manager!=null){
                manager.PiezaPuesta();
            }
        }
    }
}