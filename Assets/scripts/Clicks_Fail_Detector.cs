using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Clicks_Fail_Detector : MonoBehaviour , IPointerDownHandler
{
    public TextMeshProUGUI Text;
    public CameraShake shake;

    public float force = 0.5f;
    public void OnPointerDown(PointerEventData eventData)
    {
        Global.Clicks_Fails++;
        Debug.Log("Clicks_Fails: " + Global.Clicks_Fails);
        shake.Sacudir(0.5f, force);
    }
    public void Update()
    {
        Text.text = "Clicks fallidos: " + Global.Clicks_Fails.ToString();
    }
}
