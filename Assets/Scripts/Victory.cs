using UnityEngine;
using static StaticVariables;
using Unity.Services.Analytics;
using static EventManager;

public class Victory : MonoBehaviour
{
    void Start()
    {


        //EVENTO GAME COMPLETE

        if (GameStats.Instance != null)
        {
            SessionData.time = Mathf.RoundToInt(GameStats.Instance.tiempoTotal);
            SessionData.death = GameStats.Instance.muertesTotales;
            Debug.Log($"Tiempo total: {GameStats.Instance.tiempoTotal:F2} segundos");
            Debug.Log($"Muertes totales: {GameStats.Instance.muertesTotales}");
        }

        Debug.Log("GameComplete");
        Debug.Log($"¡Ganaste el nivel {StaticVariables.SessionData.level} en {StaticVariables.SessionData.time} segundos, con {StaticVariables.SessionData.death} muertes!");

        GameCompleteEvent GameComplete = new GameCompleteEvent
        {
            time = SessionData.time,
            death = SessionData.death,
        };

        AnalyticsService.Instance.RecordEvent(GameComplete);

        // Mostrar tiempo y muertes totales

    }
}
