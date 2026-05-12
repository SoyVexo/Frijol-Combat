using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    private string SavePath;

    void Awake() 
    {
        // Usamos Awake para que la ruta esté lista de inmediato
        SavePath = Path.Combine(Application.persistentDataPath, "savefile.json");
    }

    public void SaveGame(SaveData dataToSave)
    {
        // Convertimos directamente el objeto que recibimos
        string json = JsonUtility.ToJson(dataToSave, true);
        File.WriteAllText(SavePath, json);
        Debug.Log("Archivo guardado físicamente en: " + SavePath);
    }

    public SaveData LoadGame()
    {
        if (File.Exists(SavePath))
        {
            string json = File.ReadAllText(SavePath);
            return JsonUtility.FromJson<SaveData>(json);
        }
        return null;
    }
}