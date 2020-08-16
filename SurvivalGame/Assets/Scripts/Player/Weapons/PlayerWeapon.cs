using UnityEngine;

namespace Player.Weapons
{
    public class PlayerWeapon : MonoBehaviour
    {
        private WeaponInfo currentWeapon;
        private float fireRateCooldownTimer = 0.0f;

        SpriteRenderer spriteRenderer;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (fireRateCooldownTimer > 0.0f)
                fireRateCooldownTimer -= Time.deltaTime;
        }

        public void UpdateWeapon(WeaponInfo newWeapon)
        {
            fireRateCooldownTimer = -1.0f;
            currentWeapon = newWeapon;

            spriteRenderer.sprite = newWeapon.weaponSprite;
        }

        public bool Fire()
        {
            if(fireRateCooldownTimer > 0f)
            {
                
                return false;
            }

            //Fire
            return true;
        }
    }
}