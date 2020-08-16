namespace Player.Weapons
{
    public struct WeaponInfo
    {
        public enum FireType { SINGLE, SPLIT_3, SPLIT_5 };

        public FireType fireType;
        public float fireRate;
        public float damage;
    }
}
