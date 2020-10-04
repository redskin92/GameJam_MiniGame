using UnityEngine;
using Player.Weapons;
using Inventory;
using System;

namespace Player
{
    public class Player : MonoBehaviour
    {
        Vector3 lookToVector = new Vector3();

        [SerializeField]
        private PlayerWeapon playerWeapon;

        [SerializeField]
        private WeaponInfo defaultWeaponInfo;

        private bool isBaseWeapon = true;

        private PlayerInventory playerIneventory;

        // Start is called before the first frame update
        void Start()
        {
            UpdatePlayersWeapon(defaultWeaponInfo, true);

            //playerIneventory = GameObject.FindGameObjectWithTag("PlayerInventory").GetComponent<PlayerInventory>();
            //playerIneventory.OnWeaponEquipped += WeaponUpdated;
        }

        // Update is called once per frame
        void Update()
        {
            LookToMouse();

            CheckInput();
        }

        private void WeaponUpdated(object sender, EventArgs args)
        {
            WeaponInfo weaponInfo = (WeaponInfo)sender;

            UpdatePlayersWeapon(weaponInfo, false);
        }

        private void CheckInput()
        {
            if (Input.GetMouseButton(0))
            {
                if (playerWeapon.CanFire())
                {
                    playerWeapon.Fire(this.transform.rotation);

                    // Check if weapon is expired
                    if (!isBaseWeapon && !playerWeapon.CheckAmmo())
                    {
                        UpdatePlayersWeapon(defaultWeaponInfo, true);                        
                    }
                }
            }
        }

        private void LookToMouse()
        {
            Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 dir = Input.mousePosition - objectPos;

            lookToVector = Vector3.zero;
            lookToVector.z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(lookToVector);
        }

        private void UpdatePlayersWeapon(WeaponInfo weaponInfo, bool baseWeapon)
        {
            playerWeapon.UpdateWeapon(defaultWeaponInfo);
            isBaseWeapon = baseWeapon;
        }
    }
}