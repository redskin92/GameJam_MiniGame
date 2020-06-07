using System;
using UnityEngine;

namespace ElijahScripts.Tank
{
    public class TankControls : MonoBehaviour
    {
        [SerializeField]
        protected float moveSpeed = 1;

        [SerializeField]
        protected float turnSpeed = 1;

        protected bool inputEnabled = false;

        protected TankEntity entity;

        private void Awake()
        {
            entity = GetComponent<TankEntity>();
        }

        public void EnableInput(bool enable)
        {
            inputEnabled = enable;
        }

        public void Update()
        {
            if (!inputEnabled)
                return;

            if (Input.GetKey(KeyCode.A))
                entity.Move(-moveSpeed * Time.deltaTime);
            if (Input.GetKey(KeyCode.D))
                entity.Move(moveSpeed * Time.deltaTime);

            if (Input.GetKey(KeyCode.Q))
                entity.Angle += turnSpeed;
            if (Input.GetKey(KeyCode.E))
                entity.Angle -= turnSpeed;

            if (Input.GetKey(KeyCode.W))
                entity.Power++;
            if (Input.GetKey(KeyCode.S))
                entity.Power--;

            if (Input.GetKeyDown(KeyCode.Space))
                entity.Fire();
        }
    }
}