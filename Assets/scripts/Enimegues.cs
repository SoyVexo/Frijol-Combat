using UnityEngine;
using UnityEngine.EventSystems;

public class Enimegues : MonoBehaviour, IPointerDownHandler
{
    public Camera camaraPrincipal;
    public Rigidbody2D rbObjeto;
    public float fuerza = 500f;

    public GameManager gamemanager;

    // 1. Solo declaramos la variable aquí arriba (sin darle valor todavía)
    private Vector3 centroMundo;
    
    void Start()
    {
        // 2. Si no arrastraste la cámara en el inspector, la buscamos automáticamente para evitar errores
        if (camaraPrincipal == null)
        {
            camaraPrincipal = Camera.main;
        }

        // 3. Calculamos el centro del mundo de forma segura dentro del Start()
        centroMundo = camaraPrincipal.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        centroMundo.z = 0f;

        // Calcular la dirección 2D desde el objeto hacia el centro del mundo
        Vector2 direccion = ((Vector2)centroMundo - rbObjeto.position).normalized;

        // Aplicar la fuerza al Rigidbody2D
        rbObjeto.AddForce(direccion * fuerza);
    }

    // Este método se ejecutará gracias al Physics 2D Raycaster de la cámara principal
    public void OnPointerDown(PointerEventData eventData)
    {
        if (gamemanager != null)
        {
            gamemanager.GameOver();
        }
    }

    void Update()
    {
        // Comprobamos la distancia real en metros/unidades. 
        // Nota: He cambiado 150f por 15f. Si se aleja mucho de la pantalla, se destruye.
        if (Vector2.Distance(transform.position, centroMundo) > 15f)
        {
            Destroy(gameObject);
        }
    }
}
