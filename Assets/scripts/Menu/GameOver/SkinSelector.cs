using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;


public class SkinSelector : MonoBehaviour
{
    public GameObject playerPrefab;

    public GameObject DefaultCanvas;

    public GameObject SkinsCanvas;

    public GameObject frijol;

    public void Boton()
    {
        string nombre = EventSystem.current.currentSelectedGameObject.name;

        if (nombre == "red")
        {
            Global.color_player = Color.red;
            AnimSkin();
        }
        else if (nombre == "blue")
        {
            Global.color_player = Color.blue;
            AnimSkin();
        }
        else if (nombre == "green")
        {
            Global.color_player = Color.green;
            AnimSkin();
        }
        else if (nombre == "yellow")
        {
            Global.color_player = Color.yellow;
            AnimSkin();
        }
        else if (nombre == "black")
        {
            Global.color_player = new Color32(130, 41, 129, 255);
            AnimSkin();
        }
        else if (nombre == "white")
        {
            Global.color_player = Color.white;
            AnimSkin();
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
    void AnimSkin()
    {
        frijol.transform.DOScale(new Vector3(1,1.29f,1), 0.2f).OnComplete(() => {frijol.transform.DOScale(new Vector3(1,1,1), 0.2f);});
    }
}
