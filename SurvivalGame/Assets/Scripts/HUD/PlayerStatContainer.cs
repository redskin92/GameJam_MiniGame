using UnityEngine;

namespace HUD
{
    // TODO: Replace with actual implementation.
    public struct PlayerData
    {
        public float health;
        public float maxHealth;
    }
    
    public abstract class PlayerStatContainer : MonoBehaviour
    {
        public abstract void DataUpdated(PlayerData data);
    }
}