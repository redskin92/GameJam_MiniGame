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
			
			switch(currentWeapon.fireType)
			{
				case WeaponInfo.FireType.SINGLE:
					GameObject newBullet = GameObject.Instantiate(currentWeapon.bulletPrefab, this.transform.position, playerFace);
					newBullet.GetComponent<Bullet>().SetUpBullet();
					break;
				case WeaponInfo.FireType.SPLIT_5:
					float spread = 60f;
					float spreadInc = spread / 5;

					Vector3 bulletDir = playerFace.eulerAngles;
					bulletDir.z += (spread * 0.5f);
					for(int i = 0; i < 5; ++i)
					{
						GameObject spread5Bullet = GameObject.Instantiate(currentWeapon.bulletPrefab, this.transform.position, Quaternion.Euler(bulletDir));
						spread5Bullet.GetComponent<Bullet>().SetUpBullet();

						bulletDir.z -= spreadInc;
					}
					break;
			}

            currentAmmo--;            
        }


        public bool CheckAmmo()
        {
            return currentAmmo > 0;
        }
    }
}