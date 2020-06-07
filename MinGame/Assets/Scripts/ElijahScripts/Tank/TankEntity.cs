using System;
using ElijahScripts.Manager;
using ElijahScripts.Projectiles;
using UnityEngine;
using UnityEngine.UI;

namespace ElijahScripts.Tank
{
    public class TankEntity : MonoBehaviour
    {
        [SerializeField]
        protected float maxHealth;
        [SerializeField]
        protected float maxFuel;

        [SerializeField]
        protected float maxPower;

        [SerializeField]
        protected Transform barrelTransform;

        // TODO: Move to separate weapon handler if multiple are implemented.
        [SerializeField]
        protected Transform projectileSpawn;

        // TODO: Pull this from selected weapon if multiple are implemented.
        [SerializeField]
        protected GameObject projectilePrefab;

        [SerializeField]
        protected Image powerIndicator;
        
        protected float health;
        protected float fuel;
        
        protected float angle;
        protected float power;

        public float Health
        {
            get => health;
            protected set
            {
                health = value;
                if (health < 0)
                    health = 0;
                else if (health > maxHealth)
                    health = maxHealth;
            }
        }
        
        public float HealthPercent => health / maxHealth;

        public float Fuel
        {
            get => fuel;
            protected set
            {
                fuel = value;
                if (fuel < 0)
                    fuel = 0;
                else if (fuel > maxFuel)
                    fuel = maxFuel;
            }
        }
        
        public float FuelPercent => fuel / maxFuel;

        public float Angle
        {
            get => angle;
            set => angle = value % 360.0f;
        }
        
        public float Power
        {
            get => power;
            set
            {
                power = value;
                if (power > maxPower)
                    power = maxPower;
                else if (power < 0.0f)
                    power = 0.0f;
            }
        }
        
        public float PowerPercent => power / maxPower;

        /// <summary>
        /// Initializes the tank to initial values.
        /// </summary>
        public void Initialize()
        {
            Health = maxHealth;
            Fuel = maxFuel;
        }

        /// <summary>
        /// Fires when the player is selected.
        /// </summary>
        public void Select()
        {
            Fuel = maxFuel;
        }

        public void ApplyDamage(float damageAmount)
        {
            Health -= damageAmount;
            if (Health <= 0.0f)
                HandleDeath();
        }

        public void Move(float moveAmount)
        {
            transform.position += Vector3.right * moveAmount;
        }

        public void Fire()
        {
            GameObject projectileObject = TankGameManager.InstantiateObject(projectilePrefab, projectileSpawn.position, projectileSpawn.rotation);
            Projectile projectile = projectileObject.GetComponent<Projectile>();
            projectile.GetComponent<Rigidbody2D>().AddForce(barrelTransform.right * Power, ForceMode2D.Impulse);
        }

        private void Update()
        {
            barrelTransform.localRotation = Quaternion.Euler(0, 0, Angle);
            powerIndicator.fillAmount = PowerPercent;
        }

        private void HandleDeath()
        {
            GetComponent<TankColor>().SetColor(Color.black);
        }

        /// <summary>
        /// Prevent max values from being less than 0.
        /// </summary>
        private void OnValidate()
        {
            if (maxHealth < 1)
                maxHealth = 1;
            if (maxFuel < 1)
                maxFuel = 1;
            if (maxPower < 1)
                maxPower = 1;
        }
    }
}
