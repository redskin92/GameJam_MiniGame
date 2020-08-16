using System.Collections;
using System.Collections.Generic;
using HUD;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    #region Fields

    public static HUDManager Instance { get; private set; }

    public List<PlayerStatContainer> statContainers;

    // TODO: Remove after player stats have been implemented.
    [SerializeField]
    public float playerHealth = 100.0f;

    [SerializeField]
    public float playerMaxHealth = 100.0f;

    #endregion

    #region Methods

    private void Awake()
    {
        Instance = this;
    }

    // TODO: Remove. This is only for testing.
    public void Update()
    {
        if (playerHealth > playerMaxHealth)
            playerHealth = playerMaxHealth;
        if (playerHealth < 0)
            playerHealth = 0;
        if (playerMaxHealth < 1)
            playerMaxHealth = 1;
        
        PlayerData d = new PlayerData();
        d.health = playerHealth;
        d.maxHealth = playerMaxHealth;
        Instance.UpdatePlayerData(d);
    }

    /// <summary>
    /// Event that should be called when the player's stats change.
    /// </summary>
    /// <param name="data"></param>
    public void UpdatePlayerData(PlayerData data)
    {
        if (statContainers == null)
            return;
        
        for (int i = 0; i < statContainers.Count; i++)
        {
            statContainers[i].DataUpdated(data);
        }
    }

    #endregion
}
