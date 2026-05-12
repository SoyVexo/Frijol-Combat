using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public static class Global
{
    public static int segundos;
    public static float velocity;
    public static int max_velocidad = 30;

    public static int veces;

    public static int points;

    public static int record;

    public static int Coins;

    public static int Clicks_Fails;

    public static int Segundos_vividos;

    private static float counter;

    public static Color color_player = Color.white;

    public static float Monedas_Ganadas;

    public static void Restart()
    {
        velocity = 0;
        veces = 0;
        points = 0;
        record = 0;
        Clicks_Fails = 0;
        Monedas_Ganadas = 0;
        Segundos_vividos = 0;
        counter = 0f;
    }

    public static void CoinsAdd()
    {
        Monedas_Ganadas = Mathf.Floor((points * Segundos_vividos) / (Clicks_Fails == 0 ? 1 : Clicks_Fails));
        Coins += (int)Monedas_Ganadas;
    }

    // se deve poner en el update del GameManager para que funciona el contador de segundos
    public static void Update()
    {
        counter += Time.deltaTime;
        SecondsCounter();
    }
    private static void SecondsCounter()
    {
        if (counter >= 1f)
        {
            Segundos_vividos++;
            counter = 0f;
            Debug.Log("Segundos_vividos: " + Segundos_vividos);
        }
    }

    public static int ColorDefault = 0;
}
