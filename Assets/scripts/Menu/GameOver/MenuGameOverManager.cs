using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGameOverManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TextMeshProUGUI puntosText;
    
    
    public void button()
    {
        Global.Restart();
        SceneManager.LoadScene("Game");

        
    }
    
    public void Start()
    {
        puntosText.text = "Puntos: " + Global.points.ToString();
    }
}
