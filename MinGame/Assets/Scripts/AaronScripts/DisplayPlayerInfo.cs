using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayPlayerInfo : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer spriteUsed;

    public int PlayerIndex { get; set; }

    public string Name { get; set; }

    public void SetPlayerInfo(int playerIndex, Sprite spriteSelected, string playerName)
    {
        PlayerIndex = playerIndex;

        if(spriteUsed != null && spriteSelected != null)
        {
            spriteUsed.sprite = spriteSelected;
        }

        if (playerName != null)
        {
            Name = playerName;
        }
    }
}
