using TMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.Microsoft.GDK;
using System.Collections;
using JetBrains.Annotations;

public class MenuGameOverManager : MonoBehaviour
{
    [Header("UI Text Components")]
    public TextMeshProUGUI puntosText;
    public TextMeshProUGUI recordText;
    public TextMeshProUGUI CoinsText;

    [Header("Advertising Components")]
    public InterstitialAdExample skipAd;
    public RewardedAdsButton rewardedAd;
    public Button RewardedAdButton;

    public GameObject ExplocionPrefab;
    public GameObject PlayerImage;
    public Transform SpawnExplosionPosition;
    public GameObject TextPlay;
    public float TextPlayRotationSpeed = 50f;

    [Header("Transition Components")]
    public GameObject panel;

    void Update()
    {
        TextPlay.transform.localEulerAngles += new Vector3(0f, 0f, TextPlayRotationSpeed * Time.deltaTime);
    }

    public void Start()
    {
        // 1. Aseguramos que el panel empiece completamente opaco (A = 1)
        panel.SetActive(true);
        Image panelImage = panel.GetComponent<Image>();
        panelImage.color = new Color(panelImage.color.r, panelImage.color.g, panelImage.color.b, 1f);

        // 2. Efecto Fade In (se aclara la pantalla). Al terminar, apaga el panel para poder clicar botones
        panelImage.DOFade(0f, 0.5f).OnComplete(() => 
        {
            panel.SetActive(false);
        });
        
        // 3. Guardar partida y cargar anuncios intermedios
        SaveGame();
        
        if (Global.AdsOn)
        {
            skipAd.LoadAd();
            skipAd.ShowAd();
        }
        
        // 4. Actualizar textos de la interfaz
        puntosText.text = "Puntos: " + Global.points.ToString();
        recordText.text = "Record: " + Global.record.ToString();
        CoinsText.text = "Coins: " + Global.Coins.ToString();
    }

    public void button()
    {
        SaveGame();
        StartCoroutine(Retry());
    }

    public void RewardedAd()
    {   
        if (Global.AdsOn)
        {
            rewardedAd.LoadAd();
            Global.Coins += 1000;
            CoinsText.text = "Coins: " + Global.Coins.ToString();
            rewardedAd.ShowAd();
            RewardedAdButton.interactable = false;
        }
    }

    public void SaveGame()
    {
        // Usamos FindFirstObjectByType (estándar moderno de Unity que reemplaza a FindObjectOfType)
        SaveManager saveData = Object.FindFirstObjectByType<SaveManager>();
        
        if (saveData != null) 
        {
            SaveData data = new SaveData();
            data.points = Global.points;
            data.record = Global.record;
            data.coins = Global.Coins;
            data.colorSkin = Global.color_player;
            
            saveData.SaveGame(data);
        }
        else 
        {
            Debug.LogError("No se encontró el SaveManager en la escena.");
        }
    }
    IEnumerator Retry()
    {
        
        Instantiate(ExplocionPrefab, SpawnExplosionPosition.position, ExplocionPrefab.transform.rotation);
        PlayerImage.SetActive(false);
        TextPlay.SetActive(false);
        
        yield return new WaitForSeconds(0.5f);
        
        
        panel.SetActive(true);
        Image panelImage = panel.GetComponent<Image>();

        // Hace Fade hacia 1 (opaco). Al terminar (0.5s), reinicia la lógica y carga la escena
        panelImage.DOFade(1f, 0.5f).OnComplete(() => 
        {
            Global.Restart();
            SceneManager.LoadScene("Game");
        });
    }
}
