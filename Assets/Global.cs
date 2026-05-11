using UnityEngine;

public static class Global
{
    public static int segundos;
    public static float velocity;
    public static int max_velocidad = 30;

    public static int veces;

    public static int points;

    public static void Restart()
    {
        velocity = 0;
        veces = 0;
        points = 0;
    }
}
