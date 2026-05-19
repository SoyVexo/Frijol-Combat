using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI puntosText;
    public timer timer; 

    private bool isGameOverProcessed = false;

    public bool UAreANoob = false;

    public frijol player;

    public GameObject enemyPrefab; 
    public GameObject[] spawnPoints;
    
    public TextMeshProUGUI NoobText;

    public GameObject panel;

    public GameObject StartFade;
    

    public void Start()
    {
        StartFade.SetActive(true);
        StartFade.GetComponent<Image>().DOFade(0, 1f).OnComplete(() => StartFade.SetActive(false));
        
        StartCoroutine(EnemySpawnCoroutine());

        LoadGame();
        
        Global.points = 0;
    }
    
    public void Update()
    {
        
        if (UAreANoob)
        {
            return;
        }
        
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
        if (UAreANoob)
        {
            AppQuit();
        }
    }

    public void GameOver()
    {
        if (UAreANoob || isGameOverProcessed)
        {
            return;
        }
        
        isGameOverProcessed = true;
        
        if (Global.points < 100)
        {
            Global.NoobDetect += 1;
            if (Global.NoobDetect >= 5)
            {
                NoobText.text = "Wow, u are really noob at this game, u have died 5 times with less than 100 points, u need a break";
                panel.SetActive(true);
                StartCoroutine(CountDown(5f));
            }
            else
            {
                StartFade.SetActive(true);
                StartFade.GetComponent<Image>().DOFade(1, 0.3f).OnComplete(() =>
                {
                    Global.NoobDetect = 0;
                    Global.CoinsAdd();
                    Debug.Log("Monedas ganadas: " + Global.Monedas_Ganadas.ToString());
                    Debug.Log("Total de monedas: " + Global.Coins.ToString());
                
                    SceneManager.LoadScene("GameOver");
                });
                
            }
        }
        else
        {
            StartFade.GetComponent<Image>().DOFade(1, 0.3f).OnComplete(() =>
                {
                    Global.NoobDetect = 0;
                    Global.CoinsAdd();
                    Debug.Log("Monedas ganadas: " + Global.Monedas_Ganadas.ToString());
                    Debug.Log("Total de monedas: " + Global.Coins.ToString());
                
                    SceneManager.LoadScene("GameOver");
                });
        }
        
        
        
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
            Global.color_player = data.colorSkin;
        }
    }

    void SpawnEnemy()
    {
        
        int spawnPoint = Random.Range(0, spawnPoints.Length);
        Instantiate(enemyPrefab, spawnPoints[spawnPoint].transform.position, Quaternion.identity);

    }
    IEnumerator EnemySpawnCoroutine()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(5f); 
        }
    }
    IEnumerator CountDown(float seconds)
{
    float remainingTime = seconds;

    while (remainingTime > 0)
    {
        // Muestra el tiempo actual en la consola (opcional)
        Debug.Log("Tiempo restante: " + remainingTime);

        // Espera 1 segundo de tiempo real, ignorando el timeScale
        yield return new WaitForSecondsRealtime(1f);

        // Resta un segundo al contador
        remainingTime--;
    }

    Debug.Log("¡Tiempo agotado!");
    
    UAreANoob = true;
}

    void AppQuit()
    {
        Application.Quit();
        Debug.LogError("Quitting the game");
    }

}
