using UnityEngine;

namespace HUD
{
    public abstract class PlayerStatContainer : MonoBehaviour
    {
        public abstract void DataUpdated(Player.Player.PlayerEventParams data);
    }
}