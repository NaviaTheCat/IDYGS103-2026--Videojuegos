using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player1 : MonoBehaviour
{
    public float velocidad = 5f;

    private Vector2 movimiento;

    public void OnMove(InputValue value)
    {
        movimiento = value.Get<Vector2>();
    }

    void Update()
    {
        Vector3 direccion = new Vector3(
            movimiento.x,
            movimiento.y,
            0f
        );
        transform.Translate(
            direccion * velocidad * Time.deltaTime
        );
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("PicoMuerte2"))
        {
            Debug.Log("¡Perdiste te atrapo player 2! Reiniciando...");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
    }


    void ReiniciarNivel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}