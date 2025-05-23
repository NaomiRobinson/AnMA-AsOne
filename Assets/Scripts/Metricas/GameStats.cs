using UnityEngine;

public class GameStats : MonoBehaviour
{
    public static GameStats Instance;

    public float tiempoTotal = 0f;
    public int muertesTotales = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SumarTiempo(float tiempo)
    {
        tiempoTotal += tiempo;
    }

    public void SumarMuerte()
    {
        muertesTotales++;
    }
}
