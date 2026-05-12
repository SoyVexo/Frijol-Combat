using NUnit.Framework;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI puntosText;
    public timer timer; 

    public frijol player;

    public void Start()
    {
        LoadGame();
        
        Global.points = 0;
    }
    
    public void Update()
    {
        Global.Update();
        
        puntosText.text = "Puntos: " + Global.points.ToString();

        if (Global.points > Global.record)
        {
            Global.record = Global.points;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Global.Coins = 0;
            Debug.LogWarning("Monedas reseteadas a 0");
        }
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
        Global.CoinsAdd();
        Debug.Log("Monedas ganadas: " + Global.Monedas_Ganadas.ToString());
        Debug.Log("Total de monedas: " + Global.Coins.ToString());
        
        SceneManager.LoadScene("GameOver");
    }

    void LoadGame()
    {
        SaveManager saveData = FindObjectOfType<SaveManager>();
        SaveData data = saveData.LoadGame();
        if (data != null)
        {
            Global.points = data.points;
            Global.record = data.record;
            Global.Coins = data.coins;
        }
    }

}
