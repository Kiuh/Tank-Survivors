﻿using UnityEngine;

namespace Tank.Towers.Cannon
{
    public class Property : MonoBehaviour
    {
        [SerializeField]
        private string positionName;
        public string Name => positionName;

        private float vectorLength = 0.2f;
        private float sphereRadius = 0.02f;

        private void OnDrawGizmos()
        {
            Vector3 rotatedVector =
                Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z) * Vector3.up;

            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, rotatedVector * vectorLength);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, sphereRadius);
        }
    }
}
