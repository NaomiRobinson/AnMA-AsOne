using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public void PlayGame()
    {

        SceneManager.LoadScene("Nivel1");
    }

    public void ShowHelp()
    {

        SceneManager.LoadScene("Ayuda");
    }


    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");

        // Reiniciar contadores de muerte y tiempo
        if (GameStats.Instance != null)
        {
            GameStats.Instance.tiempoTotal = 0f;
            GameStats.Instance.muertesTotales = 0;
        }

        // También podés reiniciar los contadores de StaticVariables si querés
        StaticVariables.SessionData.death = 0;
        StaticVariables.SessionData.time = 0;
        StaticVariables.SessionData.level = 1;
    }



}
