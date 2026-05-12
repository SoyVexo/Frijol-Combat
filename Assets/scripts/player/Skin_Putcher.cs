using UnityEngine;

public class Skin_Putcher : MonoBehaviour
{
    void FixedUpdate()
    {
        if (Global.ColorDefault == 0)
            {GetComponent<SpriteRenderer>().color = Global.color_player; Global.ColorDefault++;}
        
        GetComponent<SpriteRenderer>().color = Global.color_player;

    }
}
