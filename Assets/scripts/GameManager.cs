using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI puntosText;
    public timer timer;

    public void Start()
    {
        Global.points = 0;
    }
    
    public void Update()
    {
        puntosText.text = "Puntos: " + Global.points.ToString();
    }
    void FixedUpdate()
    {
        if (timer.segundos <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

}
