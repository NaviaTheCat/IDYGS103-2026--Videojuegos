using UnityEngine;

public class PlataformaMovil : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    public float velocidad = 3f;
    public float distancia = 4f; // Cuánto se desplaza desde su inicio

    private Vector3 posicionInicial;

    void Start()
    {
        posicionInicial = transform.position;
    }

    void Update()
    {
        // Calculamos el desplazamiento oscilante de izquierda a derecha usando Transform
        float desplazamiento = Mathf.PingPong(Time.time * velocidad, distancia);
        
        // Actualizamos la posición en el eje X
        transform.position = posicionInicial + new Vector3(desplazamiento, 0f, 0f);
    }
}