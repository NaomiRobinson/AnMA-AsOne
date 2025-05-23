using UnityEngine;

public class Victory : MonoBehaviour
{
    void Start()
    {
        //EVENTO GAME COMPLETE
        Debug.Log("GameComplete");
        Debug.Log($"¡Ganaste el nivel {StaticVariables.SessionData.level} en {StaticVariables.SessionData.time} segundos, con {StaticVariables.SessionData.death} muertes!");

        // Mostrar tiempo y muertes totales
        if (GameStats.Instance != null)
        {
            Debug.Log($"Tiempo total: {GameStats.Instance.tiempoTotal:F2} segundos");
            Debug.Log($"Muertes totales: {GameStats.Instance.muertesTotales}");
        }
    }
}
