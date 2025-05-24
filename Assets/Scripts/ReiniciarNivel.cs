using UnityEngine;
using UnityEngine.SceneManagement;
using static StaticVariables;
using Unity.Services.Analytics;
using static EventManager;

public class ReiniciarNivel : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Obstacle"))
        {
            Reiniciar(collision.gameObject);
        }
    }

    public void Reiniciar(GameObject causante)
    {
        Debug.Log("GameOver");
        string type = causante.tag;
        string name = "Desconocido";

        Identidad identidad = causante.GetComponent<Identidad>();
        if (identidad != null)
        {
            name = identidad.nombre.ToString();
        }

        // Sumar una muerte global
        SessionData.death++;
        if (GameStats.Instance != null)
            GameStats.Instance.SumarMuerte();

        // Sumar una muerte al nivel actual
        var salida = FindObjectOfType<Salida>();
        if (salida != null)
            salida.SumarMuerteNivel();

        //EVENTO GAMEOVER
        Debug.Log($"Se murió en el nivel {SessionData.level}. Lo mató {name} del grupo {type}.");
        SessionData.name = name;
        SessionData.type = type;



        GameOverEvent GameOver = new GameOverEvent
        {
            level = SessionData.level,
            name = SessionData.name,
            type = SessionData.type,
        };

        AnalyticsService.Instance.RecordEvent(GameOver);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
