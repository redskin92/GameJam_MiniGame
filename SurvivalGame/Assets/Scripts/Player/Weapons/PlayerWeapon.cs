using UnityEngine;

namespace Player.Weapons
{
    public class PlayerWeapon : MonoBehaviour
    {
        private WeaponInfo currentWeapon;
        private float fireRateCooldownTimer = 0.0f;
        private int currentAmmo;

        SpriteRenderer spriteRenderer;
        bool isBase = true;

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
            currentAmmo = currentWeapon.ammo;

            spriteRenderer.sprite = newWeapon.weaponSprite;
        }

        public bool CanFire()
        {
            return fireRateCooldownTimer <= 0.0f;
        }

        public void Fire(Quaternion playerFace)
        {
            fireRateCooldownTimer = currentWeapon.fireRate;
            GameObject newBullet = GameObject.Instantiate(currentWeapon.bulletPrefab, this.transform.position, playerFace);
            newBullet.GetComponent<Bullet>().SetUpBullet();
            currentAmmo--;            
        }

        public bool CheckAmmo()
        {
            return currentAmmo > 0;
        }
    }
}