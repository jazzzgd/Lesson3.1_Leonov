using System;
using UnityEngine;

namespace Arcanoid
{
    public class GateControls : MonoBehaviour
    {
        //Объявление события
        public event Action OnDetect;

        [Header("Main Sphere"), SerializeField]
        public Transform _sphere;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out SphereControls sphere))
            {
                OnDetect?.Invoke();
            }
        }
    }
}
