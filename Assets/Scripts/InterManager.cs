using UnityEngine;

public class InterManager : MonoBehaviour{
    public GameObject sprite1;
    public GameObject sprite2;
    public float retrasoCambio=0.5f;
    private bool mostrandoSprite1=true;
    private bool puedeCambiar=true;

    void Start(){
        sprite1.SetActive(true);
        sprite2.SetActive(false);
    }
    void Update(){
        if (puedeCambiar && Input.GetKeyDown(KeyCode.Space)){
            Invoke(nameof(CambiarSprite),retrasoCambio);
        }
    }
    void CambiarSprite(){
        mostrandoSprite1=!mostrandoSprite1;
        sprite1.SetActive(mostrandoSprite1);
        sprite2.SetActive(!mostrandoSprite1);
    }
    public void Bloquear(){
        puedeCambiar=false;
        CancelInvoke(nameof(CambiarSprite));
    }
}