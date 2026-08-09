// Este script va en un GameObject vacío (por ejemplo "Spawner") en la escena.
using UnityEngine;
public class BackgroundSpawner : MonoBehaviour{
    public GameObject[] spritePrefabs;
    public float spawnInterval=2f;
    public float spawnX=12f;
    public float minY=-3f;
    public float maxY=3f;
    private float timer;
    void Update()
    {
        timer+=Time.deltaTime;
        if (timer>=spawnInterval)
        {
            timer = 0f;
            SpawnSprite();
        }
    }

    void SpawnSprite()
    {
        if (spritePrefabs.Length == 0) return;

        // Elige un sprite al azar de la lista (si solo tienes uno, siempre usa ese)
        GameObject prefab = spritePrefabs[Random.Range(0, spritePrefabs.Length)];

        float randomY = Random.Range(minY, maxY);
        Vector3 spawnPos = new Vector3(spawnX, randomY, 0f);

        Instantiate(prefab, spawnPos, Quaternion.identity);
    }
}