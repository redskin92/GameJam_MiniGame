using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField]
    ScoreCalculator scoreCalculator;

    [SerializeField]
    DisplayPlayerInfo playerInfo;

    // called when the player hits a spike
    void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.name.Contains("Spike"))
        {
            if (playerInfo != null && scoreCalculator != null)
            {
                scoreCalculator.PlayerDie(playerInfo.PlayerIndex);
            }
    
            Destroy(col.gameObject);
            gameObject.SetActive(false);
        }
    }
}
