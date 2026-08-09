using UnityEngine;
public class PuzzleManager : MonoBehaviour{
    public GameObject ganaste;
    private int piezasPuestas=0;
    public int totalPiezas;

    public void PiezaPuesta(){
        ++piezasPuestas;
        if(piezasPuestas>=totalPiezas){
            RompecabezasCompleto();
        }
    }
    void RompecabezasCompleto(){
        ganaste.SetActive(true);
    }
}