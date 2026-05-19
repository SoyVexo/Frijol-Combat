using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;

public class frijol : MonoBehaviour, IPointerDownHandler
{

    private Vector2 lastFrameVelocity;
    Rigidbody2D rb;

    public float velocity = 5f;

    public float real_velocity;

    public GameObject explosionPrefab;

    public CameraShake camara;

    public timer timer;
    public TextMeshProUGUI TextPuntos;

    private int spawn_count = 0;
    
    
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (Global.veces == 0) {Global.velocity = velocity; Global.veces++;}

        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);


        if (Mathf.Abs(x) < 0.2f) x = x < 0 ? -0.5f : 0.5f;
        if (Mathf.Abs(y) < 0.2f) y = y < 0 ? -0.5f : 0.5f;

        Vector2 dirInicial = new Vector2(x, y).normalized;

        rb.linearVelocity = dirInicial * Global.velocity;
        
    }

    void Update()
    {
        lastFrameVelocity = rb.linearVelocity;
        real_velocity = Global.velocity;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 collisionNormal = collision.contacts[0].normal;

        Vector2 direction = Vector2.Reflect(lastFrameVelocity.normalized, collisionNormal);

        rb.linearVelocity = direction * Global.velocity;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (spawn_count >= 1)
        {
            Debug.LogWarning("Se ha intentado aparecer mas de un frijol, autoarreglándose");
            return;
        }

        spawn_count++;
        
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

        Global.velocity += 0.5f;

        if (Global.velocity > Global.max_velocidad) {Global.velocity = Global.max_velocidad;}

        Global.points += 1 * (int)Global.velocity;
        TextPuntos.color = Color.green;
        TextPuntos.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.15f).OnComplete(() => {TextPuntos.transform.DOScale(new Vector3(1, 1, 1), 0.15f); TextPuntos.color = Color.white;});

        Destroy(gameObject, 0.02f); 

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
