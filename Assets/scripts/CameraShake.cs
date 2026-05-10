using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    private Vector3 posicionOriginal;

    void Start()
    {
        posicionOriginal = transform.localPosition;
    }

    public void Sacudir(float duracion, float fuerza)
    {
        StopAllCoroutines(); // Por si ya se estaba sacudiendo
        StartCoroutine(Shake(duracion, fuerza));
    }

    IEnumerator Shake(float duracion, float fuerza)
    {
        float tiempoTranscurrido = 0f;

        while (tiempoTranscurrido < duracion)
        {
            // Generamos un movimiento aleatorio
            float x = Random.Range(-1f, 1f) * fuerza;
            float y = Random.Range(-1f, 1f) * fuerza;

            transform.localPosition = new Vector3(x, y, posicionOriginal.z);

            tiempoTranscurrido += Time.deltaTime;
            yield return null; // Espera al siguiente frame
        }

        // Al terminar, volvemos a la posición normal
        transform.localPosition = posicionOriginal;
    }
}