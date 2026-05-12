using UnityEngine;
using UnityEngine.EventSystems;


public class SkinSelector : MonoBehaviour
{
    public GameObject playerPrefab;

    public GameObject DefaultCanvas;

    public GameObject SkinsCanvas;

    public void Boton()
    {
        string nombre = EventSystem.current.currentSelectedGameObject.name;

        if (nombre == "red")
        {
            Global.color_player = Color.red;
        }
        else if (nombre == "blue")
        {
            Global.color_player = Color.blue;
        }
        else if (nombre == "green")
        {
            Global.color_player = Color.green;
        }
        else if (nombre == "yellow")
        {
            Global.color_player = Color.yellow;
        }
        else if (nombre == "black")
        {
            Global.color_player = Color.black;
        }
        else if (nombre == "white")
        {
            Global.color_player = Color.white;
        }
        else if (nombre == "skins") {
            DefaultCanvas.gameObject.SetActive(false);
            SkinsCanvas.gameObject.SetActive(true);
        }
        else if (nombre == "back") {
            DefaultCanvas.gameObject.SetActive(true);
            SkinsCanvas.gameObject.SetActive(false);
        }
    }
}
