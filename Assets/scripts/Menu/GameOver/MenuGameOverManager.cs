using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuGameOverManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TextMeshProUGUI puntosText;
    public TextMeshProUGUI recordText;

    public TextMeshProUGUI CoinsText;

    public InterstitialAdExample skipAd;
    
    
    public void button()
    {
        Global.Restart();
        SceneManager.LoadScene("Game");

        
    }

    public RewardedAdsButton rewardedAd;

    public Button RewardedAdButton;
    
    public void RewardedAd()
    {
        rewardedAd.LoadAd();
        Global.Coins += 1000;
        CoinsText.text = "Coins: " + Global.Coins.ToString();
        rewardedAd.ShowAd();
        RewardedAdButton.interactable = false;
    }
    
    
    public void Start()
    {
        SaveGame();
        
        skipAd.LoadAd();
        skipAd.ShowAd();
        
        puntosText.text = "Puntos: " + Global.points.ToString();
        recordText.text = "Record: " + Global.record.ToString();
        CoinsText.text = "Coins: " + Global.Coins.ToString();
    }

    public void SaveGame()
{
    // Cambiamos FindObjectOfType por FindFirstObjectByType
    SaveManager saveData = Object.FindFirstObjectByType<SaveManager>();
    
    if (saveData != null) 
    {
        SaveData data = new SaveData();
        data.points = Global.points;
        data.record = Global.record;
        data.coins = Global.Coins;
        
        saveData.SaveGame(data);
    }
    else 
    {
        Debug.LogError("No se encontró el SaveManager en la escena.");
    }
}
}
