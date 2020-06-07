using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoManager : MonoBehaviour
{
    [SerializeField]
    private List<DisplayPlayerInfo> players;

    public List<PlayerInfo> PlayerInfo
    {
        get; set;
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerInfo = new List<PlayerInfo>();
        if (GameFlow.Instance != null )
        {
            for (int i = 0; i < 2; ++i)
            {
                PlayerInfo.Add(GameFlow.Instance.GetPlayerInfo(i));

                if (players.Count > i)
                {
                    players[i].SetPlayerInfo(i, GameFlow.Instance.GetPlayerSprite(i), PlayerInfo[i].name);
                }
            }
        }
        else
        {
            for (int i = 0; i < 2; ++i)
            {
                PlayerInfo.Add(new PlayerInfo("Player: " + i, 0));

                if (players.Count > i)
                {
                    players[i].SetPlayerInfo(i, null, PlayerInfo[i].name);
                }
            }
        }
    }
}
