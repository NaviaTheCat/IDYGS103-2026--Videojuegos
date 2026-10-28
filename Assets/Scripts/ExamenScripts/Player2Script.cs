using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player2Script : MonoBehaviour
{
    public float velocidad = 5f;

    private Vector2 movimiento;

    public void OnMove(InputValue value)
    {
        Vector2 inputOriginal = value.Get<Vector2>();

        if(Math.Abs(inputOriginal.x) > Mathf.Abs(inputOriginal.y))
        {
            movimiento = new Vector2(inputOriginal.x, 0f);
        }
        else
        {
             movimiento = new Vector2(0f, inputOriginal.y);
        }
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

        if (collision.gameObject.CompareTag("PicoMuerte1"))
        {
            Debug.Log("¡Perdiste te atrapo el player 1! Reiniciando...");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
    }


    void ReiniciarNivel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}