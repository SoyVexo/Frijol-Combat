using UnityEngine;
using UnityEngine.EventSystems;

public class frijol : MonoBehaviour, IPointerDownHandler
{

    private Vector2 lastFrameVelocity;
    Rigidbody2D rb;

    public float velocity = 5f;

    public GameObject explosionPrefab;

    public CameraShake camara;

    public timer timer;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(1, 1).normalized * velocity;
    }

    void Update()
    {
        lastFrameVelocity = rb.linearVelocity;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 collisionNormal = collision.contacts[0].normal;

        Vector2 direction = Vector2.Reflect(lastFrameVelocity.normalized, collisionNormal);

        rb.linearVelocity = direction * velocity;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Camera cam = Camera.main;

        // 1. Obtenemos los límites de la pantalla en coordenadas del mundo
        // (0,0) es abajo-izquierda, (1,1) es arriba-derecha
        Vector3 esquinaInferior = cam.ViewportToWorldPoint(new Vector3(0.1f, 0.1f, 0)); 
        Vector3 esquinaSuperior = cam.ViewportToWorldPoint(new Vector3(0.9f, 0.9f, 0));

        // 2. Generamos un valor aleatorio entre esos límites
        float randomX = Random.Range(esquinaInferior.x, esquinaSuperior.x);
        float randomY = Random.Range(esquinaInferior.y, esquinaSuperior.y);

        Instantiate(gameObject, new Vector3(randomX, randomY, 0), transform.rotation);

        
        camara.Sacudir(0.5f, 0.1f);
        explosion();
        Destroy(gameObject);

        timer.segundos = (int)Mathf.Min(timer.segundos + 1, timer.segundosIniciales);

        Debug.Log("Segundos restantes: " + timer.segundos);
    }

    //logica de particulas de explocion

    void explosion()
    {
        GameObject explosion = Instantiate(explosionPrefab, transform.position, explosionPrefab.transform.rotation);
        Destroy(explosion, 1f);
    }
    
}
