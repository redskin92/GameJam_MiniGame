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

		private float health;
		private float maxHealth = 100f;
		private bool isDead = false;

		private PlayerInventory playerIneventory;

		public WeaponBase testWeaponInfo;

		#region Player HP Events
		
		public delegate void PlayerHPChangedEventHandler(PlayerEventParams e);

		public static event PlayerHPChangedEventHandler PlayerHPChanged;

		public struct PlayerEventParams
		{
			public PlayerEventParams(float diff, float curr, float max)
			{
				hpDiff = diff;
				currentHP = curr;
				maxHP = max;
			}

			float hpDiff;
			float currentHP;
			float maxHP;
		}

		#endregion

		// Start is called before the first frame update
		void Start()
		{
			isDead = false;
			health = maxHealth;
			UpdatePlayersWeapon(defaultWeaponInfo, true);

			playerIneventory = GameObject.FindGameObjectWithTag("PlayerInventory").GetComponent<PlayerInventory>();	
			playerIneventory.OnWeaponEquipped += WeaponUpdated;			
		}

		private void OnDestroy()
		{
			if (playerIneventory != null)
				playerIneventory.OnWeaponEquipped -= WeaponUpdated;
		}

		// Update is called once per frame
		void Update()
		{
			if (isDead)
				return;

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

			if(Input.GetKey(KeyCode.T))
			{
				UpdatePlayersWeapon(testWeaponInfo.weaponInfo, false);
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
			playerWeapon.UpdateWeapon(weaponInfo);
			isBaseWeapon = baseWeapon;
		}

		#region Player Health 

		public float GetHealth()
		{
			return health;
		}

		public float GetMaxHealth()
		{
			return maxHealth;
		}

		/// <summary>
		/// Damages the player 
		/// </summary>
		/// <param name="amount"></param>
		public void Damage(float amount)
		{
			if (isDead)
				return;

			float healthBefore = health;
			health -= amount;

			if (health < 0)
			{
				health = 0f;
				isDead = true;
				// TODO - Fire something for death
			}

			FireHPEvent(health - healthBefore);
		}

		/// <summary>
		/// Heals the player. 
		/// If you want to heal to max health send float.max and overheal to false.
		/// overheal will allow the players HP to exceed max health.
		/// </summary>
		/// <param name="amount"></param>
		/// <param name="overheal"></param>
		public void Heal(float amount, bool overheal = false)
		{
			if (isDead)
				return;

			float healthBefore = health;
			
			health += amount;
			if (health > maxHealth && !overheal)
				health = maxHealth;

			FireHPEvent(health - healthBefore);
		}

		private void FireHPEvent(float healthDiff)
		{
			var handler = PlayerHPChanged;
			if (handler != null)
			{
				PlayerHPChanged(new PlayerEventParams(healthDiff, health, maxHealth));
			}
		}

		#endregion
	}
}