using UnityEngine;
using UnityEngine.SceneManagement;
using static StaticVariables;

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

        Debug.Log($"Se murió. Lo mató {name} del grupo {type}.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
