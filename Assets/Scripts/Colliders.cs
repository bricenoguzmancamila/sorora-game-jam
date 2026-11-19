using UnityEngine;
using UnityEditor;

public static class Colliders
{
    [MenuItem("Tools/agregar colliders a arboles y arbustos")]
    static void Agregar()
    {
        int total = 0;
        foreach (string nombre in new[] { "arbustos", "arboles" })
        {
            GameObject padre = GameObject.Find(nombre);
            if (padre == null) { Debug.LogWarning("none: " + nombre); continue; }

            foreach (SpriteRenderer sr in padre.GetComponentsInChildren<SpriteRenderer>(true))
            {
                GameObject go = sr.gameObject;
                foreach (Collider2D c in go.GetComponents<Collider2D>())
                    Undo.DestroyObjectImmediate(c);
                Undo.AddComponent<PolygonCollider2D>(go); 
                total++;
            }
        }
        Debug.Log("colliders added: " + total);
    }
}