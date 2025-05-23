using UnityEngine;

public class Victory : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("GameComplete");
        Debug.Log($"¡Ganaste el nivel {StaticVariables.SessionData.level} en {StaticVariables.SessionData.time} segundos, con {StaticVariables.SessionData.death} muertes!");

        // Mostrar tiempo y muertes totales
        if (GameStats.Instance != null)
        {
            Debug.Log($"Tiempo total: {GameStats.Instance.tiempoTotal:F2} segundos");
            Debug.Log($"Muertes totales: {GameStats.Instance.muertesTotales}");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
