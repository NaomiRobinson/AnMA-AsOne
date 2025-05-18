using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AbrirPuerta : MonoBehaviour
{
    public enum TipoPalanca { Boton, Presion }
    public TipoPalanca tipo = TipoPalanca.Boton;

    public GameObject puertaAsociada;
    public GameObject palanca;

    private Animator animPuerta;
    private Animator animPalanca;

    private bool estaJugador = false;
    private bool puertaAbierta = false;
    private bool palancaActivada = false;

    private bool palancaUsada = false;
    private float timer;
    private bool timerMostrado = false;

    void Start()
    {
        animPuerta = puertaAsociada?.GetComponent<Animator>();
        animPalanca = palanca?.GetComponent<Animator>();
        timer = 0;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Nivel2")
        {
            TimerPalanca();
        }
        if (tipo == TipoPalanca.Boton && estaJugador && Input.GetKeyDown(KeyCode.Space) && !palancaUsada)
        {
            Debug.Log("Interactuo con palanca");
            palancaActivada = !palancaActivada;
            puertaAbierta = !puertaAbierta;

            AnimacionesControlador.SetBool(animPalanca, "estaActivada", palancaActivada);
            AnimacionesControlador.SetBool(animPuerta, "estaAbierta", puertaAbierta);

            palancaUsada = true;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        estaJugador = true;

        if (tipo == TipoPalanca.Presion)
        {
            puertaAbierta = true;
            AnimacionesControlador.SetBool(animPuerta, "estaAbierta", true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        estaJugador = false;

        if (tipo == TipoPalanca.Presion)
        {
            puertaAbierta = false;
            AnimacionesControlador.SetBool(animPuerta, "estaAbierta", false);
        }
    }

    public bool EstaAbierta() => puertaAbierta;

    public void Abrir()
    {
        puertaAbierta = true;
        AnimacionesControlador.SetBool(animPuerta, "estaAbierta", true);
    }

    private void TimerPalanca()
    {
        if (!palancaUsada)
        {
            timer += Time.deltaTime;
        }
        else if (!timerMostrado)
        {
            Debug.Log("Tiempo hasta activar palanca: " + timer.ToString("F2") + " segundos.");
            timerMostrado = true;
        }
    }

}
