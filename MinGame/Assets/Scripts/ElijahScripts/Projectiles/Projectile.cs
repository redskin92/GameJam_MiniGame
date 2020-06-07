using System;
using ElijahScripts.Manager;
using ElijahScripts.Tank;
using UnityEngine;

namespace ElijahScripts.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField]
        protected float damageAmount;

        protected virtual void OnHit(Collision2D collision)
        {
            for (int i = 0; i < collision.contactCount; i++)
            {
                var tankEntity = collision.contacts[i].collider.GetComponent<TankEntity>();
                if (tankEntity != null)
                    tankEntity.ApplyDamage(damageAmount);
            }
        }

        private void Update()
        {
            // TODO: Change to world bounds rather than camera bounds. Might make scrolling camera.
            var screenPos = TankGameManager.GetCamera().WorldToViewportPoint(transform.position);
            if (screenPos.x < 0 || screenPos.x > 1 || screenPos.y < 0)
            {
                Destroy(gameObject);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            OnHit(other);
            // TODO: Might not want to destroy all projectiles on collision.
            Destroy(gameObject);
        }
    }
}