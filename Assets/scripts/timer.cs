using UnityEngine;
using UnityEngine.UI; // Para la barra

public class timer : MonoBehaviour
{
    public Image imagenBarra;
    public float contador = 0f;
    public int segundos = 10;
    
    public float segundosIniciales; // Para saber el 100%

    void Start()
    {
        // Guardamos el total al principio (ej: 10)
        segundosIniciales = segundos;
    }

    void Update()
    {

        if (segundos > 0)
        {
            contador += Time.deltaTime;
            segundos_count();

            // ACTUALIZACIÓN DE LA BARRA
            // Usamos una pequeña resta para que la barra baje suave 
            // entre segundo y segundo.
            float tiempoExacto = segundos - contador;
            imagenBarra.fillAmount = tiempoExacto / segundosIniciales;
        }
    }

    void segundos_count()
    {
        if (contador >= 1) // El >= es más seguro
        {
            segundos -= 1;
            contador = 0f;            
        }
    }
}