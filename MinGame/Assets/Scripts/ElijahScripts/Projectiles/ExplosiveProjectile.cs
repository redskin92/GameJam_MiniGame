using ElijahScripts.Tank;
using UnityEngine;

namespace ElijahScripts.Projectiles
{
    public class ExplosiveProjectile : Projectile
    {
        [SerializeField]
        protected float explosiveRadius;
        
        protected override void OnHit(Collision2D collision)
        {
            Explode(collision);
        }

        void Explode(Collision2D collision)
        {
            Collider2D[] explosionCollisions = new Collider2D[10];
            var size = Physics2D.OverlapCircleNonAlloc(collision.contacts[0].point, explosiveRadius, explosionCollisions);

            for (int i = 0; i < size; i++)
            {
                var entity = explosionCollisions[i].GetComponentInChildren<TankEntity>();
                if (entity != null)
                {
                    entity.ApplyDamage(damageAmount);
                }
            }
        }
    }
}