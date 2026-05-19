using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{
    public Image imagenBarra;
    public float contador = 0f;
    public int segundos = 5;
    
    public float segundosIniciales;

    public Color colorLleno = Color.green;
    public Color colorVacio = Color.red;

    // NUEVO: Control de transparencia dinámica (Valores entre 0.0 y 1.0)
    [Range(0f, 1f)] public float alphaMinimo = 0.2f; // Casi invisible al principio (20%)
    [Range(0f, 1f)] public float alphaMaximo = 0.7f; // Más visible al final, pero deja ver el fondo (70%)

    void Start()
    {
        segundosIniciales = segundos;
    }

    void Update()
    {
        if (segundos > 0)
        {
            contador += Time.deltaTime;
            segundos_count();

            float tiempoExacto = segundos - contador;
            float porcentaje = tiempoExacto / segundosIniciales;

            imagenBarra.fillAmount = porcentaje;

            // 1. Calculamos el color base (la mezcla entre verde y rojo)
            Color colorFinal = Color.Lerp(colorVacio, colorLleno, porcentaje);

            // 2. NUEVO: Calculamos la transparencia dinámica.
            // Invertimos el porcentaje con (1 - porcentaje) para que el alpha
            // SUBA (se vuelva más visible) cuando el tiempo BAJA.
            float alphaDinamico = Mathf.Lerp(alphaMinimo, alphaMaximo, 1f - porcentaje);
            
            // 3. Aplicamos ese alpha al color antes de dárselo a la barra
            colorFinal.a = alphaDinamico;
            imagenBarra.color = colorFinal;
        }
        else
        {
            imagenBarra.fillAmount = 0;
            
            // Al final, se queda en el color de vacío con el alpha máximo permitido
            Color colorFinal = colorVacio;
            colorFinal.a = alphaMaximo;
            imagenBarra.color = colorFinal;
        }
    }

    void segundos_count()
    {
        if (contador >= 1)
        {
            segundos -= 1;
            contador = 0f;            
        }
    }
}