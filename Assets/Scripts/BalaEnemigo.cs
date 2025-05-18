using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BalaEnemigo : MonoBehaviour
{
    public float velocidad;
    public int daño;

    void Start()
    {

    }


    private void Update()
    {
        transform.Translate(Time.deltaTime * velocidad * Vector2.right);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            ReiniciarNivel reinicio = collision.gameObject.GetComponent<ReiniciarNivel>();
            if (reinicio != null)
            {
                reinicio.Reiniciar(gameObject); 
            }
            else
            {
                Debug.LogWarning("El jugador no tiene el componente ReiniciarNivel.");
            }
        }


        if (collision.gameObject.CompareTag("Walls"))
        {
            Debug.Log("bala toco pared");
            Destroy(gameObject);
        }


    }
}
