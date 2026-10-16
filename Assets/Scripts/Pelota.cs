using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement; // <-- 1. AGREGAR ESTA LÍNEA ARRIBA
using UnityEngine.SceneManagement; // Necesario para reiniciar/cargar escenas

public class Pelota : MonoBehaviour
{

    [Header("UI / Pantalla")]
    public TextMeshProUGUI textoPantalla;
    [Header("Fuerzas de Movimiento")]
    public float fuerzaResistenciaDerecha = 4f; // La fuerza constante que empuja a la derecha
    public float fuerzaJugador = 10f;             // Fuerza que aplicas al presionar teclas
    public float fuerzaSaltoImpulso = 8f;         // Impulso vertical con Espacio

    private Rigidbody2D rb;
    private float movimientoH;
    private bool juegoTerminado = false; // Evita que se siga moviendo al ganar o perder
    

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        // Aseguramos que el texto esté vacío al iniciar la partida
        if (textoPantalla != null)
        {
            textoPantalla.text = "";
        }
    }

    void Update()
    {
        if (juegoTerminado) return; // Si ya ganó o perdió, no lee controles

        // Leemos la dirección horizontal (-1 izquierda, 0 sin presionar, 1 derecha)
        movimientoH = Input.GetAxisRaw("Horizontal");

        // Impulso hacia arriba al presionar Espacio
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * fuerzaSaltoImpulso, ForceMode2D.Impulse);
        }
    }

    void FixedUpdate()
    {
        // 1. Calculamos la fuerza horizontal total:
        //    Inicia con la fuerza constante que siempre empuja a la derecha (+)
        float velocidadX = fuerzaResistenciaDerecha;

        // 2. Le sumamos el input del jugador.
        //    Si presionas Izquierda (-1), le restará velocidad al empuje de la derecha.
        //    Como fuerzaJugador (10) es mayor que la resistencia (4), lograrás ir a la izquierda (-6 total).
        velocidadX += movimientoH * fuerzaJugador;

        // 3. Aplicamos la velocidad horizontal respetando la velocidad vertical (rebote)
        rb.linearVelocity = new Vector2(velocidadX, rb.linearVelocity.y);
    }

   // <-- 2. AGREGAR EL MÉTODO AQUÍ ABAJO (Dentro de la clase)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 1. Choque con Obstáculo Negro (Derrota)
        if (collision.gameObject.CompareTag("Muerte"))
        {
            Debug.Log("¡Perdiste! Reiniciando...");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            if (textoPantalla != null)
                textoPantalla.text = "¡PERDISTE!";
        }
        // 2. Choque con Objeto Verde (Victoria)
        else if (collision.gameObject.CompareTag("Victoria"))
        {
            Debug.Log("¡GANASTE EL JUEGO! ¡Felicidades!");
            juegoTerminado = true;
            rb.linearVelocity = Vector2.zero; // Detiene la pelota
            if (textoPantalla != null)
                textoPantalla.text = "¡GANASTE!";

            // Opcional: Reinicia el nivel tras 3 segundos para volver a jugar
            Invoke("ReiniciarNivel", 3f);
        }
    }

    

    void ReiniciarNivel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}