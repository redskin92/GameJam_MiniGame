using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Weapons
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField]
        private float speed = 2.0f;

        [SerializeField]
        private float lifetime = 5.0f;

        private float currentLifetimeCounter;
        bool bulletActive = false;

        public void SetUpBullet()
        {
            bulletActive = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (!bulletActive)
                return;

            currentLifetimeCounter += Time.deltaTime;

            if (currentLifetimeCounter > lifetime)
            {
                Destroy(this.gameObject);
                bulletActive = false;
                return;
            }

            float travelMod = speed * Time.deltaTime;
            this.transform.Translate(Vector3.right * travelMod);
        }
    }
}