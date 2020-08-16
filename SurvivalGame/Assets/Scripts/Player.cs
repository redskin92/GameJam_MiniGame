using UnityEngine;
using Player.Weapons;

namespace Player
{
    public class Player : MonoBehaviour
    {
        Vector3 lookToVector = new Vector3();

        [SerializeField]
        private PlayerWeapon playerWeapon;

        [SerializeField]
        private WeaponInfo defaultWeaponInfo;

        // Start is called before the first frame update
        void Start()
        {
            // TODO register for weapon event

            playerWeapon.UpdateWeapon(defaultWeaponInfo);
        }

        // Update is called once per frame
        void Update()
        {
            LookToMouse();

            CheckInput();
        }

        private void CheckInput()
        {
            if(Input.GetMouseButton(0))
            {
                if(playerWeapon.CanFire())
                {
                    playerWeapon.Fire(this.transform.rotation);                    
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

        
    }
}