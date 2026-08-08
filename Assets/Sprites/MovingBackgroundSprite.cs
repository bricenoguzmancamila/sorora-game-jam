using UnityEngine;
public class MovingBackgroundSprite : MonoBehaviour
{
    public float speed=2f;
    public float destroyX=-12f;
    void Update()
    {
        /*movimiento izq frame x frame*/
        transform.position+=Vector3.left*speed*Time.deltaTime;
        if (transform.position.x<destroyX){
            Destroy(gameObject);
        }
    }
}