using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using static StaticVariables;
using Unity.Services.Analytics;
using static EventManager;

public class Salida : MonoBehaviour
{
    public GameObject jugadorAsignado;
    private Animator animPuerta;
    public AbrirPuerta palancaAsociada;
    private static int jugadoresEnSalida = 0;
    public bool requiereAbrir = false;

    //VARIABLES PARA CADA NIVEL INDIVIDUAL
    private float tiempoNivel = 0f;
    private bool nivelEnCurso = true;
    private static int muertesNivel = 0;

    void Start()
    {
        animPuerta = GetComponent<Animator>();
        tiempoNivel = 0f;
        nivelEnCurso = true;
    }

    //CONTADOR PARA EL TIEMPO
    void Update()
    {
        if (nivelEnCurso)
        {
            tiempoNivel += Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == jugadorAsignado)
        {
            if (requiereAbrir && palancaAsociada != null && !palancaAsociada.EstaAbierta())
            {
                Debug.Log("La puerta está cerrada, no puedes pasar.");
                return;
            }
            if (!requiereAbrir)
            {
                AnimacionesControlador.SetBool(animPuerta, "estaAbierta", true);
            }
            if (!requiereAbrir && palancaAsociada != null)
            {
                palancaAsociada.Abrir();
            }

            jugadoresEnSalida++;

            if (jugadoresEnSalida == 2)
            {
                if (!requiereAbrir || (palancaAsociada != null && palancaAsociada.EstaAbierta()))
                {
                    PasarNivel();
                }
                else
                {
                    Debug.Log("Ambos están en la salida, pero la puerta aún está cerrada.");
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (animPuerta != null)
        {
            animPuerta.SetBool("estaAbierta", false);
        }
        if (other.gameObject == jugadorAsignado)
        {
            if (jugadoresEnSalida > 0)
            {
                jugadoresEnSalida--;
            }
            Debug.Log($"{jugadorAsignado.name} salió de la salida. Jugadores en salida: {jugadoresEnSalida}");
        }
    }

    private void PasarNivel()
    {
        nivelEnCurso = false;
        SessionData.time = Mathf.RoundToInt(tiempoNivel);

        if (GameStats.Instance != null)
            GameStats.Instance.SumarTiempo(tiempoNivel);

        Debug.Log("Ambos jugadores están en sus salidas");

        //EVENTO LEVELCOMPLETE
        Debug.Log("Completo un nivel");
        Debug.Log($"¡Completaste el nivel {SessionData.level} en {SessionData.time} segundos!");
        Debug.Log($"Te moriste {muertesNivel} veces en este nivel."); // <-- SOLO LAS DE ESTE NIVEL
        muertesNivel = 0; // Reiniciar para el próximo nivel
        TransicionEscena.Instance.Disolversalida(SceneManager.GetActiveScene().buildIndex + 1);


        LevelCompleteEvent LevelComplete = new LevelCompleteEvent
        {
            level = SessionData.level,
            time = SessionData.time,
            death = SessionData.death,
        };

        AnalyticsService.Instance.RecordEvent(LevelComplete);

        SessionData.level++; //CONTADOR DE NIVEL
    }


    //ACUMULADOR MUERTE POR NIVEL
    public void SumarMuerteNivel()
    {
        muertesNivel++;
    }
}
